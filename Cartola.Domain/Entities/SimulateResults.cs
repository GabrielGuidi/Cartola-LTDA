using System.Collections.Generic;

namespace Cartola.Domain.Entities
{
    public class Analysis
    {
        public Stats Stats { get; set; }
        public IList<SimulateResults> Results { get; set; }
    }

    public class SimulateResults
    {
        public string Type { get; set; }
        public IList<Results> Results { get; set; }
        public decimal Mean { get; set; }
        public decimal PriceMean { get; set; }
        public decimal TotalScore { get; set; }
        public int EsquemaId { get; set; }
        public string EsquemaNome { get; set; }
        public List<Stats> Stats { get; set; }
    }

    public class Stats
    {
        public List<Mode> Modes { get; set; }
        public List<Partial> Partials { get; set; }
    }

    public class Partial
    {
        public string Type { get; set; }
        public int InitialRound { get; set; }
        public int FinalRound { get; set; }
        public string Phase { get; set; }
        public decimal Mean { get; set; }
        public decimal PriceMean { get; set; }
        public decimal TotalScore { get; set; }
        public int EsquemaId { get; set; }
        public string EsquemaNome { get; set; }
    }

    public class Mode
    {
        public string Type { get; set; }
        public decimal Mean { get; set; }
        public decimal PriceMean { get; set; }
        public decimal TotalScore { get; set; }
        public int EsquemaId { get; set; }
        public string EsquemaNome { get; set; }
    }

    public class Results
    {
        public int Round { get; set; }
        public decimal Score { get; set; }
        public decimal TeamPrice { get; set; }

    }
}
