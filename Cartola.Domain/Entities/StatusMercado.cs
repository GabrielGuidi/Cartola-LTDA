using System;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    public class StatusMercado
    {
        [JsonPropertyName("status_mercado")]
        public int Status { get; set; }

        [JsonPropertyName("rodada_atual")]
        public int RodadaAtual { get; set; }

        [JsonPropertyName("times_escalados")]
        public long TimesEscalados { get; set; }

        [JsonPropertyName("fechamento")]
        public StatusMercadoFechamento Fechamento { get; set; }

        [JsonPropertyName("mercado_pos_rodada")]
        public bool MercadoPosRodada { get; set; }

        [JsonPropertyName("aviso")]
        public string Aviso { get; set; }

        [JsonPropertyName("aviso_url")]
        public string AvisoUrl { get; set; }

        [JsonIgnore]
        public DateTime DataFechamento { get; set; }
    }

    public class StatusMercadoFechamento
    {
        [JsonPropertyName("dia")]
        public int Dia { get; set; }

        [JsonPropertyName("mes")]
        public int Mes { get; set; }

        [JsonPropertyName("ano")]
        public int Ano { get; set; }

        [JsonPropertyName("hora")]
        public int Hora { get; set; }

        [JsonPropertyName("minuto")]
        public int Minuto { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
