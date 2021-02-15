namespace Cartola.Domain.Entities
{
    public class MagicNumber
    {
        public int PosicaoId { get; set; }
        public string Pelo { get; set; }
        public int ClubeId { get; set; }
        public bool EmCasa { get; set; }
        public string Contra { get; set; }
        public int AdversarioId { get; set; }
        public decimal Desarme { get; set; }
        public decimal DesarmeAdversario { get; set; }
        public decimal DefesaDificil { get; set; }
        public decimal DefesaDificilAdversario { get; set; }
        public decimal SaldoDeGol { get; set; }
        public decimal SaldoDeGolAdversario { get; set; }
        public decimal Gol { get; set; }
        public decimal GolAdversario { get; set; }
        public decimal Assistencia { get; set; }
        public decimal AssistenciaAdversario { get; set; }
        public decimal Finalizacoes { get; set; }
        public decimal FinalizacoesAdversario { get; set; }
        public decimal PontosCedidos { get; set; }
        public decimal PontosCedidosAdversario { get; set; }
        public decimal MediaDesarmes { get; set; }
        public decimal MediaDesarmesAdversario { get; set; }
        public decimal MediaPontos { get; set; }
        public decimal MediaPontosAdversario { get; set; }
    }
}