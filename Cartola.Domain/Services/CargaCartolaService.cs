using Cartola.Domain.Entities;
using Cartola.Domain.Enuns;
using Cartola.Domain.Services.IRepositories;
using Cartola.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cartola.Domain.Services
{
    public class CargaCartolaService : ICargaCartolaService
    {
        private readonly ICargaCartolaRepository _cargaCartolaRepository;
        private readonly ICartolaService _cartolaService;

        public CargaCartolaService(ICargaCartolaRepository cargaCartolaRepository, ICartolaService cartolaService)
        {
            _cargaCartolaRepository = cargaCartolaRepository;
            _cartolaService = cartolaService;
        }

        public CartolaCargaResponse CargaClubes()
        {
            var listaClubes = _cargaCartolaRepository.GetClubesFromApi();

            var response = _cargaCartolaRepository.InsertClubes(listaClubes);

            return response;
        }
        public CartolaCargaResponse CargaPosicoes()
        {
            List<Posicao> listaPosicoes = _cargaCartolaRepository.GetPosicoesFromApi();

            var response = _cargaCartolaRepository.InsertPosicoes(listaPosicoes);

            return response;
        }

        public CartolaCargaResponse CargaEsquemas()
        {
            List<Esquema> listaEsquemas = _cargaCartolaRepository.GetEsquemasFromApi();

            var response = _cargaCartolaRepository.InsertEsquemas(listaEsquemas);

            return response;
        }

        public CartolaCargaResponse CargaStatus()
        {
            List<Status> listaStatus = _cargaCartolaRepository.GetStatusFromApi();

            var response = _cargaCartolaRepository.InsertStatus(listaStatus);

            return response;
        }

        public CartolaCargaResponse CargaRodada()
        {
            List<Rodada> listaRodadas = _cargaCartolaRepository.GetRodadaFromApi();

            foreach (var rodada in listaRodadas)
            {
                rodada.DataInicio = DateTime.Parse(rodada.DataInicioJson);
                rodada.DataFim = DateTime.Parse(rodada.DataFimJson);
            }

            var response = _cargaCartolaRepository.InsertRodada(listaRodadas);

            return response;
        }

        public CartolaCargaResponse CargaPartidas(int? rodada = null)
        {
            List<Partida> listaPartidas = _cargaCartolaRepository.GetPartidaFromApi(rodada);

            foreach (var partida in listaPartidas)
            {
                partida.DataPartida = DateTime.Parse(partida.DataPartidaJSON);
                ConverterAproveitamentos(partida);
                partida.ClubeVencedorId = ClubeVencedor(partida);
            }

            var response = _cargaCartolaRepository.InsertPartida(listaPartidas);

            return response;
        }

        public CartolaCargaResponse CargaJogadores()
        {
            List<Jogador> listaJogadores = _cargaCartolaRepository.GetJogadoresFromApi();

            foreach (var jogador in listaJogadores)
            {
                if (jogador.ScoutAtual != null)
                {
                    jogador.ScoutAtual.Origem = OrigemEnum.Jogador;
                    jogador.ScoutAtual.JogadorId = jogador.JogadorId;
                    jogador.ScoutAtual.RodadaId = jogador.RodadaId;
                }
            }

            var response = _cargaCartolaRepository.InsertJogadores(listaJogadores);

            return response;
        }

        public CartolaCargaResponse CargaParciais(int? rodada = null, bool consolidar = false)
        {
            List<PontuacaoParcial> listaPontuacaoParcial = _cargaCartolaRepository.GetPontuacaoParcialFromApi(rodada);

            if (!listaPontuacaoParcial.Any())
                return new CartolaCargaResponse() { Mensagem = "Menhum dado encontrado na origem!" };

            foreach (var parcial in listaPontuacaoParcial)
            {
                if (parcial.Scout != null)
                {
                    parcial.Scout.Origem = OrigemEnum.Pontuacao;
                    parcial.Scout.JogadorId = parcial.JogadorId;
                }
            }

            var response = _cargaCartolaRepository.InsertPontuacaoParcial(listaPontuacaoParcial, consolidar);

            return response;
        }

        public CartolaCargaResponse ConsolidarRodada(int? rodada = 22)
        {
            var response = new CartolaCargaResponse();
            var isRodadaAtual = rodada == null;

            if (isRodadaAtual)
            {
                var responseJogadores = CargaJogadores();
                response.Mensagem += $"Tabela Jogadores: inserts => {responseJogadores.QuantidadeInserts}, updates => {responseJogadores.QuantidadeUpdates}\n{(string.IsNullOrWhiteSpace(responseJogadores.Mensagem) ? "" : responseJogadores.Mensagem + "\n")}";
            }

            var rodadaConsolidar = rodada ?? _cartolaService.GetUltimaRodadaSemConsolidar();

            var responsePartidas = CargaPartidas(rodadaConsolidar);
            response.Mensagem += $"Tabela Partidas: inserts => {responsePartidas.QuantidadeInserts}, updates => {responsePartidas.QuantidadeUpdates}\n{(string.IsNullOrWhiteSpace(responsePartidas.Mensagem) ? "" : responsePartidas.Mensagem + "\n")}";

            var responseParciais = CargaParciais(rodadaConsolidar, true);
            response.Mensagem += $"Tabela PontuacaoParcial: inserts => {responseParciais.QuantidadeInserts}, updates => {responseParciais.QuantidadeUpdates}\n{(string.IsNullOrWhiteSpace(responseParciais.Mensagem) ? "" : responseParciais.Mensagem + "\n")}";


            var responseGerais = _cargaCartolaRepository.ConsolidarRegistrosRestantes(rodadaConsolidar);
            response.Mensagem += $"Tabelas gerais: inserts => {responseGerais.QuantidadeInserts}, updates => {responseGerais.QuantidadeUpdates}\n{(string.IsNullOrWhiteSpace(responseGerais.Mensagem) ? "" : responseGerais.Mensagem + "\n")}";

            return response;
        }

        #region [Tools]
        private void ConverterAproveitamentos(Partida partida)
        {
            for (int i = 0; i < partida.AproveitamentoMandanteJson.Count - 1; i++)
                partida.AproveitamentoMandante += string.IsNullOrWhiteSpace(partida.AproveitamentoMandanteJson[i]) ? "0" : partida.AproveitamentoMandanteJson[i] + ";";
            partida.AproveitamentoMandante += string.IsNullOrWhiteSpace(partida.AproveitamentoMandanteJson[^1]) ? "0" : partida.AproveitamentoMandanteJson[^1];

            for (int i = 0; i < partida.AproveitamentoVisitanteJson.Count - 1; i++)
                partida.AproveitamentoVisitante += string.IsNullOrWhiteSpace(partida.AproveitamentoVisitanteJson[i]) ? "0" : partida.AproveitamentoVisitanteJson[i] + ";";
            partida.AproveitamentoVisitante += string.IsNullOrWhiteSpace(partida.AproveitamentoVisitanteJson[^1]) ? "0" : partida.AproveitamentoVisitanteJson[^1];
        }

        private int? ClubeVencedor(Partida partida)
        {
            if (partida.PlacarOficialMandante == null || partida.PlacarOficialVisitante == null)
                return null;

            if (partida.PlacarOficialMandante == partida.PlacarOficialVisitante)
                return null;

            if (partida.PlacarOficialMandante > partida.PlacarOficialVisitante)
                return partida.ClubeCasaId;

            return partida.ClubeVisitanteId;
        }
        #endregion
    }
}
