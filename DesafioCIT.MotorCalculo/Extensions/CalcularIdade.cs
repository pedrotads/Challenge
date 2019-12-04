using System;
using System.Globalization;

namespace DesafioCIT.MotorCalculo.Helpers.Extensions.CalcularIdade
{
    //Extensões para facilitar o cálculo de Idedade
    internal static class CalcularIdadeExtension
    {
        internal static int CalcularIdade(this DateTime dataNascimento)
        {
            var dataAtual = DateTime.Today;

            var idade = dataAtual.Year - dataNascimento.Year;

            if (dataNascimento.Date > dataAtual.AddYears(-idade)) idade--;

            return idade;
        }

        //Extensão para converter a data informada no request num formato padrão
        internal static bool ConverterParaDateTime(this string stringData, out DateTime data)
        {
            return DateTime.TryParseExact(
                            stringData,
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out data);
        }
    }
}