using System;
using Xunit;
using DesafioCIT.Tests.Mock;
using DesafioCIT.API.Controllers;
using DesafioCIT.MotorCalculo.Implementation;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using DesafioCIT.MotorCalculo.Validacoes;
using Microsoft.Extensions.Logging.Abstractions;

namespace DesafioCIT.Tests
{
    public class ValidacoesFuncionais
    {
        [Fact]
        public void TesteComValoresDefault()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock();
                var resultadoExperado = ResultDefaultPrinceOut.Mock();

                var resultado = controller.Post(entrada);

                var obj1Str = JsonConvert.SerializeObject(resultadoExperado);
                var obj2Str = JsonConvert.SerializeObject(resultado.Value);

                dbContext.Dispose();

                Assert.Equal(obj1Str, obj2Str);
            }
        }

        [Fact]
        public void MenorDe18Anos()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>()); var entrada = InputDefaultPrinceIn.Mock(17);

                var resultado = controller.Post(entrada);

                Assert.IsType<BadRequestObjectResult>(resultado.Result);

                var result = resultado.Result as BadRequestObjectResult;
                var mensagem = (string)result.Value;
                Assert.Contains(mensagem, ValidacaoConstantes.MenorDe18Anos);
            }
        }

        [Fact]
        public void MaiorDe45Anos()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock(46);

                var resultado = controller.Post(entrada);

                Assert.IsType<BadRequestObjectResult>(resultado.Result);

                var result = resultado.Result as BadRequestObjectResult;
                var mensagem = (string)result.Value;
                Assert.Contains(mensagem, ValidacaoConstantes.MaiorDe45Anos);
            }
        }

        [Fact]
        public void CidadeInvalida()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock();
                entrada.Endereco.Cidade = "lalala";
                var resultado = controller.Post(entrada);

                Assert.IsType<BadRequestObjectResult>(resultado.Result);

                var result = resultado.Result as BadRequestObjectResult;
                var mensagem = (string)result.Value;
                Assert.Contains(mensagem, ValidacaoConstantes.CidadeNaoEncontrada);
            }
        }

        [Fact]
        public void SemCoberturaObrigatoria()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock();
                entrada.Coberturas = new List<string>() { DbContextMock.ListaDeCoberturas().First(x => !x.Obrigatorio).Id };
                var resultado = controller.Post(entrada);

                Assert.IsType<BadRequestObjectResult>(resultado.Result);

                var result = resultado.Result as BadRequestObjectResult;
                var mensagem = (string)result.Value;
                Assert.Contains(mensagem, ValidacaoConstantes.CoberturasAoMenosUmaObrigatoria);
            }
        }

        [Fact]
        public void CoberturasRepetidas()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var cobertura = DbContextMock.ListaDeCoberturas().First(x => !x.Obrigatorio).Id;

                var entrada = InputDefaultPrinceIn.Mock();
                entrada.Coberturas = new List<string>() { cobertura, cobertura };
                var resultado = controller.Post(entrada);

                Assert.IsType<BadRequestObjectResult>(resultado.Result);

                var result = resultado.Result as BadRequestObjectResult;
                var mensagem = (string)result.Value;
                Assert.Contains(mensagem, ValidacaoConstantes.CoberturasNaoPodemRepetir);
            }
        }

        [Fact]
        public void CoberturasInvalidas()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock();
                entrada.Coberturas = new List<string>() { "00" };
                var resultado = controller.Post(entrada);

                Assert.IsType<BadRequestObjectResult>(resultado.Result);

                var result = resultado.Result as BadRequestObjectResult;
                var mensagem = (string)result.Value;
                Assert.Contains(mensagem, ValidacaoConstantes.CoberturasInvalidas);
            }
        }

        [Fact]
        public void MaximoDeCoberturas()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock();
                entrada.Coberturas = DbContextMock.ListaDeCoberturas().Select(x => x.Id).Take(5).ToList();
                var resultado = controller.Post(entrada);

                Assert.IsType<BadRequestObjectResult>(resultado.Result);

                var result = resultado.Result as BadRequestObjectResult;
                var mensagem = (string)result.Value;
                Assert.Contains(mensagem, ValidacaoConstantes.CoberturasMaximo4Excedido);
            }
        }

        [Fact]
        public void CepForaDoFormato()
        {
            using (var dbContext = DbContextMock.DataBaseMock())
            {
                PriceController controller = new PriceController(MotorCalculoFactory.RecuperarInstancia(dbContext, new ServicoDadosCidadeMock()), new NullLogger<PriceController>());

                var entrada = InputDefaultPrinceIn.Mock();
                entrada.Endereco.CEP = "123";
                var resultado = controller.Post(entrada);

                Assert.IsType<BadRequestObjectResult>(resultado.Result);

                var result = resultado.Result as BadRequestObjectResult;
                var mensagem = (string)result.Value;
                Assert.Contains(mensagem, ValidacaoConstantes.CepForaDoFormato);
            }
        }
    }
}
