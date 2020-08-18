using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    [Table("Rodada")]
    public class Rodada
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("rodada_id")]
        public int RodadaId { get; set; }

        [JsonPropertyName("inicio")]
        [NotMapped]
        public string DataInicioJson { get; set; }

        [JsonIgnore]
        public DateTime DataInicio { get; set; }

        [JsonPropertyName("fim")]
        [NotMapped]
        public string DataFimJson { get; set; }

        [JsonIgnore]
        public DateTime DataFim { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;

        public Rodada UpdateRodada(Rodada rodada)
        {
            RodadaId = rodada.RodadaId;
            DataInicio = rodada.DataInicio;
            DataFim = rodada.DataFim;
            DataModificacao = DateTime.Now;

            return this;
        }
    }
}
