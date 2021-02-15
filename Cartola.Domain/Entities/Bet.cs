using System.Collections.Generic;

namespace Cartola.Domain.Entities
{
    public class Wallet
    {
        public Wallet()
        {
            Bets = new List<Bet>();
        }

        public List<Bet> Bets { get; set; }
        public BetSettings BetSettings { get; set; }
    }

    public class Bet
    {
        public Bet(int rodada, decimal somatorioAporte)
        {
            Rodada = rodada;
            SomatorioAporte = somatorioAporte;
        }

        public int Rodada { get; set; }
        public decimal Aporte { get; set; }
        public decimal SomatorioAporte { get; set; }
        public double Chance { get; set; }

    }

    public class BetSettings
    {
        public BetSettings(decimal profit, decimal initialBet, decimal rate, bool overall, decimal odd)
        {
            Profit = profit;
            InitialBet = initialBet;
            Rate = rate;
            Overall = overall;
            Odds = odd;
        }

        public decimal Profit { get; set; }
        public decimal InitialBet { get; set; }
        public decimal Rate { get; set; }
        public bool Overall { get; set; }
        public decimal Odds { get; set; }
    }
}
