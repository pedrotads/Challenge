namespace DesafioCIT.MotorCalculo.DTOs
{
    //Dto para comunicação do resultado das validações do Motor de Cálculo
    public class ValidacaoOut
    {
        public ValidacaoOut(bool valida, string mensagem)
        {
            this.Valida = valida;
            this.Mensagem = mensagem;
        }

        public ValidacaoOut()
        {
            this.Valida = true;
        }

        public bool Valida { get; set; }
        public string Mensagem { get; set; }
    }
}