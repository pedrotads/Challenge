using Microsoft.AspNetCore.Mvc;
using System.Net;
using DesafioCIT.MotorCalculo.Interfaces;
using DesafioCIT.API.Filters;
using DesafioCIT.API.DTOs;
using System;
using Microsoft.Extensions.Logging;

namespace DesafioCIT.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly IMotorCalculo _motorCalculo;
        private readonly ILogger _logger;
        public PriceController(IMotorCalculo motorCalculo, ILogger<PriceController> logger)
        {
            _motorCalculo = motorCalculo;
        }

        [TypeFilter(typeof(FilterRequest))]
        [HttpPost]
        //Action para o desafio
        public ActionResult<Response<PriceOut>> Post([FromBody] PriceIn entrada)
        {
            try
            {
                //faz o mapping manual dos dados do modelo
                var cotacaoIn = new MotorCalculo.DTOs.RealizarCotacaoIn()
                {
                    Coberturas = entrada.Coberturas,
                    Endereco = new MotorCalculo.DTOs.RealizarCotacaoEnderecoIn()
                    {
                        Bairro = entrada.Endereco.Bairro,
                        CEP = entrada.Endereco.CEP,
                        Cidade = entrada.Endereco.Cidade,
                        Logradouro = entrada.Endereco.Logradouro,
                    },
                    Nascimento = entrada.Nascimento,
                    Nome = entrada.Nome
                };

                //chama o motor de cálculo para realizara cotação
                var resutado = _motorCalculo.RealizarCotacao(cotacaoIn).Result;

                //em caso de erro de negócio, informamos apenas uma mensagem simples com código 400
                if (!resutado.SucessoCotacao)
                {
                    return new BadRequestObjectResult(string.Join("\n", resutado.MensagensInconsistenca));
                }

                //Retornamos para o cliente sua cotação
                return new Response<PriceOut>(new PriceOut(resutado));
            }
            catch (Exception ex)
            {
                //Em caso de erro inexperado, enviamos um código 500 com uma mensagem padrão para o clinete
                _logger.LogError($"Mensagem: {ex.Message} StackTrace: {ex.StackTrace}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um erro inexperado no sistema, contate o admnistrador...");
            }
        }
    }
}
