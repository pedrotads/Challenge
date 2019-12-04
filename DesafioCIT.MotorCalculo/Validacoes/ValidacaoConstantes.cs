namespace DesafioCIT.MotorCalculo.Validacoes
{
    //Constantes das mensagens de retorno.
    //Criei apenas para as que iria utilizar nos testes.
    public static class ValidacaoConstantes
    {
        public const string CidadeNaoEncontrada = "Cidade não encontrada.";
        public const string CoberturasNaoPodemRepetir = "Coberturas não podem repetir.";
        public const string CoberturasInvalidas = "Existem coberturas inválidas.";
        public const string CoberturasAoMenosUmaObrigatoria = "Ao menos uma das coberturas precisa ser obrigatória.";
        public const string CoberturasMaximo4Excedido = "Máximo de 4 coberturas excedido.";
        public const string CepForaDoFormato = "CEP fora do formato.";
        public const string DataForaDoFormato = "Data fora do formato.";
        public const string MenorDe18Anos = "Menor de 18 anos.";
        public const string MaiorDe45Anos = "Idade maior que 45 anos.";


    }
}