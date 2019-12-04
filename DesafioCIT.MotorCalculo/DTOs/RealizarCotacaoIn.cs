using System.Collections.Generic;

namespace DesafioCIT.MotorCalculo.DTOs
{
    //Dto para comunicação com o Motor de Cálculo
    public class RealizarCotacaoIn
    {
        public string Nome { get; set; }

        public string Nascimento { get; set; }

        public RealizarCotacaoEnderecoIn Endereco { get; set; }

        public List<string> Coberturas { get; set; }
    }
}