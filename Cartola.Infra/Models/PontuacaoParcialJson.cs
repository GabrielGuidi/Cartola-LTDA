using Cartola.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cartola.Infra.Models
{
    class PontuacaoParcialJson
    {
        [JsonPropertyName("rodada")]
        public int Rodada { get; set; }

        [JsonPropertyName("atletas")]
        public Dictionary<string, PontuacaoParcial> Atletas{ get; set; }

        [JsonPropertyName("total_atletas")]
        public int TotalAtletas { get; set; }
    }
}
