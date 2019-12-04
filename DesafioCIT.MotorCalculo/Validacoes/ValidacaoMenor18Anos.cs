using System;
using System.Threading.Tasks;
using DesafioCIT.MotorCalculo.DTOs;
using DesafioCIT.MotorCalculo.Helpers.Extensions.CalcularIdade;

namespace DesafioCIT.MotorCalculo.Validacoes
{
    //Validações de negócio
    internal class ValidacaoMenor18Anos : Validacao
    {
        internal ValidacaoMenor18Anos()
        {
        }

        internal override Task<ValidacaoOut> Validar(RealizarCotacaoIn entrada)
        {
            DateTime dataNascimento;

            var formatoDataValido = entrada.Nascimento.ConverterParaDateTime(out dataNascimento);

            if (!formatoDataValido)
            {
                return Task.FromResult(new ValidacaoOut(false, ValidacaoConstantes.DataForaDoFormato));
            }

            if (dataNascimento.CalcularIdade() < 18)
            {
                return Task.FromResult(new ValidacaoOut(false, ValidacaoConstantes.MenorDe18Anos));
            }

            return Task.FromResult(new ValidacaoOut());
        }
    }
}