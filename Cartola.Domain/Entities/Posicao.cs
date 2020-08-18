using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    [Table("Posicao")]
    public class Posicao
    {
        [Key]
        [JsonIgnore]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("id")]
        public int PosicaoId { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("abreviacao")]
        public string Abreviacao { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;

        public Posicao UpdatePosicao(Posicao novaPosicao)
        {
            PosicaoId = novaPosicao.PosicaoId;
            Nome = novaPosicao.Nome;
            Abreviacao = novaPosicao.Abreviacao;
            DataModificacao = DateTime.Now;

            return this;
        }
    }
}