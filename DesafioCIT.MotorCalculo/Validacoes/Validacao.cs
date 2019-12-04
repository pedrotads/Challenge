using System.Threading.Tasks;
using DesafioCIT.MotorCalculo.DTOs;

namespace DesafioCIT.MotorCalculo.Validacoes
{
    //Classe base para o mecanismo de validação
    internal abstract class Validacao
    {
        internal virtual Task<ValidacaoOut> Validar(RealizarCotacaoIn entrada)
        {
            return Task.FromResult(new ValidacaoOut());
        }
    }
}
