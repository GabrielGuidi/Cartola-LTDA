using Cartola.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cartola.Domain.Services
{
    public class ApostasService : IApostasService
    {
        private const decimal lucroCasa = 0.075m;
        private readonly decimal multiplicadorLucro;

        public ApostasService()
        {
            multiplicadorLucro = 1 - lucroCasa;
        }

        public List<decimal> GerarAnaliseAposta(decimal lucroPorRodada, decimal? chanceDeSucesso = null, decimal? fatorRetorno = null)
        {
            var listaAportes = new List<decimal>();
            var controleRodada = 1;

            if (fatorRetorno == null)
                fatorRetorno = CalcularFatorRetorno(chanceDeSucesso);

            if (chanceDeSucesso == null)
                chanceDeSucesso = CalcularChanceDeSucesso(fatorRetorno);

            var chanceDePerderABanca = 1d;

            while (chanceDePerderABanca * 100 > 1)
            {
                listaAportes.Add(CalcularAporte(lucroPorRodada, listaAportes, controleRodada, fatorRetorno));
                chanceDePerderABanca = Math.Pow((1 - (double)chanceDeSucesso), controleRodada++ + 1);
            }

            return listaAportes;
        }

        private decimal CalcularAporte(decimal lucroPorRodada, List<decimal> listaAportes, int controleRodada, decimal? fatorRetorno)
        {
            var somaAporteAnteriores = listaAportes.Sum();
            var aporte = (lucroPorRodada * controleRodada + somaAporteAnteriores) / ((decimal)fatorRetorno - 1);

            return Math.Round(aporte, 2);
        }

        private decimal CalcularChanceDeSucesso(decimal? fatorRetorno)
        {
            if (fatorRetorno == null)
                throw new ArgumentException("Valor fatorRetorno não pode ser nulo!");

            var chanceDeSucesso = 1 / (fatorRetorno / multiplicadorLucro);

            return (decimal)chanceDeSucesso;
        }

        private decimal CalcularFatorRetorno(decimal? chanceDeSucesso)
        {
            if (chanceDeSucesso == null)
                throw new ArgumentException("Valor chanceDeSucesso não pode ser nulo!");

            var fatorRetornoBruto = 1m / chanceDeSucesso;

            var fatorRetorno = fatorRetornoBruto * multiplicadorLucro;

            return (decimal)fatorRetorno;
        }
    }
}
