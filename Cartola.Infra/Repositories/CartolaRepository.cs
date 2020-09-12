using Cartola.Domain.Entities;
using Cartola.Domain.Services.IRepositories;
using Cartola.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public CartolaRepository(IHttpClientCartolaApi clientFactory, CartolaDBContext cartolaDBContext)
        {
            _httpClientCartolaApi = clientFactory;
            _cartolaDBContext = cartolaDBContext;
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
    }
}
