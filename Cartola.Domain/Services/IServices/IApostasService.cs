using System.Collections.Generic;

namespace Cartola.Domain.Services.IServices
{
    public interface IApostasService
    {
        List<decimal> GerarAnaliseAposta(decimal lucroPorRodada, decimal? chanceDeSucesso = null, decimal? fatorRetorno = null);
    }
}
