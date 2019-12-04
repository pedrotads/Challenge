using Xunit;
using DesafioCIT.Tests.Mock;
using DesafioCIT.API.Controllers;
using DesafioCIT.MotorCalculo.Implementation;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Logging.Abstractions;

namespace DesafioCIT.Tests
{
    public class ValidacoesValores
    {
        [Fact]
        public void Teste18Anos()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock(18, new List<string>() { "01", "02" });
                var resultadoExperado = ResultDefaultPrinceOut.Mock();

                resultadoExperado.ResponseObject.Premio = 254.80m;
                resultadoExperado.ResponseObject.ValorParcelas = 254.80m;
                resultadoExperado.ResponseObject.CoberturaTotal = 55000.00m;

                var resultado = controller.Post(entrada);

                var obj1Str = JsonConvert.SerializeObject(resultadoExperado);
                var obj2Str = JsonConvert.SerializeObject(resultado.Value);

                dbContext.Dispose();

                Assert.Equal(obj1Str, obj2Str);
            }
        }

        [Fact]
        public void Teste30Anos()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock(30, new List<string>() { "01", "02" });
                var resultadoExperado = ResultDefaultPrinceOut.Mock();

                resultadoExperado.ResponseObject.Premio = 130.00m;
                resultadoExperado.ResponseObject.ValorParcelas = 130.00m;
                resultadoExperado.ResponseObject.CoberturaTotal = 55000.00m;

                var resultado = controller.Post(entrada);

                var obj1Str = JsonConvert.SerializeObject(resultadoExperado);
                var obj2Str = JsonConvert.SerializeObject(resultado.Value);

                dbContext.Dispose();

                Assert.Equal(obj1Str, obj2Str);
            }
        }

        [Fact]
        public void Teste45Anos()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock(45, new List<string>() { "01", "02" });
                var resultadoExperado = ResultDefaultPrinceOut.Mock();

                resultadoExperado.ResponseObject.Premio = 91.00m;
                resultadoExperado.ResponseObject.ValorParcelas = 91.00m;
                resultadoExperado.ResponseObject.CoberturaTotal = 55000.00m;

                var resultado = controller.Post(entrada);

                var obj1Str = JsonConvert.SerializeObject(resultadoExperado);
                var obj2Str = JsonConvert.SerializeObject(resultado.Value);

                dbContext.Dispose();

                Assert.Equal(obj1Str, obj2Str);
            }
        }

        [Fact]
        public void Teste30Anos1Parcela()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock(30, new List<string>() { "-01" });
                var resultadoExperado = ResultDefaultPrinceOut.Mock();

                resultadoExperado.ResponseObject.Premio = 500.00m;
                resultadoExperado.ResponseObject.ValorParcelas = 500.00m;
                resultadoExperado.ResponseObject.CoberturaTotal = 500.00m;
                resultadoExperado.ResponseObject.ValorParcelas = 500.00m;
                resultadoExperado.ResponseObject.Parcelas = 1;

                var resultado = controller.Post(entrada);

                var obj1Str = JsonConvert.SerializeObject(resultadoExperado);
                var obj2Str = JsonConvert.SerializeObject(resultado.Value);

                dbContext.Dispose();

                Assert.Equal(obj1Str, obj2Str);
            }
        }

        [Fact]
        public void Teste30Anos2Parcelas()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock(30, new List<string>() { "-01", "-02" });
                var resultadoExperado = ResultDefaultPrinceOut.Mock();

                resultadoExperado.ResponseObject.Premio = 1000.00m;
                resultadoExperado.ResponseObject.ValorParcelas = 1000.00m;
                resultadoExperado.ResponseObject.CoberturaTotal = 1000.00m;
                resultadoExperado.ResponseObject.ValorParcelas = 500.00m;
                resultadoExperado.ResponseObject.Parcelas = 2;

                var resultado = controller.Post(entrada);

                var obj1Str = JsonConvert.SerializeObject(resultadoExperado);
                var obj2Str = JsonConvert.SerializeObject(resultado.Value);

                dbContext.Dispose();

                Assert.Equal(obj1Str, obj2Str);
            }
        }
        [Fact]
        public void Teste30Anos3Parcelas()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock(30, new List<string>() { "-01", "-02", "-03" });
                var resultadoExperado = ResultDefaultPrinceOut.Mock();

                resultadoExperado.ResponseObject.Premio = 1500.00m;
                resultadoExperado.ResponseObject.ValorParcelas = 1500.00m;
                resultadoExperado.ResponseObject.CoberturaTotal = 1500.00m;
                resultadoExperado.ResponseObject.ValorParcelas = 500.00m;
                resultadoExperado.ResponseObject.Parcelas = 3;

                var resultado = controller.Post(entrada);

                var obj1Str = JsonConvert.SerializeObject(resultadoExperado);
                var obj2Str = JsonConvert.SerializeObject(resultado.Value);

                dbContext.Dispose();

                Assert.Equal(obj1Str, obj2Str);
            }
        }
        [Fact]
        public void Teste30Anos4Parcelas()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock(30, new List<string>() { "-01", "-02", "-03", "-04" });
                var resultadoExperado = ResultDefaultPrinceOut.Mock();

                resultadoExperado.ResponseObject.Premio = 2500.00m;
                resultadoExperado.ResponseObject.ValorParcelas = 1500.00m;
                resultadoExperado.ResponseObject.CoberturaTotal = 2500.00m;
                resultadoExperado.ResponseObject.ValorParcelas = 625.00m;
                resultadoExperado.ResponseObject.Parcelas = 4;

                var resultado = controller.Post(entrada);

                var obj1Str = JsonConvert.SerializeObject(resultadoExperado);
                var obj2Str = JsonConvert.SerializeObject(resultado.Value);

                dbContext.Dispose();

                Assert.Equal(obj1Str, obj2Str);
            }
        }

    }
}
