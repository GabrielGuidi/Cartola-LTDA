using Cartola.Domain.Entities;
using System.Collections.Generic;

namespace Cartola.Domain.Services.IServices
{
    public interface IApostasService
    {
        List<Bet> GenerateBetsBook(decimal lucroPorRodada, decimal initialBet, decimal fatorRetorno, bool overall = false);
    }
}
