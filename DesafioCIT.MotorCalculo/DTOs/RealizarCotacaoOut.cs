using System;
using System.Collections.Generic;

namespace DesafioCIT.MotorCalculo.DTOs
{
    //Dto para comunicação do retorno da cotação do Motor de Cálculo
    public class RealizarCotacaoOut
    {

        public RealizarCotacaoOut()
        {
            SucessoCotacao = true;
        }
        public bool SucessoCotacao { get; set; }

        public List<string> MensagensInconsistenca { get; set; } = new List<string>();

        private decimal _premio;
        public decimal Premio
        {
            get { return RetornaValorFormatado(_premio); }
            set { _premio = value; }
        }
        public int QuantidadeParcelas { get; set; }

        public decimal ValorParcelas
        {
            get { return RetornaValorFormatado(_premio / QuantidadeParcelas); }
        }

        public DateTime PrimeiroVencimento { get; set; }

        private decimal _coberturaTotal;
        public decimal CoberturaTotal
        {
            get { return RetornaValorFormatado(_coberturaTotal); }
            set { _coberturaTotal = value; }
        }

        private decimal RetornaValorFormatado(decimal valor)
        {
            return Math.Round(valor, 2, MidpointRounding.AwayFromZero);
        }
    }
}