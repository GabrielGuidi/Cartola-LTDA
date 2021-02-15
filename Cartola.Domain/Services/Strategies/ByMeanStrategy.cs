﻿using Cartola.Domain.Entities;
using Cartola.Domain.Services.Strategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cartola.Domain.Services.Strategies
{
    public class ByMeanStrategy : IBestPlayerStrategy
    {
        public bool CalculateWithOnlyHomePlayers => false;
        public bool AllowOpponents => true;
        public Func<JogadorHistorico, decimal> FunctionWorstPlayer => x => x.MediaNum;
        public string Name => "By mean";

        public IEnumerable<JogadorHistorico> DoTheMagic(
            int round, IEnumerable<JogadorHistorico> players, int quantity,
            int? position = null, IEnumerable<JogadorHistorico> blockedPlayers = null)
        {
            return players
                .Where(x => x.RodadaId == round)
                .Where(x => x.StatusId == 7)
                .Where(x => position is null || x.PosicaoId == position)
                .Where(x => blockedPlayers is null || blockedPlayers.Contains(x) == false)
                .OrderByDescending(x => x.MediaNum)
                .ThenByDescending(x => x.JogosNum)
                .ThenByDescending(x => x.PrecoNum)
                .Take(quantity);
        }
    }
}
