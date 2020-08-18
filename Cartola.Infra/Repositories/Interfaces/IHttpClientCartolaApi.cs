using System.Net.Http;
using System.Threading.Tasks;

namespace Cartola.Infra.Repositories.Interfaces
{
    public interface IHttpClientCartolaApi
    {
        public HttpRequestMessage GetRequest(string endpoint, HttpMethod method = null, bool withToken = false, StringContent content = null);

        public HttpClient GetClient();

        public T Request<T>(string endpoint, HttpMethod method = null, bool withToken = false, StringContent content = null) where T : class;
    }
}