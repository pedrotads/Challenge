using System;
using System.Threading.Tasks;
using DesafioCIT.MotorCalculo.DTOs;
using DesafioCIT.MotorCalculo.Helpers.Extensions.CalcularIdade;

namespace DesafioCIT.MotorCalculo.Validacoes
{
    //Validações de negócio
    internal class ValidacaoMaior45Anos : Validacao
    {
        internal ValidacaoMaior45Anos()
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

            if (dataNascimento.CalcularIdade() > 45)
            {
                return Task.FromResult(new ValidacaoOut(false, ValidacaoConstantes.MaiorDe45Anos));
            }

            return Task.FromResult(new ValidacaoOut());
        }
    }
}