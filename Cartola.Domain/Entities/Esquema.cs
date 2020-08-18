using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    [Table("Esquema")]
    public class Esquema
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("esquema_id")]
        public int EsquemaId { get; set; }

        [MaxLength(16)]
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;


        #region [Navigation]
        [JsonPropertyName("posicoes")]
        public EsquemaPosicoes Posicoes { get; set; }
        #endregion

        public Esquema UpdateEsquema(Esquema esquema)
        {
            EsquemaId = esquema.EsquemaId;
            Nome = esquema.Nome;
            Posicoes = esquema.Posicoes != null ? Posicoes.UpdateEsquemaPosicoes(esquema.Posicoes) : esquema.Posicoes;
            DataModificacao = DateTime.Now;

            return this;
        }
    }

    [Table("EsquemaPosicoes")]
    public class EsquemaPosicoes
    {
        [Key]
        [JsonIgnore]
        public int PosicoesEscalacaoId { get; set; }

        [JsonPropertyName("gol")]
        public int Goleiro { get; set; }

        [JsonPropertyName("lat")]
        public int Laterais { get; set; }

        [JsonPropertyName("zag")]
        public int Zagueiros { get; set; }

        [JsonPropertyName("mei")]
        public int Meias { get; set; }

        [JsonPropertyName("ata")]
        public int Atacantes { get; set; }

        [JsonPropertyName("tec")]
        public int Tecnico { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;


        #region [Relationship]
        [JsonIgnore]
        public int EsquemaId { get; set; }

        [JsonIgnore]
        public Esquema Esquema { get; set; }
        #endregion

        public EsquemaPosicoes UpdateEsquemaPosicoes(EsquemaPosicoes posicoes)
        {
            Goleiro = posicoes.Goleiro;
            Laterais = posicoes.Laterais;
            Zagueiros = posicoes.Zagueiros;
            Meias = posicoes.Meias;
            Atacantes = posicoes.Atacantes;
            Tecnico = posicoes.Tecnico;
            DataModificacao = DateTime.Now;

            return this;
        }
    }
}
