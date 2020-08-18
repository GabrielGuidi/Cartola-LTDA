using Cartola.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    [Table("Jogador")]
    public class Jogador
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("atleta_id")]
        public int JogadorId { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [MaxLength(64)]
        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [MaxLength(64)]
        [JsonPropertyName("apelido")]
        public string Apelido { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("foto")]
        public string Foto { get; set; }

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


        [JsonIgnore]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;


        #region [Relationship]
        [JsonPropertyName("rodada_id")]
        public int RodadaId { get; set; }

        [JsonIgnore]
        public Rodada Rodada { get; set; }

        [JsonPropertyName("clube_id")]
        public int ClubeId { get; set; }

        [JsonIgnore]
        public Clube Clube { get; set; }

        [JsonPropertyName("posicao_id")]
        public int PosicaoId { get; set; }

        [JsonIgnore]
        public Posicao Posicao { get; set; }

        [JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        [JsonIgnore]
        public Status Status { get; set; }

        [JsonIgnore]
        public int? ScoutAtualId { get; set; }

        [JsonPropertyName("scout")]
        public Scout ScoutAtual { get; set; }
        #endregion


        #region [Navigation]
        [JsonIgnore]
        public List<JogadorHistorico> JogadoresHistorico { get; set; }

        [JsonIgnore]
        public List<Scout> HistoricoScouts { get; set; }

        [JsonIgnore]
        public List<PontuacaoParcial> HistoricoParciais { get; set; }
        #endregion

        public Jogador UpdateJogador(Jogador jogador)
        {
            Nome = jogador.Nome;
            Slug = jogador.Slug;
            Apelido = jogador.Apelido;
            Foto = jogador.Foto;
            JogadorId = jogador.JogadorId;
            RodadaId = jogador.RodadaId;
            ClubeId = jogador.ClubeId;
            PosicaoId = jogador.PosicaoId;
            StatusId = jogador.StatusId;
            PontosNum = jogador.PontosNum;
            PrecoNum = jogador.PrecoNum;
            VariacaoNum = jogador.VariacaoNum;
            MediaNum = jogador.MediaNum;
            JogosNum = jogador.JogosNum;
            ScoutAtual = ScoutAtual != null ? ScoutAtual.UpdateScout(jogador.ScoutAtual) : jogador.ScoutAtual;
            DataModificacao = DateTime.Now;

            return this;
        }
    }

    [Table("JogadorHistorico")]
    public class JogadorHistorico
    {
        [Key]
        public int JogadorHistoricoId { get; set; }

        public decimal PontosNum { get; set; }

        public decimal PrecoNum { get; set; }

        public decimal VariacaoNum { get; set; }

        public decimal MediaNum { get; set; }

        public int JogosNum { get; set; }


        [JsonIgnore]
        public bool Consolidado { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;


        #region [Relationship]
        public int JogadorId { get; set; }

        public Jogador Jogador { get; set; }

        public int RodadaId { get; set; }

        public Rodada Rodada { get; set; }

        public int ClubeId { get; set; }

        public Clube Clube { get; set; }

        public int PosicaoId { get; set; }

        public Posicao Posicao { get; set; }

        public int StatusId { get; set; }

        public Status Status { get; set; }

        public int? ScoutId { get; set; }

        public Scout Scout { get; set; }
        #endregion

        public JogadorHistorico UpdateJogadorHistorico(JogadorHistorico jogadorHistorico)/*TODO*/
        {
            PontosNum = jogadorHistorico.PontosNum;
            PrecoNum = jogadorHistorico.PrecoNum;
            VariacaoNum = jogadorHistorico.VariacaoNum;
            MediaNum = jogadorHistorico.MediaNum;
            JogosNum = jogadorHistorico.JogosNum;
            ClubeId = jogadorHistorico.ClubeId;
            PosicaoId = jogadorHistorico.PosicaoId;
            StatusId = jogadorHistorico.StatusId;
            Scout = Scout != null ? Scout.UpdateScout(jogadorHistorico.Scout) : jogadorHistorico.Scout;
            DataModificacao = DateTime.Now;

            return this;
        }

        public static explicit operator JogadorHistorico(Jogador jogador)
        {
            var response = new JogadorHistorico
            {
                PontosNum = jogador.PontosNum,
                PrecoNum = jogador.PrecoNum,
                VariacaoNum = jogador.VariacaoNum,
                MediaNum = jogador.MediaNum,
                JogosNum = jogador.JogosNum,

                JogadorId = jogador.JogadorId,
                RodadaId = jogador.RodadaId,
                ClubeId = jogador.ClubeId,
                PosicaoId = jogador.PosicaoId,
                StatusId = jogador.StatusId,
                Scout = jogador.ScoutAtual?.Clone(OrigemEnum.JogadorHistorico),
            };

            return response;
        }

        public void UpdateJogadorHistorico(PontuacaoParcial parcial)
        {
            PontosNum = parcial.Pontuacao;
            DataModificacao = DateTime.Now;
        }

        public bool Comparar(PontuacaoParcial parcial)
        {
            if (parcial == null)
                return false;

            if (PontosNum == parcial.Pontuacao)
                return true;

            return false;
        }
    }
}
