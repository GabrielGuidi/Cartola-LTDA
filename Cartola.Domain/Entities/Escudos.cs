using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    [Table("Escudos")]
    public class Escudos
    {
        [Key]
        [JsonIgnore]
        public int EscudosId { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("60x60")]
        public string Grande { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("45x45")]
        public string Medio { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("30x30")]
        public string Pequeno { get; set; }

        
        #region [Relationship]
        [JsonIgnore]
        public int ClubeId { get; set; }

        [JsonIgnore]
        public Clube Clube { get; set; }
        #endregion

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;

        public Escudos UpdateEscudo(Escudos escudo)
        {
            Grande = escudo.Grande;
            Medio = escudo.Medio;
            Pequeno = escudo.Pequeno;
            DataModificacao = DateTime.Now;

            return this;
        }
    }
}