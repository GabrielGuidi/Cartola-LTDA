using Cartola.Domain.Entities;
using Cartola.Domain.Enuns;
using Cartola.Domain.Services.IRepositories;
using Cartola.Infra.Models;
using Cartola.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Cartola.Infra.Repositories
{
    public class CargaCartolaRepository : ICargaCartolaRepository
    {
        private readonly IHttpClientCartolaApi _httpClientCartolaApi;
        private readonly CartolaDBContext _cartolaDBContext;

        private readonly string _clubes = "https://api.cartolafc.globo.com/clubes";
        private readonly string _atletas_pontuados = "https://api.cartolafc.globo.com/atletas/pontuados/";
        private readonly string _esquemas = "https://api.cartolafc.globo.com/esquemas";
        private readonly string _atletas_mercado = "https://api.cartolafc.globo.com/atletas/mercado";
        private readonly string _rodadas = "https://api.cartolafc.globo.com/rodadas";
        private readonly string _partidas = "https://api.cartolafc.globo.com/partidas/";

        #region [links]
        /*
         * private readonly string _atletas_mercado = "https://api.cartolafc.globo.com/atletas/mercado";
         * private readonly string _esquemas = "https://api.cartolafc.globo.com/esquemas";
         * private readonly string _clubes = "https://api.cartolafc.globo.com/clubes";
         * private readonly string _mercado_status = "https://api.cartolafc.globo.com/mercado/status";
         * private readonly string _partidas = "https://api.cartolafc.globo.com/partidas";
         * private readonly string _auth_time = "https://api.cartolafc.globo.com/auth/time";
         * private readonly string _auth_time_salvar = "https://api.cartolafc.globo.com/auth/time/salvar";
         
         * atletas_parciais: "//api.cartolafc.globo.com/atletas/pontuados/1"
         * busca_times: "//api.cartolafc.globo.com/times?q="
         * mercado_destaques: "//api.cartolafc.globo.com/mercado/destaques"
         * partidas: "//api.cartolafc.globo.com/partidas/{rodada}"
         * posrodada_destaques: "//api.cartolafc.globo.com/pos-rodada/destaques"
         * rodadas: "//api.cartolafc.globo.com/rodadas"
         * time_adv: "//api.cartolafc.globo.com/time/slug/{slug}/{rodada}", // opcionalmente aceita a rodada
         * time_id: "//api.cartolafc.globo.com/time/id/{id}/{rodada}", // opcionalmente aceita a rodada
        */

        /*
         * auth: "//api.cartolafc.globo.com/auth/time/info"
         * check_slug_time: "//api.cartolafc.globo.com/logged/time/?search="
         * check_slug_liga: "//api.cartolafc.globo.com/logged/liga/?search="
         * historico_transacoes: "//api.cartolafc.globo.com/auth/time/historico/"
         * performance_time: "//api.cartolafc.globo.com/auth/stats/historico"         
         * noticias: "//api.cartolafc.globo.com/auth/noticias"
         * performance_atletas: "//api.cartolafc.globo.com/logged/stats/atletas"
         * atleta_pontuacao: "//api.cartolafc.globo.com/auth/mercado/atleta/{idAtleta}/pontuacao"
         * 
         * São Paulo e Goiás não jogaram a primeira rodada.
        */
        #endregion

        public CargaCartolaRepository(IHttpClientCartolaApi httpClientCartolaApi, CartolaDBContext cartolaDBContext)
        {
            _httpClientCartolaApi = httpClientCartolaApi;
            _cartolaDBContext = cartolaDBContext;
        }

        #region [Data from api]
        public List<Clube> GetClubesFromApi()
        {
            var data = _httpClientCartolaApi.Request<Dictionary<string, Clube>>(_clubes);

            var response = data.Select(x => x.Value).ToList();

            return response;
        }

        public List<Posicao> GetPosicoesFromApi()
        {
            var data = _httpClientCartolaApi.Request<AtletasPontuadosJson>(_atletas_pontuados);

            var response = data.Posicoes
                .Select(x => x.Value)
                .ToList();

            return response;
        }

        public List<Esquema> GetEsquemasFromApi()
        {
            var response = _httpClientCartolaApi.Request<List<Esquema>>(_esquemas);

            return response;
        }

        public List<Status> GetStatusFromApi()
        {
            var data = _httpClientCartolaApi.Request<AtletasMercadoJson>(_atletas_mercado);

            var response = data.Status
                .Select(x => x.Value)
                .ToList();

            return response;
        }

        public List<Rodada> GetRodadaFromApi()
        {
            var response = _httpClientCartolaApi.Request<List<Rodada>>(_rodadas);

            return response;
        }

        public List<Partida> GetPartidaFromApi(int? rodada = null)
        {
            var data = _httpClientCartolaApi.Request<PartidasJson>(_partidas + rodada ?? "");

            foreach (var row in data.Partidas)
            {
                row.Rodada = data.RodadaAtual;
            }

            return data.Partidas;
        }

        public List<Jogador> GetJogadoresFromApi()
        {
            var data = _httpClientCartolaApi.Request<AtletasMercadoJson>(_atletas_mercado);

            return data.Atletas;
        }

        public List<PontuacaoParcial> GetPontuacaoParcialFromApi(int? rodada = null)
        {
            var data = _httpClientCartolaApi.Request<PontuacaoParcialJson>(_atletas_pontuados + rodada ?? "");

            foreach (var parcial in data.Atletas)
            {
                parcial.Value.JogadorId = int.Parse(parcial.Key);
                parcial.Value.RodadaId = data.Rodada;

                if (parcial.Value.Scout != null)
                {
                    parcial.Value.Scout.RodadaId = data.Rodada;
                }
            }

            var response = data.Atletas.Select(x => x.Value).ToList();

            return response;
        }
        #endregion

        #region [Inserts]
        public CartolaCargaResponse InsertClubes(List<Clube> clubes)
        {
            var apiResponse = new CartolaCargaResponse();
            try
            {
                using var transaction = _cartolaDBContext.Database.BeginTransaction();
                var db = _cartolaDBContext.Clube.Include(b => b.Escudos).ToList();

                foreach (var clube in clubes)
                {
                    if (db.Any(x => x.ClubeId == clube.ClubeId))
                    {
                        Clube clubeUpdate = db.First(x => x.ClubeId == clube.ClubeId).UpdateClube(clube);
                        _cartolaDBContext.Update(clubeUpdate);
                        apiResponse.QuantidadeUpdates++;
                    }
                    else
                    {
                        _cartolaDBContext.Add(clube);
                        apiResponse.QuantidadeInserts++;
                    }
                }

                _cartolaDBContext.SaveChanges();
                transaction.Commit();

                return apiResponse;
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }
        }

        public CartolaCargaResponse InsertPosicoes(List<Posicao> listaPosicoes)
        {
            var apiResponse = new CartolaCargaResponse();
            try
            {
                using var transaction = _cartolaDBContext.Database.BeginTransaction();
                var db = _cartolaDBContext.Posicao.ToList();

                foreach (var posicao in listaPosicoes)
                {
                    if (db.Any(x => x.PosicaoId == posicao.PosicaoId))
                    {
                        Posicao posicaoUpdate = db.First(x => x.PosicaoId == posicao.PosicaoId).UpdatePosicao(posicao);
                        _cartolaDBContext.Update(posicaoUpdate);
                        apiResponse.QuantidadeUpdates++;
                    }
                    else
                    {
                        _cartolaDBContext.Add(posicao);
                        apiResponse.QuantidadeInserts++;
                    }
                }

                _cartolaDBContext.SaveChanges();
                transaction.Commit();

                return apiResponse;
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }
        }


        public CartolaCargaResponse InsertEsquemas(List<Esquema> listaEsquemas)
        {
            var apiResponse = new CartolaCargaResponse();
            try
            {
                using var transaction = _cartolaDBContext.Database.BeginTransaction();
                var db = _cartolaDBContext.Esquema.Include(b => b.Posicoes).ToList();

                foreach (var esquema in listaEsquemas)
                {
                    if (db.Any(x => x.EsquemaId == esquema.EsquemaId))
                    {
                        Esquema esquemaUpdate = db.First(x => x.EsquemaId == esquema.EsquemaId).UpdateEsquema(esquema);
                        _cartolaDBContext.Update(esquemaUpdate);
                        apiResponse.QuantidadeUpdates++;
                    }
                    else
                    {
                        _cartolaDBContext.Add(esquema);
                        apiResponse.QuantidadeInserts++;
                    }
                }

                _cartolaDBContext.SaveChanges();
                transaction.Commit();

                return apiResponse;
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }
        }

        public CartolaCargaResponse InsertStatus(List<Status> listaStatus)
        {
            var apiResponse = new CartolaCargaResponse();
            try
            {
                using var transaction = _cartolaDBContext.Database.BeginTransaction();
                var db = _cartolaDBContext.Status.ToList();

                foreach (var status in listaStatus)
                {
                    if (db.Any(x => x.StatusId == status.StatusId))
                    {
                        Status statusUpdate = db.First(x => x.StatusId == status.StatusId).UpdateStatus(status);
                        _cartolaDBContext.Update(statusUpdate);
                        apiResponse.QuantidadeUpdates++;
                    }
                    else
                    {
                        _cartolaDBContext.Add(status);
                        apiResponse.QuantidadeInserts++;
                    }
                }

                _cartolaDBContext.SaveChanges();
                transaction.Commit();

                return apiResponse;
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }
        }

        public CartolaCargaResponse InsertRodada(List<Rodada> listaRodada)
        {
            var apiResponse = new CartolaCargaResponse();
            try
            {
                using var transaction = _cartolaDBContext.Database.BeginTransaction();
                var db = _cartolaDBContext.Rodada.ToList();

                foreach (var rodada in listaRodada)
                {
                    if (db.Any(x => x.RodadaId == rodada.RodadaId))
                    {
                        Rodada rodadaUpdate = db.First(x => x.RodadaId == rodada.RodadaId).UpdateRodada(rodada);
                        _cartolaDBContext.Update(rodadaUpdate);
                        apiResponse.QuantidadeUpdates++;
                    }
                    else
                    {
                        _cartolaDBContext.Add(rodada);
                        apiResponse.QuantidadeInserts++;
                    }
                }

                _cartolaDBContext.SaveChanges();
                transaction.Commit();

                return apiResponse;
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }
        }

        public CartolaCargaResponse InsertPartida(List<Partida> listaPartidas)
        {
            var apiResponse = new CartolaCargaResponse();
            try
            {
                using var transaction = _cartolaDBContext.Database.BeginTransaction();
                var db = _cartolaDBContext.Partida.Include(b => b.Transmissao).ToList();

                foreach (var partida in listaPartidas)
                {
                    if (db.Any(x => x.PartidaId == partida.PartidaId))
                    {
                        Partida partidaUpdate = db.First(x => x.PartidaId == partida.PartidaId).UpdatePartida(partida);
                        _cartolaDBContext.Update(partidaUpdate);
                        apiResponse.QuantidadeUpdates++;
                    }
                    else
                    {
                        _cartolaDBContext.Add(partida);
                        apiResponse.QuantidadeInserts++;
                    }
                }

                _cartolaDBContext.SaveChanges();
                transaction.Commit();

                return apiResponse;
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }
        }

        public CartolaCargaResponse InsertJogadores(List<Jogador> listaJogadores)
        {
            var apiResponse = new CartolaCargaResponse();
            try
            {
                using var scope = new TransactionScope(TransactionScopeOption.Required,
                                                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

                var db = _cartolaDBContext.Jogador.Include(b => b.ScoutAtual).ToList();

                foreach (var jogador in listaJogadores)
                {
                    if (db.Any(x => x.JogadorId == jogador.JogadorId))
                    {
                        Jogador jogadorUpdate = db.First(x => x.JogadorId == jogador.JogadorId).UpdateJogador(jogador);
                        _cartolaDBContext.Update(jogadorUpdate);
                        apiResponse.QuantidadeUpdates++;
                    }
                    else
                    {
                        _cartolaDBContext.Add(jogador);
                        apiResponse.QuantidadeInserts++;
                    }
                }

                _cartolaDBContext.SaveChanges();
                scope.Complete();
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }

            var responseJogadoresHistoricos = InsertJogadoresHistorico(listaJogadores);

            if (string.IsNullOrWhiteSpace(responseJogadoresHistoricos.Errors))
            {
                apiResponse.Mensagem = $"Tabela JogadoresHistorico: inserts => {responseJogadoresHistoricos.QuantidadeInserts}, updates => {responseJogadoresHistoricos.QuantidadeUpdates}";
            }

            return apiResponse;
        }

        public CartolaCargaResponse InsertJogadoresHistorico(List<Jogador> listaJogadores, int? rodada = null)
        {
            var apiResponse = new CartolaCargaResponse();
            try
            {
                if (rodada == null)
                    rodada = listaJogadores.First(x => x.RodadaId >= 0).RodadaId;

                using var scope = new TransactionScope();
                var db = _cartolaDBContext.JogadorHistorico.Include(b => b.Scout).Where(x => x.RodadaId == rodada).ToList();

                foreach (var jogador in listaJogadores)
                {
                    JogadorHistorico jogadorHistorio = (JogadorHistorico)jogador;

                    if (db.Any(x => x.JogadorId == jogadorHistorio.JogadorId))
                    {
                        JogadorHistorico jogadorHistoricoUpdate = db.First(x => x.JogadorId == jogadorHistorio.JogadorId);
                        if (!jogadorHistoricoUpdate.Consolidado)
                        {
                            jogadorHistoricoUpdate = jogadorHistoricoUpdate.UpdateJogadorHistorico(jogadorHistorio);
                            _cartolaDBContext.Update(jogadorHistoricoUpdate);
                            apiResponse.QuantidadeUpdates++;
                        }
                    }
                    else
                    {
                        _cartolaDBContext.Add(jogadorHistorio);
                        apiResponse.QuantidadeInserts++;
                    }

                }

                _cartolaDBContext.SaveChanges();
                scope.Complete();

                return apiResponse;
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }
        }

        public CartolaCargaResponse InsertPontuacaoParcial(List<PontuacaoParcial> listaPontuacaoParcial, bool consolidar = false)
        {
            var apiResponse = new CartolaCargaResponse();

            using var transaction = _cartolaDBContext.Database.BeginTransaction();
            try
            {
                var rodadaAtual = listaPontuacaoParcial.First(x => x.RodadaId > 0).RodadaId;

                var db = _cartolaDBContext.PontuacaoParcial.Include(b => b.Scout).Where(x => x.RodadaId == rodadaAtual).ToList();

                foreach (var parcial in listaPontuacaoParcial)
                {
                    if (db.Any(x => x.JogadorId == parcial.JogadorId))
                    {
                        PontuacaoParcial pontuacaoParcialUpdate = db.First(x => x.JogadorId == parcial.JogadorId);

                        if (!pontuacaoParcialUpdate.Consolidado && !pontuacaoParcialUpdate.Comparar(parcial))
                        {
                            pontuacaoParcialUpdate = pontuacaoParcialUpdate.UpdatePontuacaoParcial(parcial);

                            if (consolidar)
                                Consolidar(pontuacaoParcialUpdate);

                            _cartolaDBContext.Update(pontuacaoParcialUpdate);
                            apiResponse.QuantidadeUpdates++;
                        }
                        else if (!pontuacaoParcialUpdate.Consolidado && consolidar)
                        {
                            Consolidar(pontuacaoParcialUpdate);
                            _cartolaDBContext.Update(pontuacaoParcialUpdate);
                            apiResponse.QuantidadeUpdates++;
                        }
                    }
                    else
                    {
                        if (consolidar)
                            Consolidar(parcial);

                        _cartolaDBContext.Add(parcial);

                        apiResponse.QuantidadeInserts++;
                    }
                }

                _cartolaDBContext.SaveChanges();
                transaction.Commit();

                CartolaCargaResponse updadeResponse = UpdateJogadoresHistorico(listaPontuacaoParcial, rodadaAtual, consolidar);

                apiResponse.Mensagem = $"Tabela JogadoresHistorico: updates => {updadeResponse.QuantidadeUpdates}";

                return apiResponse;
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }
        }

        private CartolaCargaResponse UpdateJogadoresHistorico(List<PontuacaoParcial> listaPontuacaoParcial, int rodadaAtual, bool consolidar = false)
        {
            var apiResponse = new CartolaCargaResponse();

            using var transaction = _cartolaDBContext.Database.BeginTransaction();
            try
            {
                var db = _cartolaDBContext.JogadorHistorico.Where(x => x.RodadaId == rodadaAtual).ToList();

                foreach (var parcial in listaPontuacaoParcial)
                {
                    var jogadorHistorico = db.FirstOrDefault(x => x.JogadorId == parcial.JogadorId && x.RodadaId == parcial.RodadaId);

                    if (jogadorHistorico != null && !jogadorHistorico.Consolidado && !jogadorHistorico.Comparar(parcial))
                    {
                        jogadorHistorico.UpdateJogadorHistorico(parcial);

                        if (consolidar)
                            Consolidar(jogadorHistorico);

                        _cartolaDBContext.Update(jogadorHistorico);
                        apiResponse.QuantidadeUpdates++;
                    }
                    else if (!jogadorHistorico.Consolidado && consolidar)
                    {
                        Consolidar(jogadorHistorico);
                        _cartolaDBContext.Update(jogadorHistorico);
                        apiResponse.QuantidadeUpdates++;
                    }
                }

                _cartolaDBContext.SaveChanges();
                transaction.Commit();

                return apiResponse;
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }
        }
        #endregion

        #region [Tools]
        private void Consolidar(PontuacaoParcial pontuacaoParcialUpdate)
        {
            pontuacaoParcialUpdate.Consolidado = true;

            if (pontuacaoParcialUpdate.Scout != null)
                pontuacaoParcialUpdate.Scout.Consolidado = true;
        }

        private void Consolidar(JogadorHistorico jogadorHistorico)
        {
            jogadorHistorico.Consolidado = true;

            if (jogadorHistorico.Scout != null)
                jogadorHistorico.Scout.Consolidado = true;
        }

        public CartolaCargaResponse ConsolidarRegistrosRestantes(int rodadaConsolidar)
        {
            var apiResponse = new CartolaCargaResponse();
            var Origens = new List<int>() { (int)OrigemEnum.JogadorHistorico, (int)OrigemEnum.Pontuacao };

            using var transaction = _cartolaDBContext.Database.BeginTransaction();
            try
            {
                var registroJogadorHistorico = _cartolaDBContext.JogadorHistorico
                    .Where(x => x.RodadaId == rodadaConsolidar && x.Consolidado == false)
                    .ToList();

                var registroScout = _cartolaDBContext.Scout
                    .Where(x => x.RodadaId == rodadaConsolidar && x.Consolidado == false && Origens.Contains((int)x.Origem))
                    .ToList();

                var registroPontuacaoParcial = _cartolaDBContext.PontuacaoParcial
                    .Where(x => x.RodadaId == rodadaConsolidar && x.Consolidado == false)
                    .ToList();

                foreach (var jogador in registroJogadorHistorico)
                {
                    jogador.Consolidado = true;
                    _cartolaDBContext.Update(jogador);
                    apiResponse.QuantidadeUpdates++;
                }

                foreach (var scout in registroScout)
                {
                    scout.Consolidado = true;
                    _cartolaDBContext.Update(scout);
                    apiResponse.QuantidadeUpdates++;
                }

                foreach (var parcial in registroPontuacaoParcial)
                {
                    parcial.Consolidado = true;
                    _cartolaDBContext.Update(parcial);
                    apiResponse.QuantidadeUpdates++;
                }

                _cartolaDBContext.SaveChanges();
                transaction.Commit();

                return apiResponse;
            }
            catch (Exception erro)
            {
                apiResponse.Errors = erro.Message;
                return apiResponse;
            }
        }
        #endregion
    }
}
