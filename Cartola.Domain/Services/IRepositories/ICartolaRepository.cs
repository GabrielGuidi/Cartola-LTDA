using Cartola.Domain.Entities;
using System.Collections.Generic;

namespace Cartola.Domain.Services.IRepositories
{
    public interface ICartolaRepository
    {
        List<Jogador> GetJogadoresMercado();
        List<PontuacaoParcial> GetParciais(int? rodada = null);

        List<Clube> Clubes { get; }
        List<Posicao> Posicoes { get; }
        List<Jogador> Jogadores { get; }
        List<Esquema> Esquemas { get; }

        List<JogadorHistorico> GetJogadoresHistoricoSemConsolidar();
    }
}