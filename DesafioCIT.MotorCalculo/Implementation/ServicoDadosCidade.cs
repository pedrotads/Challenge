using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using DesafioCIT.MotorCalculo.Interfaces;

namespace DesafioCIT.MotorCalculo.Implementation
{
    //Classe criada para buscar a lista de cidades validas do https://www.redesocialdecidades.org.br/cities
    //A idéia seria evoluir e criar algum cache para não termos que fazer o request em toda a requisição
    internal class ServicoDadosCidade : IServicoDadosCidade
    {
        public List<string> RecuperaDadosCidade()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync("https://www.redesocialdecidades.org.br/cities");
                response.Wait();

                var responseBody = response.Result.Content.ReadAsStringAsync().Result;
                return JsonDocument.Parse(responseBody)
                    .RootElement.GetProperty("cities")
                    .EnumerateArray()
                    .Select(x => x.GetProperty("name").ToString().ToUpper())
                    .ToList();
            }
        }
    }
}
