
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioCIT.MotorCalculo.DataAccessLayer;
using DesafioCIT.MotorCalculo.DataAccessLayer.Models;
using DesafioCIT.MotorCalculo.DTOs;
using DesafioCIT.MotorCalculo.Interfaces;
using DesafioCIT.MotorCalculo.Validacoes;
using DesafioCIT.MotorCalculo.Helpers.Extensions.CalcularIdade;
using System;

namespace DesafioCIT.MotorCalculo.Implementation
{
    //Classe responsável pelas regras da cotação, deixei como internal para impedir acesso da mesma de fora do assembly
    //A idéia é que esse project seja como um produto, expondo somente o necessário aos seus consumidores (factories, interfaces, DTO's)
    internal class MotorCalculo : IMotorCalculo
    {
        private readonly DataBaseContext _dbContext;
        private readonly IServicoDadosCidade _servicoDadosCidade;
        internal MotorCalculo(DataBaseContext dbContext, IServicoDadosCidade servicoDadosCidade)
        {
            _servicoDadosCidade = servicoDadosCidade;
            _dbContext = dbContext;
        }

        //Método responsável por realizar as validações e a cotação
        public async Task<RealizarCotacaoOut> RealizarCotacao(RealizarCotacaoIn entrada)
        {
            var retorno = new RealizarCotacaoOut();


            //Grupo de regras para a cotação. A idéia seria evoluir isso para um mecanismo que buscasse do banco de dados ou do config essas regras.
            //Permitindo que os clientes configurem suas regras.
            var validacoes = new List<Validacao>()
            {
                new ValidacaoMenor18Anos(),
                new ValidacaoMaior45Anos(),
                new ValidacaoCidadeExistente(_servicoDadosCidade),
                new ValidacaoFormatoCEP(),
                new ValidacaoCoberturas(_dbContext)
            };

            List<Task<ValidacaoOut>> tasksValidacao = new List<Task<ValidacaoOut>>();
            foreach (var validacao in validacoes)
            {
                tasksValidacao.Add(validacao.Validar(entrada));
            }

            await Task.WhenAll<ValidacaoOut>(tasksValidacao);

            var resultadoValidacoes = tasksValidacao.Select(x => x.Result).ToList();

            var validacoesFalhas = resultadoValidacoes.Where(x => !x.Valida).ToList();

            //Em caso de inconsistências, retorna com a flag SucessoCotação = false
            if (validacoesFalhas.Any())
            {
                retorno.SucessoCotacao = false;
                retorno.MensagensInconsistenca.AddRange(validacoesFalhas.Select(x => x.Mensagem));
                return retorno;
            }

            DateTime dataNascimento;

            entrada.Nascimento.ConverterParaDateTime(out dataNascimento);

            var idade = dataNascimento.CalcularIdade();

            var listaCoberturas = _dbContext.Coberturas
                                    .Where(x => entrada.Coberturas.Contains(x.Id)).ToList();

            retorno.Premio = CalcularPremio(listaCoberturas, idade);
            retorno.CoberturaTotal = listaCoberturas.Sum(x => x.Valor);
            retorno.QuantidadeParcelas = CaclularQuantidadeParcelas(retorno.Premio);
            retorno.PrimeiroVencimento = CalcularPrimeiroVencimento();

            return retorno;
        }

        private decimal CalcularPremio(List<Cobertura> coberturas, int idade)
        {
            var fator = idade <= 30
                ? 0.08m * (30 - idade)
                : -0.02m * (idade - 30);

            return coberturas.Sum(x => x.Premio) * (1 + fator);
        }

        private int CaclularQuantidadeParcelas(decimal valorPremio)
        {
            if (valorPremio <= 500)
            {
                return 1;
            }

            if (valorPremio <= 1000)
            {
                return 2;
            }

            if (valorPremio <= 2000)
            {
                return 3;
            }

            return 4;
        }

        private DateTime CalcularPrimeiroVencimento()
        {
            DateTime dataAtual = DateTime.Now;

            int diasUteis = 1;

            DateTime dataPrimeiroVencimento = new DateTime(dataAtual.Year, dataAtual.Month, 1);
            dataPrimeiroVencimento = dataPrimeiroVencimento.AddMonths(1);
            do
            {
                if (dataPrimeiroVencimento.DayOfWeek != DayOfWeek.Sunday
                    && dataPrimeiroVencimento.DayOfWeek != DayOfWeek.Saturday)
                {
                    diasUteis++;
                }
                dataPrimeiroVencimento = dataPrimeiroVencimento.AddDays(1);

            } while (diasUteis < 5);

            return dataPrimeiroVencimento;
        }
    }
}
