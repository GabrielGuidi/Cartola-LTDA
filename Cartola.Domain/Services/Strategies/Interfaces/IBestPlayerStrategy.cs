using Cartola.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Cartola.Domain.Services.Strategies.Interfaces
{
    public interface IBestPlayerStrategy
    {
        public bool CalculateWithOnlyHomePlayers { get; }
        public bool AllowOpponents { get; }
        public Func<JogadorHistorico, decimal> FunctionWorstPlayer { get; }
        public string Name { get; }
        public IEnumerable<JogadorHistorico> DoTheMagic(
            int round, IEnumerable<JogadorHistorico> players, int quantity, 
            int? position = null, IEnumerable<JogadorHistorico> blockedPlayers = null);
    }
}
