using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    [Table("Clube")]
    public class Clube
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("id")]
        public int ClubeId { get; set; }

        [MaxLength(64)]
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [MaxLength(16)]
        [JsonPropertyName("abreviacao")]
        public string Abreviacao { get; set; }

        [JsonPropertyName("posicao")]
        public int? Posicao { get; set; }

        [MaxLength(64)]
        [JsonPropertyName("nome_fantasia")]
        public string NomeFantasia { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;


        #region [Navigation]
        [JsonPropertyName("escudos")]
        public Escudos Escudos { get; set; }

        [JsonIgnore]
        public List<Partida> PartidasMandante { get; set; }

        [JsonIgnore]
        public List<Partida> PartidasVisitante { get; set; }

        [JsonIgnore]
        public List<Jogador> Jogadores { get; set; }
        #endregion

        public Clube UpdateClube(Clube clube)
        {
            ClubeId = clube.ClubeId;
            Nome = clube.Nome;
            Abreviacao = clube.Abreviacao;
            Posicao = clube.Posicao;
            Escudos = Escudos != null ? Escudos.UpdateEscudo(clube.Escudos) : clube.Escudos;
            NomeFantasia = clube.NomeFantasia;
            DataModificacao = DateTime.Now;

            return this;
        }
    }
}
