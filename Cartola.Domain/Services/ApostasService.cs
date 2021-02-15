using Cartola.Domain.Entities;
using Cartola.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cartola.Domain.Services
{
    public class ApostasService : IApostasService
    {
        private const decimal houseGain = 0.075m;
        private readonly decimal profitFactor;

        public ApostasService()
        {
            profitFactor = 1 - houseGain;
        }

        public List<Bet> GenerateBetsBook(decimal profit, decimal initialBet, decimal rate, bool overall = false)
        {
            var wallet = new Wallet()
            {
                BetSettings = new BetSettings(profit, initialBet, rate, overall, CalculateWinRate(rate))
            };

            wallet.Bets.Add(CalculateInitialBet(wallet));
            GenerateListBets(wallet);

            return wallet.Bets;
        }

        private Bet CalculateInitialBet(Wallet wallet)
        {
            var initialBet = wallet.BetSettings.InitialBet > 0 ?
                wallet.BetSettings.InitialBet :
                DoTheMath(wallet.BetSettings.Profit, wallet.BetSettings.Rate, 0);

            return new Bet(1, initialBet) { Aporte = initialBet, Chance = CalculateBetOdds(wallet.BetSettings.Odds, 1) };
        }

        private void GenerateListBets(Wallet wallet)
        {
            var loseAllOdds = double.MaxValue;

            while (loseAllOdds * 100 > 0.1)
            {
                wallet.Bets.Add(CalculateBets(new Bet(wallet.Bets.Last().Rodada + 1, wallet.Bets.Sum(x => x.Aporte)), wallet.BetSettings));

                loseAllOdds = wallet.Bets.Last().Chance;
            }
        }

        private Bet CalculateBets(Bet bet, BetSettings betSettings)
        {
            bet.Chance = CalculateBetOdds(betSettings.Odds, bet.Rodada);
            bet.Aporte = DoTheMath((betSettings.Overall ? 1 : bet.Rodada) * betSettings.Profit, betSettings.Rate, bet.SomatorioAporte);
            bet.SomatorioAporte += bet.Aporte;
            
            return bet;
        }

        private double CalculateBetOdds(decimal odds, int rodada)
        {
            return Math.Pow((1 - (double)odds), rodada);
        }

        private decimal DoTheMath(decimal targetProfit, decimal rate, decimal betsSum)
        {
            return Math.Round((targetProfit + betsSum) / ((decimal)rate - 1), 2);
        }

        private decimal CalculateWinRate(decimal rate)
        {
            return 1 / (rate / profitFactor);
        }
    }
}
