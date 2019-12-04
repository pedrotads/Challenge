using System.Threading.Tasks;
using DesafioCIT.MotorCalculo.DTOs;

namespace DesafioCIT.MotorCalculo.Interfaces
{
    public interface IMotorCalculo
    {
        Task<RealizarCotacaoOut> RealizarCotacao(RealizarCotacaoIn entrada);
    }
}
