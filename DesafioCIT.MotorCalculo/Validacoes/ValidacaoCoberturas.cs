using System.Linq;
using System.Threading.Tasks;
using DesafioCIT.MotorCalculo.DataAccessLayer;
using DesafioCIT.MotorCalculo.DTOs;


namespace DesafioCIT.MotorCalculo.Validacoes
{
    //Validações de negócio
    internal class ValidacaoCoberturas : Validacao
    {
        private readonly DataBaseContext _dbContext;
        internal ValidacaoCoberturas(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal override Task<ValidacaoOut> Validar(RealizarCotacaoIn entrada)
        {
            var coberturasEntrada = entrada.Coberturas;

            var repetidas = coberturasEntrada
                 .GroupBy(cob => cob)
                 .Where(cobRepetidas => cobRepetidas.Count() > 1)
                 .Select(cob => cob)
                 .ToList();

            if (repetidas.Any())
            {
                return Task.FromResult(new ValidacaoOut(false, ValidacaoConstantes.CoberturasNaoPodemRepetir));
            }

            var coberturasBanco = _dbContext.Coberturas.Where(x => coberturasEntrada.Contains(x.Id)).ToList();

            if (coberturasBanco.Count != coberturasEntrada.Count)
            {
                return Task.FromResult(new ValidacaoOut(false, ValidacaoConstantes.CoberturasInvalidas));
            }

            if (!coberturasBanco.Any(x => x.Obrigatorio))
            {
                return Task.FromResult(new ValidacaoOut(false, ValidacaoConstantes.CoberturasAoMenosUmaObrigatoria));
            }

            if (coberturasBanco.Count() > 4)
            {
                return Task.FromResult(new ValidacaoOut(false, ValidacaoConstantes.CoberturasMaximo4Excedido));
            }

            return Task.FromResult(new ValidacaoOut());
        }
    }
}