using Cartola.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cartola.Infra.Models
{
    public class AtletasPontuadosJson
    {
        [JsonPropertyName("rodada")]
        public int Rodada { get; set; }

        //[JsonPropertyName("atletas")]
        //public Dictionary<string, Atleta> Clubes { get; set; }

        [JsonPropertyName("posicoes")]
        public Dictionary<string, Posicao> Posicoes { get; set; }

        [JsonPropertyName("total_atletas")]
        public int TotalAtletas { get; set; }
    }
}
