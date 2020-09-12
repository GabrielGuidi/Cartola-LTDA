using Cartola.Domain.Entities;
using System.Collections.Generic;

namespace Cartola.Domain.Services.IRepositories
{
    public interface ICargaCartolaRepository
    {
        List<Clube> GetClubesFromApi();
        List<Posicao> GetPosicoesFromApi();
        List<Esquema> GetEsquemasFromApi();
        List<Status> GetStatusFromApi();
        List<PontuacaoParcial> GetPontuacaoParcialFromApi(int? rodada);
        List<Rodada> GetRodadaFromApi();
        List<Partida> GetPartidaFromApi(int? rodada);
        List<Jogador> GetJogadoresFromApi();

        CartolaCargaResponse InsertClubes(List<Clube> listaClubesAPI);
        CartolaCargaResponse InsertPosicoes(List<Posicao> listaPosicoes);
        CartolaCargaResponse InsertEsquemas(List<Esquema> listaEsquemas);
        CartolaCargaResponse InsertStatus(List<Status> listaStatus);
        CartolaCargaResponse InsertRodada(List<Rodada> listaRodada);
        CartolaCargaResponse InsertPartida(List<Partida> listaPartidas);
        CartolaCargaResponse InsertJogadores(List<Jogador> listaJogadores);
        CartolaCargaResponse InsertPontuacaoParcial(List<PontuacaoParcial> listaPontuacaoParcial, bool consolidar = false);
        CartolaCargaResponse ConsolidarRegistrosRestantes(int rodadaConsolidar);
        StatusMercado GetStatusMercadoFromApi();
    }
}
