using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    [Table("PontuacaoParcial")]
    public class PontuacaoParcial
    {
        [Key]
        [JsonIgnore]
        public int PontuacaoParcialId { get; set; }

        [MaxLength(64)]
        [JsonPropertyName("apelido")]
        public string Apelido { get; set; }

        [JsonPropertyName("pontuacao")]
        public decimal Pontuacao { get; set; }


        #region [Relationship]
        [JsonIgnore]
        public int JogadorId { get; set; }

        [JsonIgnore]
        public Jogador Jogador { get; set; }

        [JsonIgnore]
        public int RodadaId { get; set; }

        [JsonIgnore]
        public Rodada Rodada { get; set; }

        [JsonIgnore]
        public int? ScoutId { get; set; }

        [JsonPropertyName("scout")]
        public Scout Scout { get; set; }
        #endregion


        [JsonIgnore]
        public bool Consolidado { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;


        public PontuacaoParcial UpdatePontuacaoParcial(PontuacaoParcial pontuacaoParcial)
        {
            Apelido = pontuacaoParcial.Apelido;
            Pontuacao = pontuacaoParcial.Pontuacao;
            JogadorId = pontuacaoParcial.JogadorId;
            RodadaId = pontuacaoParcial.RodadaId;
            Scout = Scout != null ? Scout.UpdateScout(pontuacaoParcial.Scout) : pontuacaoParcial.Scout;
            DataModificacao = DateTime.Now;

            return this;
        }

        public bool Comparar(PontuacaoParcial parcial)
        {
            if (Apelido == parcial.Apelido &&
                Pontuacao == parcial.Pontuacao &&
                JogadorId == parcial.JogadorId &&
                RodadaId == parcial.RodadaId &&
                (Scout == parcial.Scout || (Scout?.Comparar(parcial.Scout) ?? false)))
                return true;

            return false;
        }
    }
}
