using System.Text.Json.Serialization;

namespace Cartola.Infra.Models
{
    public class AuthenticationJson
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("userMessage")]
        public string UserMessage { get; set; }

        [JsonPropertyName("glbId")]
        public string GlobalId { get; set; }
    }
}
