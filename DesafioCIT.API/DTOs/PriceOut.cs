using System;
using System.Text.Json.Serialization;

namespace DesafioCIT.API.DTOs
{
    public class PriceOut

    {
        [JsonPropertyName("premio")]
        public decimal Premio { get; set; }

        [JsonPropertyName("parcelas")]
        public int Parcelas { get; set; }

        [JsonPropertyName("valor_parcelas")]
        public decimal ValorParcelas { get; set; }

        [JsonPropertyName("primeiro_vencimento")]
        public string PrimeiroVencimento { get; set; }

        [JsonPropertyName("cobertura_total")]
        public decimal CoberturaTotal { get; set; }

        public PriceOut()
        {

        }
        public PriceOut(MotorCalculo.DTOs.RealizarCotacaoOut resultadoCotacao)
        {
            this.Premio = resultadoCotacao.Premio;
            this.Parcelas = resultadoCotacao.QuantidadeParcelas;
            this.ValorParcelas = resultadoCotacao.ValorParcelas;
            this.PrimeiroVencimento = CoverterDataParaPadraoRetorno(resultadoCotacao.PrimeiroVencimento);
            this.CoberturaTotal = resultadoCotacao.CoberturaTotal;
        }

        private string CoverterDataParaPadraoRetorno(DateTime data)
        {
            return data.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}