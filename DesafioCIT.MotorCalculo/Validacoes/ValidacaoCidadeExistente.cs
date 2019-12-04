using System.Threading.Tasks;
using DesafioCIT.MotorCalculo.DTOs;
using DesafioCIT.MotorCalculo.Interfaces;

namespace DesafioCIT.MotorCalculo.Validacoes
{
    //Validações de negócio
    internal class ValidacaoCidadeExistente : Validacao
    {
        private readonly IServicoDadosCidade _reperarDadosCidade;
        internal ValidacaoCidadeExistente(IServicoDadosCidade reperarDadosCidade)
        {
            _reperarDadosCidade = reperarDadosCidade;
        }

        internal override Task<ValidacaoOut> Validar(RealizarCotacaoIn entrada)
        {
            var cidades = _reperarDadosCidade.RecuperaDadosCidade();

            if (cidades.Contains(entrada.Endereco.Cidade.ToUpper()))
            {
                return Task.FromResult(new ValidacaoOut());
            }

            return Task.FromResult(new ValidacaoOut(false, ValidacaoConstantes.CidadeNaoEncontrada));
        }
    }
}