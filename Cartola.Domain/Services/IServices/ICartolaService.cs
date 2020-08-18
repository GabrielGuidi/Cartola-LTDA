using Cartola.Domain.Entities;
using System.Collections.Generic;

namespace Cartola.Domain.Services.IServices
{
    public interface ICartolaService
    {
        List<Jogador> GetJogadoresMercado();
        List<PontuacaoParcial> GetParciais();
        
        List<Jogador> GetJogadores();
        List<Clube> GetClubes();
        List<Posicao> GetPosicoes();
        List<Esquema> GetEsquemas();
        
        string FormatarFotoJogador(string link);
    }
}