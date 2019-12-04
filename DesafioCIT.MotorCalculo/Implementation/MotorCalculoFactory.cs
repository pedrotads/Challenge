using DesafioCIT.MotorCalculo.DataAccessLayer;
using DesafioCIT.MotorCalculo.Interfaces;

namespace DesafioCIT.MotorCalculo.Implementation
{
    //Factory responsável por dar acesso as assemblies de fora ao motor de cálculo
    public static class MotorCalculoFactory
    {
        public static IMotorCalculo RecuperarInstancia(DataBaseContext dbContext)
        {
            return new MotorCalculo(dbContext, new ServicoDadosCidade());
        }

        public static IMotorCalculo RecuperarInstancia(DataBaseContext dbContext, IServicoDadosCidade servicoDadosCidade)
        {
            return new MotorCalculo(dbContext, servicoDadosCidade);
        }
    }
}
