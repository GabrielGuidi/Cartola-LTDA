using Cartola.Domain.Entities;
using Cartola.Domain.Services.IRepositories;
using Cartola.Domain.Services.IServices;
using Cartola.Domain.Services.Strategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cartola.Domain.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ICartolaRepository _cartolaRepository;
        private readonly IList<IBestPlayerStrategy> _strategies;

        private readonly IList<JogadorHistorico> _players;
        private readonly IList<JogadorHistorico> _homePlayers;
        private readonly IList<Partida> _listaPartidas;
        private readonly IList<EsquemaPosicoes> _esquemas;

        public AnalyticsService(ICartolaRepository cartolaRepository, IList<IBestPlayerStrategy> strategies)
        {
            _cartolaRepository = cartolaRepository;
            _strategies = strategies;

            _listaPartidas = _cartolaRepository.GetPartidas();
            _esquemas = _cartolaRepository.GetEsquemaPosicoes();
            _players = _cartolaRepository.GetJogadoresHistorico();
            _homePlayers = _players
                .Where(x => _listaPartidas.Any(y => y.Rodada == x.RodadaId + 1 && y.ClubeCasaId == x.ClubeId))
                .ToList();
        }

        public Analysis Analyze()
        {
            var analysis = new Analysis()
            {
                Results = new List<SimulateResults>(GetSimulateResults())
            };

            #region [Calculate stats]
            var modes = new List<Mode>();
            var partials = new List<Partial>();
            foreach (var result in analysis.Results)
            {
                modes.Add(new Mode()
                {
                    Type = result.Type,
                    Mean = result.Mean,
                    PriceMean = result.PriceMean,
                    TotalScore = result.TotalScore,
                    EsquemaId = result.EsquemaId,
                    EsquemaNome = result.EsquemaNome
                });

                partials.AddRange(CalculatePhase(5, result, "Each5Round"));
                partials.AddRange(CalculatePhase(18, result, "EachTurn", "Por turno"));
            }

            analysis.Stats = new Stats()
            {
                Modes = modes
                .OrderByDescending(x => x.Mean)
                .ToList(),

                Partials = partials
                .OrderBy(x => x.Type)
                .ThenBy(x => x.InitialRound)
                .ThenByDescending(x => x.Mean)
                .ToList()
            };
            #endregion

            return analysis;
        }

        private IEnumerable<SimulateResults> GetSimulateResults()
        {
            foreach (var esquema in _esquemas)
            {
                foreach (var strategy in _strategies)
                {
                    if (strategy.CalculateWithOnlyHomePlayers)
                        yield return GetSimulate(strategy.Name, strategy, esquema, _homePlayers);
                    else
                        yield return GetSimulate(strategy.Name, strategy, esquema);
                }
            }
        }

        public SimulateResults GetSimulate(
            string type, IBestPlayerStrategy strategy, EsquemaPosicoes esquemaPosicoes, IList<JogadorHistorico> jogadores = null)
        {
            var result = new SimulateResults()
            {
                Type = $"{type} [{esquemaPosicoes.Esquema.Nome}]",
                Results = CalculateResults(jogadores ?? _players, strategy, esquemaPosicoes)
                .ToList()
            };
            result.Mean = result.Results.Sum(x => x.Score) / result.Results.Count;
            result.TotalScore = result.Results.Sum(x => x.Score);
            result.PriceMean = result.Results.Sum(x => x.TeamPrice) / result.Results.Count(x => x.TeamPrice > 0);
            result.EsquemaId = esquemaPosicoes.EsquemaId;
            result.EsquemaNome = esquemaPosicoes.Esquema.Nome;

            return result;
        }

        public IEnumerable<Results> CalculateResults(
            IList<JogadorHistorico> jogadores, IBestPlayerStrategy strategy, EsquemaPosicoes esquemaPosicoes)
        {
            var maxRound = jogadores.Max(x => x.RodadaId);
            for (int round = 0; round < maxRound; round++)
            {
                if (jogadores.Any(x => x.RodadaId == round + 1))
                {
                    var team = GetTeam(round, jogadores, strategy, esquemaPosicoes);

                    if (strategy.AllowOpponents == false)
                    {
                        team = ReplaceOpponents(team, strategy, round, jogadores);
                    }

                    var result = new Results()
                    {
                        Round = round + 1,
                        Score = team.Sum(x => GetNextRoundScore(x)),
                        TeamPrice = team.Distinct().Sum(x => x.PrecoNum)
                    };

                    yield return result;
                }
            }
        }

        private IEnumerable<JogadorHistorico> ReplaceOpponents(IEnumerable<JogadorHistorico> team, IBestPlayerStrategy strategy, int round, IList<JogadorHistorico> jogadores)
        {
            var blockedPlayers = team.ToList();
            while (HaveOpponents(team))
            {
                team = ReplaceOneOpponents(team, strategy, round, jogadores, blockedPlayers);
            };

            return team;
        }

        private bool HaveOpponents(IEnumerable<JogadorHistorico> team)
        {
            foreach (var player in team)
            {
                if (PlayerHaveOpponents(player, team))
                    return true;
            }

            return false;
        }

        private bool PlayerHaveOpponents(JogadorHistorico player, IEnumerable<JogadorHistorico> team)
        {
            var opponent = _listaPartidas?.FirstOrDefault(x => x.ClubeCasaId == player.ClubeId && x.Rodada == player.RodadaId + 1)?.ClubeVisitanteId ??
                           _listaPartidas.FirstOrDefault(x => x.ClubeVisitanteId == player.ClubeId && x.Rodada == player.RodadaId + 1).ClubeCasaId;

            return team.Any(x => x.ClubeId == opponent);
        }

        private IEnumerable<JogadorHistorico> ReplaceOneOpponents(
            IEnumerable<JogadorHistorico> team, IBestPlayerStrategy strategy, int round,
            IList<JogadorHistorico> jogadores, IList<JogadorHistorico> blockedPlayers)
        {
            var worstPlayer = team
                .Where(x => PlayerHaveOpponents(x, team))
                .OrderBy(strategy.FunctionWorstPlayer)
                .ThenBy(x => x.JogosNum)
                .ThenBy(x => x.PrecoNum)
                .FirstOrDefault();

            var newPlayers = strategy.DoTheMagic(round, jogadores, 1, worstPlayer.PosicaoId, blockedPlayers).Single();
            team = team.Append(newPlayers);

            team = team
                .Where(x => x.JogadorHistoricoId != worstPlayer.JogadorHistoricoId)
                .OrderBy(x => x.PosicaoId)
                .ToList();

            if (team.Count() != 13)
                throw new Exception("Não deu bom ao retirar um jogador no método ReplaceOneOpponents!");

            blockedPlayers.Add(newPlayers);

            return team;
        }

        private IEnumerable<JogadorHistorico> GetTeam(int round, IList<JogadorHistorico> jogadores, IBestPlayerStrategy strategy, EsquemaPosicoes esquema)
        {
            return new List<JogadorHistorico>(strategy.DoTheMagic(round, jogadores, esquema.Goleiro, 1))
                .Concat(strategy.DoTheMagic(round, jogadores, esquema.Laterais, 2))
                .Concat(strategy.DoTheMagic(round, jogadores, esquema.Zagueiros, 3))
                .Concat(strategy.DoTheMagic(round, jogadores, esquema.Meias, 4))
                .Concat(strategy.DoTheMagic(round, jogadores, esquema.Atacantes, 5))
                .Concat(strategy.DoTheMagic(round, jogadores, esquema.Tecnico, 6))
                .Concat(strategy.DoTheMagic(round, jogadores, 1));
        }

        private decimal GetNextRoundScore(JogadorHistorico jogador)
        {
            var nextScore = _players
                .Where(x => x.JogadorId == jogador.JogadorId)?
                .FirstOrDefault(x => x.RodadaId == jogador.RodadaId + 1)?
                .PontosNum ?? 0m;

            return nextScore;
        }

        private IEnumerable<Partial> CalculatePhase(int size, SimulateResults result, string type, string phaseName = null)
        {
            var havePhaseName = string.IsNullOrWhiteSpace(phaseName) == false;

            for (int initialRound = 1; initialRound <= result.Results.Max(x => x.Round); initialRound += size)
            {
                var finalRound = initialRound + size - 1;
                var partials = result.Results.Where(x => x.Round >= initialRound && x.Round <= finalRound);

                yield return new Partial()
                {
                    Type = type,
                    InitialRound = initialRound,
                    FinalRound = finalRound,
                    Phase = $"{(havePhaseName ? phaseName + " - " : "")}Between round {initialRound} and {finalRound} ({result.Type}).",
                    Mean = partials.Sum(x => x.Score) / partials.Count(),
                    PriceMean = partials.Sum(x => x.TeamPrice) / partials.Count(x => x.TeamPrice > 0),
                    TotalScore = partials.Sum(x => x.Score),
                    EsquemaId = result.EsquemaId,
                    EsquemaNome = result.EsquemaNome
                };
            }
        }

        public object sql()
        {
            return null; /*TODO*/
        }
    }
}