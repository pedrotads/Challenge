using System.Threading.Tasks;
using DesafioCIT.MotorCalculo.DTOs;

namespace DesafioCIT.MotorCalculo.Validacoes
{
    //Validações de negócio
    internal class ValidacaoFormatoCEP : Validacao
    {
        internal ValidacaoFormatoCEP()
        {
        }

        internal override Task<ValidacaoOut> Validar(RealizarCotacaoIn entrada)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(entrada.Endereco.CEP, ("[0-9]{5}-[0-9]{3}"))
                ? Task.FromResult(new ValidacaoOut()) : Task.FromResult(new ValidacaoOut(false, ValidacaoConstantes.CepForaDoFormato));
        }
    }
}