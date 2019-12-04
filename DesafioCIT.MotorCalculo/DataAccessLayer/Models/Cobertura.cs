namespace DesafioCIT.MotorCalculo.DataAccessLayer.Models
{
    //Modelo que representa a "tabela Cobertura"
    public class Cobertura
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public decimal Premio { get; set; }
        public decimal Valor { get; set; }
        public bool Obrigatorio { get; set; }
    }
}
