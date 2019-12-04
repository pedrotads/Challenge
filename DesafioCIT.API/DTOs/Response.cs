using System;
using System.Text.Json.Serialization;

namespace DesafioCIT.API.DTOs
{
    public class Response<T> where T : class
    {
        public Response(T response)
        {
            ResponseObject = response;
        }
        [JsonPropertyName("response")]
        public T ResponseObject { get; set; }

    }
}