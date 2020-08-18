using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    public class Escalacao
    {
        [JsonPropertyName("esquema")]
        public int EsquemaId { get; set; }

        [JsonPropertyName("capitao")]
        public int Capitao { get; set; }

        [JsonPropertyName("atletas")]
        public int[] Atletas { get; set; }

        [JsonIgnore]
        public Jogador[] ListaAtletas { get; set; }
    }
}
