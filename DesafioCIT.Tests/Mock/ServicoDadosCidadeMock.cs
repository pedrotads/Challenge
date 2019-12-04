using System.Collections.Generic;
using DesafioCIT.MotorCalculo.Interfaces;

namespace DesafioCIT.Tests.Mock
{
    //Classe para simular nosso ServicoDadosCidade, assim não dependemos de terceiros para nossos testes funcionarem (https://www.redesocialdecidades.org.br/cities)
    public class ServicoDadosCidadeMock : IServicoDadosCidade
    {
        public List<string> RecuperaDadosCidade()
        {
            return new List<string>() { "ARARAS", "CAMPINAS" };
        }
    }
}
