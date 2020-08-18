using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    public class AtletaJson
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("apelido")]
        public string Apelido { get; set; }

        [JsonPropertyName("foto")]
        public string Foto { get; set; }

        [JsonPropertyName("atleta_id")]
        public int AtletaId { get; set; }

        [JsonPropertyName("rodada_id")]
        public int RodadaId { get; set; }

        [JsonPropertyName("clube_id")]
        public int ClubeId { get; set; }

        [JsonPropertyName("posicao_id")]
        public int PosicaoId { get; set; }

        [JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        [JsonPropertyName("pontos_num")]
        public decimal PontosNum { get; set; }

        [JsonPropertyName("preco_num")]
        public decimal PrecoNum { get; set; }

        [JsonPropertyName("variacao_num")]
        public decimal VariacaoNum { get; set; }

        [JsonPropertyName("media_num")]
        public decimal MediaNum { get; set; }

        [JsonPropertyName("jogos_num")]
        public int JogosNum { get; set; }

        [JsonPropertyName("scout")]
        public Scout Scout { get; set; }
    }
}