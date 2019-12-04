using System;
using DesafioCIT.API.DTOs;
using System.Collections.Generic;

namespace DesafioCIT.Tests.Mock
{
    //Esse é um request "default", criei ele para facilitar a criação dos testes, não precisando criar um objeto diferente a cada teste.
    //Basta alterar conforme necessidade.
    //Essa classe também já faz o cálculo da idade
    public static class InputDefaultPrinceIn
    {
        public static PriceIn Mock(int idade = 26, List<string> coberturas = null, string cep = "13123-123", string cidade = "Campinas")
        {
            coberturas = coberturas == null ? new List<string>() { "01" } : coberturas;

            DateTime dataNascimento = DateTime.Now.AddYears(idade * -1);

            return new PriceIn()
            {
                Nome = "Emerson",
                Nascimento = dataNascimento.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                Coberturas = coberturas,
                Endereco = new PriceEnderecoIn()
                {
                    Bairro = "Teste bairro",
                    CEP = cep,
                    Cidade = cidade,
                    Logradouro = "Teste Logradouro"
                }
            };
        }
    }
}
