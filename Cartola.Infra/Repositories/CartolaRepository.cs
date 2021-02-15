using Cartola.Domain.Entities;
using Cartola.Domain.Services.IRepositories;
using Cartola.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Cartola.Infra.Repositories
{
    public class CartolaRepository : ICartolaRepository
    {
        private readonly IHttpClientCartolaApi _httpClientCartolaApi;
        private readonly CartolaDBContext _cartolaDBContext;
        private readonly ICartolaDapperRepository _cartolaDapperRepository;

        public CartolaRepository(IHttpClientCartolaApi clientFactory, CartolaDBContext cartolaDBContext, ICartolaDapperRepository cartolaDapperRepository)
        {
            _httpClientCartolaApi = clientFactory;
            _cartolaDBContext = cartolaDBContext;
            _cartolaDapperRepository = cartolaDapperRepository;

            _clubes = new Lazy<List<Clube>>(() => GetClubes());
            _posicoes = new Lazy<List<Posicao>>(() => GetPosicoes());
            _esquemas = new Lazy<List<Esquema>>(() => GetEsquemas());
            _jogadores = new Lazy<List<Jogador>>(() => GetJogadores());
        }


        #region [Endpoints]
        private readonly string _auth_time_salvar = "https://api.cartolafc.globo.com/auth/time/salvar";
        #endregion


        #region [Propriedades]
        public List<Clube> Clubes { get => _clubes.Value; }
        private readonly Lazy<List<Clube>> _clubes;

        public List<Posicao> Posicoes { get => _posicoes.Value; }
        private readonly Lazy<List<Posicao>> _posicoes;

        public List<Esquema> Esquemas { get => _esquemas.Value; }
        private readonly Lazy<List<Esquema>> _esquemas;

        public List<Jogador> Jogadores { get => _jogadores.Value; }
        private readonly Lazy<List<Jogador>> _jogadores;
        #endregion


        #region [Objetos repository]
        public List<Clube> GetClubes()
        {
            return _cartolaDBContext.Clube
                .Where(x => x.Posicao != null)
                .ToList();
        }

        public List<Posicao> GetPosicoes()
        {
            return _cartolaDBContext.Posicao
                .ToList();
        }

        public List<Esquema> GetEsquemas()
        {
            return _cartolaDBContext.Esquema
                .ToList();
        }

        private List<Jogador> GetJogadores()
        {
            return _cartolaDBContext.Jogador
                .ToList();
        }
        #endregion

        public List<Jogador> GetJogadoresMercado()
        {
            return _cartolaDBContext.Jogador
                .Include(x => x.Clube)
                .Include(x => x.Posicao)
                .Include(x => x.ScoutAtual)
                .Include(x => x.Status)
                .ToList();
        }

        public List<PontuacaoParcial> GetParciais(int? rodada = null)
        {
            rodada = GetRodadaAtual();

            return _cartolaDBContext.PontuacaoParcial
                .Include(x => x.Jogador)
                .Include(x => x.Jogador.Clube)
                .Include(x => x.Scout)
                .Include(x => x.Jogador.Posicao)
                .Where(x => x.RodadaId == rodada)
                .ToList();
        }

        private int GetRodadaAtual()
        {
            return _cartolaDBContext.PontuacaoParcial
                .Max(x => x.RodadaId);
        }

        public bool Escalar(Escalacao escalacao)
        {
            var json = JsonSerializer.Serialize(escalacao);
            using var escalacaoJson = new StringContent(json, Encoding.UTF8, "application/json");

            var result = _httpClientCartolaApi.Request<List<dynamic>>(_auth_time_salvar, HttpMethod.Post, true, escalacaoJson);

            return result != null;
        }

        public List<JogadorHistorico> GetJogadoresHistoricoSemConsolidar()
        {
            return _cartolaDBContext.JogadorHistorico
                .Where(x => !x.Consolidado)
                .ToList();
        }

        public List<JogadorHistorico> GetJogadoresHistorico()
        {
            return _cartolaDBContext.JogadorHistorico
                .AsNoTracking()
                .Include(x => x.Jogador)
                .Include(x => x.Rodada)
                .ToList();
        }

        public List<Partida> GetPartidas()
        {
            return _cartolaDBContext.Partida
                .AsNoTracking()
                .ToList();
        }

        public List<EsquemaPosicoes> GetEsquemaPosicoes()
        {
            return _cartolaDBContext.PosicoesEscalacao
                .AsNoTracking()
                .Include(x => x.Esquema)
                .ToList();
        }

        public List<MagicNumber> GetQueryStats(int round, int position, int range = 10)
        {
            var initialRound = round - range;
            var sql = @$"
            SELECT CONTRA.Posição                                                   AS PosicaoId,
                   AFAVOR.Pelo                                                      AS Pelo,
                   AFAVOR.ClubeId                                                   AS ClubeId,
                   AFAVOR.Jogando                                                   AS 'EmCasa',
                   CONTRA.Contra                                                    AS Contra,
                   CONTRA.AdversarioId                                              AS AdversarioId,
                   CAST((AFAVOR.Desarme)AS VARCHAR(4))                              AS 'Desarme',
                   CAST((CONTRA.Desarme)AS VARCHAR(4))                              AS 'DesarmeAdversario',
                   CAST((AFAVOR.DefesaDificil)AS VARCHAR(4))                        AS 'DefesaDificil',
                   CAST((CONTRA.DefesaDificil) AS VARCHAR(4))                       AS 'DefesaDificilAdversario',
                   CAST((AFAVOR.SaldoDeGol)AS VARCHAR(4))                           AS 'SaldoDeGol',
                   CAST((CONTRA.SaldoDeGol)AS VARCHAR(4))                           AS 'SaldoDeGolAdversario',
                   CAST((AFAVOR.Gol)AS VARCHAR(4))                                  AS 'Gol',
                   CAST((CONTRA.Gol) AS VARCHAR(4))                                 AS 'GolAdversario',
                   CAST((AFAVOR.Assistencia)AS VARCHAR(4))                          AS 'Assistencia',
                   CAST((CONTRA.Assistencia)AS VARCHAR(4))                          AS 'AssistenciaAdversario',
                   CAST((AFAVOR.Finalizações)AS VARCHAR(4))                         AS 'Finalizacoes',
                   CAST((CONTRA.Finalizações)AS VARCHAR(4))                         AS 'FinalizacoesAdversario',
                   CAST((ROUND(AFAVOR.PontosCedidos, 8)) AS VARCHAR(16))            AS 'PontosCedidos',
                   CAST((ROUND(CONTRA.PontosCedidos, 8)) AS VARCHAR(16))            AS 'PontosCedidosAdversario',
                   CAST((LEFT(ROUND(AFAVOR.MediaDesarmes, 2), 4)) AS VARCHAR(16))   AS 'MediaDesarmes',
                   CAST((LEFT(ROUND(CONTRA.MediaDesarmes, 2), 4)) AS VARCHAR(16))   AS 'MediaDesarmesAdversario',
                   CAST((LEFT(ROUND(AFAVOR.MediaPontos, 2), 4)) AS VARCHAR(16))     AS 'MediaPontos',
                   CAST((LEFT(ROUND(CONTRA.MediaPontos, 2), 4)) AS VARCHAR(16))     AS 'MediaPontosAdversario'
              FROM(SELECT *,
                           TC.PontosCedidos / TC.QuantidadeJogadores AS              MediaPontos,
                           (CAST(TC.Desarme AS DECIMAL) / TC.QuantidadeJogadores) AS MediaDesarmes
                      FROM(SELECT IIF(pd.ClubeCasaId = rodada.ClubeCasaId, rodadaFora.ClubeId, rodadaCasa.ClubeId) AS 'ChaveId',
                                   ps.PosicaoId AS                                                                     Posição,
                                   IIF(jh.ClubeId = casa.ClubeId, fora.Nome, casa.Nome) AS                             Contra,
                                   IIF(jh.ClubeId = casa.ClubeId, fora.ClubeId, casa.ClubeId) AS                       AdversarioId,
                                   IIF(jh.ClubeId = casa.ClubeId, 'FORA', 'CASA') AS                                   Jogando,
                                   IIF(pd.ClubeCasaId = rodada.ClubeCasaId, rodadaFora.Nome, rodadaCasa.Nome) AS       'Hoje contra',
                                   COUNT(1) AS                                                                         QuantidadeJogadores,
                                   SUM(sc.Desarme) AS                                                                  Desarme,
                                   SUM(sc.DefesaDificil) AS                                                            DefesaDificil,
                                   SUM(sc.JogoSemSofrerGols) AS                                                        SaldoDeGol,
                                   SUM(sc.Gol) AS                                                                      Gol,
                                   SUM(sc.Assistencia) AS                                                              Assistencia,
                                   SUM(sc.FinalizacaoDefendida + sc.FinalizacaoNaTrave + sc.FinalizacaoParaFora) AS    Finalizações,
                                   SUM(jh.PontosNum) AS                                                                PontosCedidos
                              FROM dbo.Scout AS sc
                                   JOIN dbo.JogadorHistorico AS jh ON jh.JogadorId = sc.JogadorId
                                                                      AND jh.RodadaId = sc.RodadaId
                                   JOIN dbo.Jogador AS jj ON jj.JogadorId = jh.JogadorId
                                   JOIN dbo.Posicao AS ps ON jh.PosicaoId = ps.PosicaoId
                                   JOIN dbo.Partida AS pd ON pd.Rodada = jh.RodadaId
                                                             AND(pd.ClubeCasaId = jh.ClubeId
                                                                  OR pd.ClubeVisitanteId = jh.ClubeId)
                                   JOIN dbo.Clube AS casa ON casa.ClubeId = pd.ClubeCasaId
                                   JOIN dbo.Clube AS fora ON fora.ClubeId = pd.ClubeVisitanteId
                                   JOIN Partida AS rodada ON(rodada.ClubeCasaId = IIF(jh.ClubeId = casa.ClubeId, fora.ClubeId, casa
                                   .ClubeId)
                                                             AND IIF(jh.ClubeId = casa.ClubeId, 'FORA', 'CASA') = 'CASA')
                                                            OR(rodada.ClubeVisitanteId = IIF(jh.ClubeId = casa.ClubeId, fora.
                                                            ClubeId, casa.ClubeId)
                                                                AND IIF(jh.ClubeId = casa.ClubeId, 'FORA', 'CASA') = 'FORA')
                                   JOIN dbo.Clube AS rodadaCasa ON rodadaCasa.ClubeId = rodada.ClubeCasaId
                                   JOIN dbo.Clube AS rodadaFora ON rodadaFora.ClubeId = rodada.ClubeVisitanteId
                             WHERE(sc.Origem = 3
                                   OR jh.PosicaoId = 6)
                                  AND jh.PosicaoId = {position}
                                  AND rodada.Rodada = {round}
                                  AND JH.RodadaId BETWEEN {initialRound} AND {round - 1 }

                                  AND rodada.PartidaValida = 1
                             GROUP BY IIF(pd.ClubeCasaId = rodada.ClubeCasaId, rodadaFora.ClubeId, rodadaCasa.ClubeId),
                                      ps.PosicaoId,
                                      IIF(jh.ClubeId = casa.ClubeId, fora.Nome, casa.Nome),
                                      IIF(jh.ClubeId = casa.ClubeId, fora.ClubeId, casa.ClubeId),
                                      IIF(jh.ClubeId = casa.ClubeId, 'FORA', 'CASA'),
                                      IIF(pd.ClubeCasaId = rodada.ClubeCasaId, rodadaFora.Nome, rodadaCasa.Nome)) AS TC) AS CONTRA
                   JOIN(SELECT *,
                                TC.PontosCedidos / TC.QuantidadeJogadores AS              MediaPontos,
                                (CAST(TC.Desarme AS DECIMAL) / TC.QuantidadeJogadores) AS MediaDesarmes
                           FROM(SELECT IIF(jh.ClubeId = casa.ClubeId, casa.ClubeId, fora.ClubeId) AS             'ChaveId',
                                        ps.Nome AS                                                                Posição,
                                        IIF(jh.ClubeId = casa.ClubeId, casa.Nome, fora.Nome) AS                   Pelo,
                                        IIF(jh.ClubeId = casa.ClubeId, casa.ClubeId, fora.ClubeId) AS             ClubeId,
                                        IIF(jh.ClubeId = casa.ClubeId, 1, 0) AS                         Jogando,
                                        IIF(jh.ClubeId = rodada.ClubeCasaId, rodadaFora.Nome, rodadaCasa.Nome) AS 'Hoje contra',
                                        COUNT(1) AS                                                               QuantidadeJogadores,
                                        SUM(sc.Desarme) AS                                                        Desarme,
                                        SUM(sc.DefesaDificil) AS                                                  DefesaDificil,
                                        SUM(sc.JogoSemSofrerGols) AS                                              SaldoDeGol,
                                        SUM(sc.Gol) AS                                                            Gol,
                                        SUM(sc.Assistencia) AS                                                    Assistencia,
                                        SUM(sc.FinalizacaoDefendida + sc.FinalizacaoNaTrave + sc.FinalizacaoParaFora) AS
                                                                                                                  Finalizações,
                                        SUM(jh.PontosNum) AS                                                      PontosCedidos
                                   FROM dbo.Scout AS sc
                                        JOIN dbo.JogadorHistorico AS jh ON jh.JogadorId = sc.JogadorId
                                                                           AND jh.RodadaId = sc.RodadaId
                                        JOIN dbo.Jogador AS jj ON jj.JogadorId = jh.JogadorId
                                        JOIN dbo.Posicao AS ps ON jh.PosicaoId = ps.PosicaoId
                                        JOIN dbo.Partida AS pd ON pd.Rodada = jh.RodadaId
                                                                  AND(pd.ClubeCasaId = jh.ClubeId
                                                                       OR pd.ClubeVisitanteId = jh.ClubeId)
                                        JOIN dbo.Clube AS casa ON casa.ClubeId = pd.ClubeCasaId
                                        JOIN dbo.Clube AS fora ON fora.ClubeId = pd.ClubeVisitanteId
                                        JOIN Partida AS rodada ON(rodada.ClubeCasaId = jh.ClubeId
                                                                  AND jh.ClubeId = casa.ClubeId)
                                                                 OR(rodada.ClubeVisitanteId = jh.ClubeId
                                                                     AND jh.ClubeId <> casa.ClubeId)
                                        JOIN dbo.Clube AS rodadaCasa ON rodadaCasa.ClubeId = rodada.ClubeCasaId
                                        JOIN dbo.Clube AS rodadaFora ON rodadaFora.ClubeId = rodada.ClubeVisitanteId
                                  WHERE(sc.Origem = 3
                                        OR jh.PosicaoId = 6)
                                       AND jh.PosicaoId = {position}
                                       AND rodada.Rodada = {round}
                                       AND JH.RodadaId BETWEEN {initialRound} AND {round - 1}

                                       AND rodada.PartidaValida = 1
                                  GROUP BY IIF(jh.ClubeId = casa.ClubeId, casa.ClubeId, fora.ClubeId),
                                           ps.Nome,
                                           IIF(jh.ClubeId = casa.ClubeId, casa.Nome, fora.Nome),
                                           IIF(jh.ClubeId = casa.ClubeId, casa.ClubeId, fora.ClubeId),
                                           IIF(jh.ClubeId = casa.ClubeId, 1, 0),
                                           IIF(jh.ClubeId = rodada.ClubeCasaId, rodadaFora.Nome, rodadaCasa.Nome)) AS TC) AS AFAVOR
                                           ON AFAVOR.ChaveId = CONTRA.ChaveId;
            ";

            var result = _cartolaDapperRepository.GetAll<MagicNumber>(sql, null, commandType: CommandType.Text);

            return result;
        }

        public List<RoundNumber> GetRoundStats(int round)
        {
            var sql = @$"
            SELECT ABS(pr.ClubeCasaPosicao - pr.ClubeVisitantePosicao) AS        Ranking,
                   TC.MediaPonderada AS                                          MediaPonderada,
                   TC.MediaPreco AS                                              MediaPreco,
                   TC.MediaPontos AS                                             MediaPontos,
                   TC.Nome AS                                                    'CASA',

                   TC.ClubeId AS                                                 ClubeId,
                   pr.ClubeCasaPosicao AS                                        'Classificacao',
                   pr.ClubeVisitantePosicao AS                                   'ClassificacaoAdversario',
                   TF.Nome AS                                                    'FORA',
	               TF.ClubeId AS                                                 Adversario,
                   TF.MediaPontos AS                                             MediaPontos,
                   TF.MediaPreco AS                                              MediaPreco,
                   TF.MediaPonderada AS                                          MediaPonderada,
                   ABS(TC.MediaPontos - TF.MediaPontos) AS RankingMediaPontos,
                   ABS(TC.MediaPreco - TF.MediaPreco) AS RankingMediaPreco,
                   ABS(TC.MediaPonderada - TF.MediaPonderada) AS RankingMediaPonderada
              FROM dbo.Partida AS pr
                   JOIN(SELECT *,
                                PT01.TotalMedia / PT01.Quantidade AS                             MediaPontos,
                                PT01.TotalPreco / PT01.Quantidade AS                             MediaPreco,
                                (PT01.TotalMedia / PT01.Quantidade + (3 * (PT01.TotalPreco / PT01.Quantidade))) / 4 AS
                                                                                                 MediaPonderada
                           FROM(SELECT cl.ClubeId,
                                        cl.Nome,
                                        IIF(partidaCasa.ClubeCasaId IS NOT NULL, partidaCasa.ClubeCasaPosicao, partidaFora.
                                        ClubeVisitantePosicao) AS Posicao,
                                        SUM(jj.MediaNum) AS       TotalMedia,
                                        SUM(jj.PrecoNum) AS       TotalPreco,
                                        COUNT(1) AS               Quantidade
                                   FROM dbo.Jogador AS jj
                                        JOIN dbo.Clube AS cl ON cl.ClubeId = jj.ClubeId
                                        LEFT JOIN Partida AS partidaCasa ON partidaCasa.Rodada = 12
                                                                            AND partidaCasa.ClubeCasaId = jj.ClubeId
                                        LEFT JOIN Partida AS partidaFora ON partidaFora.Rodada = 12
                                                                            AND partidaFora.ClubeVisitanteId = jj.ClubeId
                                        JOIN dbo.Clube AS casa ON casa.ClubeId = ISNULL(partidaCasa.ClubeCasaId, partidaFora.
                                        ClubeCasaId)
                                        JOIN dbo.Clube AS fora ON fora.ClubeId = ISNULL(partidaCasa.ClubeVisitanteId, partidaFora.
                                        ClubeVisitanteId)
                                        JOIN dbo.Posicao AS ps ON ps.PosicaoId = jj.PosicaoId
                                  WHERE jj.StatusId = 7
                                        AND JJ.RodadaId = {round -1}
                                  GROUP BY cl.ClubeId,
                                           cl.Nome,
                                           IIF(partidaCasa.ClubeCasaId IS NOT NULL, partidaCasa.ClubeCasaPosicao, partidaFora.
                                           ClubeVisitantePosicao)) AS PT01) AS TC ON TC.ClubeId = PR.ClubeCasaId
                   JOIN(SELECT *,
                                PT02.TotalMedia / PT02.Quantidade AS                             MediaPontos,
                                PT02.TotalPreco / PT02.Quantidade AS                             MediaPreco,
                                (PT02.TotalMedia / PT02.Quantidade + (3 * (PT02.TotalPreco / PT02.Quantidade))) / 4 AS
                                                                                                 MediaPonderada
                           FROM(SELECT cl.ClubeId,
                                        cl.Nome,
                                        IIF(partidaCasa.ClubeCasaId IS NOT NULL, partidaCasa.ClubeCasaPosicao, partidaFora.
                                        ClubeVisitantePosicao) AS Posicao,
                                        SUM(jj.MediaNum) AS       TotalMedia,
                                        SUM(jj.PrecoNum) AS       TotalPreco,
                                        COUNT(1) AS               Quantidade
                                   FROM dbo.Jogador AS jj
                                        JOIN dbo.Clube AS cl ON cl.ClubeId = jj.ClubeId
                                        LEFT JOIN Partida AS partidaCasa ON partidaCasa.Rodada = 12
                                                                            AND partidaCasa.ClubeCasaId = jj.ClubeId
                                        LEFT JOIN Partida AS partidaFora ON partidaFora.Rodada = 12
                                                                            AND partidaFora.ClubeVisitanteId = jj.ClubeId
                                        JOIN dbo.Clube AS casa ON casa.ClubeId = ISNULL(partidaCasa.ClubeCasaId, partidaFora.
                                        ClubeCasaId)
                                        JOIN dbo.Clube AS fora ON fora.ClubeId = ISNULL(partidaCasa.ClubeVisitanteId, partidaFora.
                                        ClubeVisitanteId)
                                        JOIN dbo.Posicao AS ps ON ps.PosicaoId = jj.PosicaoId
                                  WHERE jj.StatusId = 7
                                        AND JJ.RodadaId = {round - 1}
                                  GROUP BY cl.ClubeId,
                                           cl.Nome,
                                           IIF(partidaCasa.ClubeCasaId IS NOT NULL, partidaCasa.ClubeCasaPosicao, partidaFora.
                                           ClubeVisitantePosicao)) AS PT02) AS TF ON TF.ClubeId = PR.ClubeVisitanteId
             WHERE PR.Rodada = {round};
            ";

            var result = _cartolaDapperRepository.GetAll<RoundNumber>(sql, null, commandType: CommandType.Text);

            return result;
        }
    }
}
