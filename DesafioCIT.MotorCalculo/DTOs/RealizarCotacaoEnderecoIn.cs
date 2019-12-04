namespace DesafioCIT.MotorCalculo.DTOs
{
    //Dto para comunicação com o Motor de Cálculo
    public class RealizarCotacaoEnderecoIn
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
    }
}