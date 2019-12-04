using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DesafioCIT.API.DTOs
{
    public class PriceIn
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Nascimento { get; set; }

        [Required]
        public PriceEnderecoIn Endereco { get; set; }

        [Required]
        public List<string> Coberturas { get; set; }
    }
}