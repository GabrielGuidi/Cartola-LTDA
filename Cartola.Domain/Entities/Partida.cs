using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    [Table("Partida")]
    public class Partida
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("partida_id")]
        public int PartidaId { get; set; }

        [JsonIgnore]
        public int Rodada { get; set; }

        [JsonPropertyName("clube_casa_posicao")]
        public int ClubeCasaPosicao { get; set; }

        [NotMapped]
        [JsonPropertyName("aproveitamento_mandante")]
        public List<string> AproveitamentoMandanteJson { get; set; }

        [MaxLength(16)]
        [JsonIgnore]
        public string AproveitamentoMandante { get; set; }

        [NotMapped]
        [JsonPropertyName("aproveitamento_visitante")]
        public List<string> AproveitamentoVisitanteJson { get; set; }

        [MaxLength(16)]
        [JsonIgnore]
        public string AproveitamentoVisitante { get; set; }

        [JsonPropertyName("clube_visitante_posicao")]
        public int ClubeVisitantePosicao { get; set; }

        [JsonPropertyName("partida_data")]
        [NotMapped]
        public string DataPartidaJSON { get; set; }

        [JsonIgnore]
        public DateTime DataPartida { get; set; }

        [MaxLength(64)]
        [JsonPropertyName("local")]
        public string LocalPartida { get; set; }

        [JsonPropertyName("valida")]
        public bool PartidaValida { get; set; }

        [JsonPropertyName("placar_oficial_mandante")]
        public int? PlacarOficialMandante { get; set; }

        [JsonPropertyName("placar_oficial_visitante")]
        public int? PlacarOficialVisitante { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("url_confronto")]
        public string UrlConfronto { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("url_transmissao")]
        public string UrlTransmissao { get; set; }

        [JsonPropertyName("transmissao")]
        public Transmissao Transmissao { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;

        
        #region [Relationship]
        [JsonIgnore]
        public int? ClubeVencedorId { get; set; }

        [JsonIgnore]
        public Clube ClubeVencedor { get; set; }

        [JsonPropertyName("clube_casa_id")]
        public int ClubeCasaId { get; set; }

        [JsonIgnore]
        public Clube ClubeCasa { get; set; }

        [JsonPropertyName("clube_visitante_id")]
        public int ClubeVisitanteId { get; set; }

        [JsonIgnore]
        public Clube ClubeVisitante { get; set; }
        #endregion


        #region [Navigation]
        #endregion

        public Partida UpdatePartida(Partida partida)
        {
            PartidaId = partida.PartidaId;
            Rodada = partida.Rodada;
            ClubeVencedorId = partida.ClubeVencedorId;
            ClubeCasaId = partida.ClubeCasaId;
            ClubeCasa = partida.ClubeCasa;
            ClubeCasaPosicao = partida.ClubeCasaPosicao;
            ClubeVisitanteId = partida.ClubeVisitanteId;
            ClubeVisitante = partida.ClubeVisitante;
            AproveitamentoMandante = partida.AproveitamentoMandante;
            AproveitamentoVisitante = partida.AproveitamentoVisitante;
            ClubeVisitantePosicao = partida.ClubeVisitantePosicao;
            DataPartida = partida.DataPartida;
            LocalPartida = partida.LocalPartida;
            PartidaValida = partida.PartidaValida;
            PlacarOficialMandante = partida.PlacarOficialMandante;
            PlacarOficialVisitante = partida.PlacarOficialVisitante;
            UrlConfronto = partida.UrlConfronto;
            UrlTransmissao = partida.UrlTransmissao;
            Transmissao = Transmissao != null ? Transmissao.UpdateTransmissao(partida.Transmissao) : partida.Transmissao;
            DataModificacao = DateTime.Now;

            return this;
        }
    }

    [Table("Transmissao")]
    public class Transmissao
    {
        [Key]
        [JsonIgnore]
        public int TransmissaoId { get; set; }

        [MaxLength(128)]
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;


        #region [Relationship]
        [JsonIgnore]
        public int PartidaId { get; set; }

        [JsonIgnore]
        public Partida Partida { get; set; }
        #endregion

        public Transmissao UpdateTransmissao(Transmissao transmissao)
        {
            Label = transmissao.Label;
            Url = transmissao.Url;
            DataModificacao = DateTime.Now;

            return this;
        }
    }
}
