using Cartola.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cartola.Infra.Models
{
    public class PartidasJson
    {
        [JsonPropertyName("partidas")]
        public List<Partida> Partidas { get; set; }

        [JsonPropertyName("rodada")]
        public int RodadaAtual { get; set; }
    }
}
