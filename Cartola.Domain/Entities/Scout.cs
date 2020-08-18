using Cartola.Domain.Enuns;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    [Table("Scout")]
    public class Scout
    {
        [Key]
        [JsonIgnore]
        public int ScoutId { get; set; }


        [JsonPropertyName("G")]
        public int Gol { get; set; }

        [JsonPropertyName("A")]
        public int Assistencia { get; set; }

        [JsonPropertyName("FT")]
        public int FinalizacaoNaTrave { get; set; }

        [JsonPropertyName("FD")]
        public int FinalizacaoDefendida { get; set; }

        [JsonPropertyName("FF")]
        public int FinalizacaoParaFora { get; set; }

        [JsonPropertyName("FS")]
        public int FaltaSofrida { get; set; }

        [JsonPropertyName("PP")]
        public int PenaltiPerdido { get; set; }

        [JsonPropertyName("I")]
        public int Impedimento { get; set; }

        [JsonPropertyName("PI")]
        public int PasseIncompleto { get; set; }

        [JsonPropertyName("DP")]
        public int DefesaDePenalti { get; set; }

        [JsonPropertyName("SG")]
        public int JogoSemSofrerGols { get; set; }

        [JsonPropertyName("DD")]
        public int DefesaDificil { get; set; }

        [JsonPropertyName("DS")]
        public int Desarme { get; set; }

        [JsonPropertyName("GC")]
        public int GolContra { get; set; }

        [JsonPropertyName("CV")]
        public int CartaoVermelho { get; set; }

        [JsonPropertyName("CA")]
        public int CartaoAmarelo { get; set; }

        [JsonPropertyName("GS")]
        public int GolSofrido { get; set; }

        [JsonPropertyName("FC")]
        public int FaltaCometida { get; set; }


        [JsonIgnore]
        public bool Consolidado { get; set; }

        [JsonIgnore]
        public DateTime DataModificacao { get; set; } = DateTime.Now;

        [JsonIgnore]
        public OrigemEnum Origem { get; set; }


        #region [Relationship]
        [JsonIgnore]
        public int? JogadorId { get; set; }

        public Jogador Jogador { get; set; }

        [JsonIgnore]
        public int RodadaId { get; set; }

        [JsonIgnore]
        public Rodada Rodada { get; set; }
        #endregion

        public Scout UpdateScout(Scout scout)
        {
            if (scout == null)
                return this;

            if (scout.Consolidado)
                return this;

            Gol = scout.Gol;
            Assistencia = scout.Assistencia;
            FinalizacaoNaTrave = scout.FinalizacaoNaTrave;
            FinalizacaoDefendida = scout.FinalizacaoDefendida;
            FinalizacaoParaFora = scout.FinalizacaoParaFora;
            FaltaSofrida = scout.FaltaSofrida;
            PenaltiPerdido = scout.PenaltiPerdido;
            Impedimento = scout.Impedimento;
            PasseIncompleto = scout.PasseIncompleto;
            DefesaDePenalti = scout.DefesaDePenalti;
            JogoSemSofrerGols = scout.JogoSemSofrerGols;
            DefesaDificil = scout.DefesaDificil;
            Desarme = scout.Desarme;
            GolContra = scout.GolContra;
            CartaoVermelho = scout.CartaoVermelho;
            CartaoAmarelo = scout.CartaoAmarelo;
            GolSofrido = scout.GolSofrido;
            FaltaCometida = scout.FaltaCometida;

            JogadorId = scout.JogadorId;
            RodadaId = scout.RodadaId;
            DataModificacao = DateTime.Now;

            return this;
        }

        internal Scout Clone(OrigemEnum origem)
        {
            var response = new Scout().UpdateScout(this);
            response.Origem = origem;

            return response;
        }

        internal bool Comparar(Scout scout)
        {
            if (scout == null)
                return false;

            if (JogadorId == scout.JogadorId &&
                RodadaId == scout.RodadaId &&
                Origem == scout.Origem &&
                Gol == scout.Gol &&
                Assistencia == scout.Assistencia &&
                FinalizacaoNaTrave == scout.FinalizacaoNaTrave &&
                FinalizacaoDefendida == scout.FinalizacaoDefendida &&
                FinalizacaoParaFora == scout.FinalizacaoParaFora &&
                FaltaSofrida == scout.FaltaSofrida &&
                PenaltiPerdido == scout.PenaltiPerdido &&
                Impedimento == scout.Impedimento &&
                PasseIncompleto == scout.PasseIncompleto &&
                DefesaDePenalti == scout.DefesaDePenalti &&
                JogoSemSofrerGols == scout.JogoSemSofrerGols &&
                DefesaDificil == scout.DefesaDificil &&
                Desarme == scout.Desarme &&
                GolContra == scout.GolContra &&
                CartaoVermelho == scout.CartaoVermelho &&
                CartaoAmarelo == scout.CartaoAmarelo &&
                GolSofrido == scout.GolSofrido &&
                FaltaCometida == scout.FaltaCometida)
                return true;

            return false;
        }
    }
}
