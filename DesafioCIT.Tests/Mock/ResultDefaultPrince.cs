using System;
using DesafioCIT.API.DTOs;

namespace DesafioCIT.Tests.Mock
{
    //Esse é um response "default", criei ele para facilitar a criação dos testes, não precisando criar um objeto diferente a cada teste.
    //Basta alterar conforme necessidade.
    public static class ResultDefaultPrinceOut
    {
        public static Response<PriceOut> Mock()
        {
            var priceOut = new PriceOut();
            priceOut.Premio = 132.00m;
            priceOut.Parcelas = 1;
            priceOut.ValorParcelas = 132.00m;
            priceOut.PrimeiroVencimento = CalcularPrimeiroVencimento().ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            priceOut.CoberturaTotal = 50000.00m;

            return new Response<PriceOut>(priceOut);
        }

        //Metodo foi duplicado conforme email
        private static DateTime CalcularPrimeiroVencimento()
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

