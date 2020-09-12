using Cartola.Domain.Entities;
using Cartola.Domain.Services.IRepositories;
using Cartola.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cartola.Domain.Services
{
    public class CartolaService : ICartolaService
    {
        private readonly ICartolaRepository _cartolaRepository;
        private readonly ICargaCartolaRepository _cargaCartolaRepository;

        public CartolaService(ICartolaRepository cartolaRepository, ICargaCartolaRepository cargaCartolaService)
        {
            _cartolaRepository = cartolaRepository;
            _cargaCartolaRepository = cargaCartolaService;
        }

        public List<Jogador> GetJogadoresMercado()
        {
            var response = _cartolaRepository.GetJogadoresMercado()
                .OrderBy(x => x.Clube.Nome)
                .ThenBy(x => x.PosicaoId)
                .ThenByDescending(x => x.PrecoNum)
                .ThenBy(x => x.Apelido)
                .ToList();

            return response;
        }

        public List<PontuacaoParcial> GetParciais()
        {
            var response = _cartolaRepository.GetParciais()
                .OrderByDescending(x => x.Pontuacao)
                .ToList();

            return response;
        }

        #region [Objetos]
        public List<Jogador> GetJogadores()
        {
            var response = _cartolaRepository.Jogadores
                .ToList();

            return response;
        }

        public List<Clube> GetClubes()
        {
            return _cartolaRepository.Clubes;
        }

        public List<Posicao> GetPosicoes()
        {
            return _cartolaRepository.Posicoes;
        }

        public List<Esquema> GetEsquemas()
        {
            return _cartolaRepository.Esquemas;
        }

        public StatusMercado GetStatusMercado()
        {
            var status = _cargaCartolaRepository.GetStatusMercadoFromApi();

            status.DataFechamento = new DateTime(
                status.Fechamento.Ano, status.Fechamento.Mes, status.Fechamento.Dia,
                status.Fechamento.Hora, status.Fechamento.Minuto, 0);

            return status;
        }
        #endregion

        #region [Tools]
        public string FormatarFotoJogador(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return "https://s.glbimg.com/es/sde/f/2020/07/17/f2485c14c045d91afbaf3fc23b1a15d4_140x140.png";

            if (!link.Contains("FORMATO"))
                return "https://s.glbimg.com/es/sde/f/2020/07/17/f2485c14c045d91afbaf3fc23b1a15d4_140x140.png";

            return link.Replace("FORMATO", "140x140");
        }

        public int GetUltimaRodadaSemConsolidar()
        {
            List<JogadorHistorico> listaJogadores = _cartolaRepository.GetJogadoresHistoricoSemConsolidar();
            var rodada = listaJogadores
                .OrderByDescending(x => x.DataModificacao)
                .FirstOrDefault()
                .RodadaId;

            return rodada;
        }
        #endregion


        #region [To do]
        //public void Escalar()
        //{
        /*TODO TESTE*/
        //var atletas = GetAtletasMercado();

        //var escalacao = new Escalacao
        //{
        //    ListaAtletas = new AtletaJson[12]
        //};
        //escalacao.ListaAtletas[0] = atletas.FirstOrDefault(x => x.PosicaoId == 1 && x.PrecoNum <= 8);
        //escalacao.ListaAtletas[1] = atletas.Where(x => x.PosicaoId == 2 && x.PrecoNum <= 8).ToArray()[0];
        //escalacao.ListaAtletas[2] = atletas.Where(x => x.PosicaoId == 2 && x.PrecoNum <= 8).ToArray()[1];
        //escalacao.ListaAtletas[3] = atletas.Where(x => x.PosicaoId == 3 && x.PrecoNum <= 8).ToArray()[0];
        //escalacao.ListaAtletas[4] = atletas.Where(x => x.PosicaoId == 3 && x.PrecoNum <= 8).ToArray()[1];
        //escalacao.ListaAtletas[5] = atletas.Where(x => x.PosicaoId == 4 && x.PrecoNum <= 8).ToArray()[0];
        //escalacao.ListaAtletas[6] = atletas.Where(x => x.PosicaoId == 4 && x.PrecoNum <= 8).ToArray()[18];
        //escalacao.ListaAtletas[7] = atletas.Where(x => x.PosicaoId == 4 && x.PrecoNum <= 8).ToArray()[16];
        //escalacao.ListaAtletas[8] = atletas.Where(x => x.PosicaoId == 5 && x.PrecoNum <= 8).ToArray()[20];
        //escalacao.ListaAtletas[9] = atletas.Where(x => x.PosicaoId == 5 && x.PrecoNum <= 8).ToArray()[18];
        //escalacao.ListaAtletas[10] = atletas.Where(x => x.PosicaoId == 5 && x.PrecoNum <= 8).ToArray()[2];
        //escalacao.ListaAtletas[11] = atletas.FirstOrDefault(x => x.PosicaoId == 6 && x.PrecoNum <= 8);

        //SetEsquema(escalacao);
        //SetAtletaEscalacao(escalacao);

        //escalacao.Capitao = escalacao.Atletas[8];
        /*TESTE*/

        //if (escalacao.ListaAtletas.Sum(x => x.PrecoNum) > 100)
        //    return;

        //bool foi =_cartolaRepository.Escalar(escalacao);
        //}

        //private void SetEsquema(Escalacao escalacao)
        //{
        //var esquemas = _cartolaRepository.GetEsquemas();
        //var esquema = esquemas.Where(x => x.Posicoes.Laterais == escalacao.ListaAtletas.Count(t => t.PosicaoId == 2) &&
        //                    x.Posicoes.Zagueiros == escalacao.ListaAtletas.Count(t => t.PosicaoId == 3) &&
        //                    x.Posicoes.Meias == escalacao.ListaAtletas.Count(t => t.PosicaoId == 4) &&
        //                    x.Posicoes.Atacantes == escalacao.ListaAtletas.Count(t => t.PosicaoId == 5));

        //escalacao.EsquemaId = esquema.FirstOrDefault().EsquemaId;
        //}

        //private void SetAtletaEscalacao(Escalacao escalacao)
        //{
        //    //escalacao.Atletas = escalacao.ListaAtletas.Select(x => x.AtletaId).ToArray();
        //}
        #endregion
    }
}
