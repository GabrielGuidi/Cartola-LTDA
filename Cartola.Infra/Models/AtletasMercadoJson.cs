using Cartola.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cartola.Infra.Models
{
    public class AtletasMercadoJson
    {
        [JsonPropertyName("atletas")]
        public List<Jogador> Atletas { get; set; }

        [JsonPropertyName("clubes")]
        public Dictionary<string, Clube> Clubes { get; set; }

        [JsonPropertyName("posicoes")]
        public Dictionary<string, Posicao> Posicoes { get; set; }

        [JsonPropertyName("status")]
        public Dictionary<string, Status> Status { get; set; }
    }
}
