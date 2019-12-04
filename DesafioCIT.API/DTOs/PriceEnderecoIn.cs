using System.ComponentModel.DataAnnotations;

namespace DesafioCIT.API.DTOs
{
    public class PriceEnderecoIn
    {
        [Required]
        public string Logradouro { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Cidade { get; set; }
    }
}