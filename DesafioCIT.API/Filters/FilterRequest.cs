using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace DesafioCIT.API.Filters
{
    public class FilterRequest : IResourceFilter
    {
        private readonly ILogger _logger;

        public FilterRequest(ILogger<FilterRequest> logger)
        {
            _logger = logger;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            try
            {
                _logger.LogInformation("Processanto Resource enviado...");

                var request = context.HttpContext.Request;
                request.EnableBuffering();
                request.Body.Position = 0;
                using (var reader = new StreamReader(request.Body))
                {
                    var resource = reader.ReadToEndAsync().Result;
                    _logger.LogInformation(resource);

                    var novoResource = JsonDocument.Parse(resource).RootElement.GetProperty("request").ToString();

                    _logger.LogInformation("Alterando Resource que será enviado ao controller...");
                    _logger.LogInformation($"Novo Resource: { novoResource }");

                    byte[] bytes = Encoding.UTF8.GetBytes(novoResource);
                    request.Body = new MemoryStream(bytes);
                }
            }
            catch
            {
                _logger.LogInformation("Formato inexperado...");
                context.Result = new BadRequestObjectResult("Json fora do formato permitido: Exemplo: { 'request': ... }");
            }
        }
    }
}