using Cartola.Domain.Entities;

namespace Cartola.Domain.Services.IServices
{
    public interface ICargaCartolaService
    {
        CartolaCargaResponse CargaClubes();
        CartolaCargaResponse CargaPosicoes();
        CartolaCargaResponse CargaEsquemas();
        CartolaCargaResponse CargaStatus();
        CartolaCargaResponse CargaRodada();
        CartolaCargaResponse CargaPartidas(int? rodada = null);
        CartolaCargaResponse CargaJogadores();
        CartolaCargaResponse CargaParciais(int? rodada = null, bool consolidar = false);
        CartolaCargaResponse ConsolidarRodada(int? rodada = null);
    }
}
