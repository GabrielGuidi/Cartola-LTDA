using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    [Table("Status")]
    public class Status
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("id")]
        public int StatusId { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;

        public Status UpdateStatus(Status status)
        {
            StatusId = status.StatusId;
            Nome = status.Nome;
            DataModificacao = DateTime.Now;
            
            return this;
        }
    }
}
