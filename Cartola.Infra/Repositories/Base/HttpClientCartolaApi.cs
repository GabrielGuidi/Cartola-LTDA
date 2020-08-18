using Cartola.Domain.Entities;
using Cartola.Infra.Models;
using Cartola.Infra.Repositories.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Cartola.Infra.Repositories.Base
{
    public class HttpClientCartolaApi : IHttpClientCartolaApi
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Lazy<string> Token;

        private readonly string _authentication = "https://login.globo.com/api/authentication";

        public HttpClientCartolaApi(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            Token = new Lazy<string>(() => Logar(new Login("EMAIL", "SENHA")));
        }

        public HttpRequestMessage GetRequest(string endpoint, HttpMethod method = null, bool withToken = false, StringContent content = null)
        {
            var request = new HttpRequestMessage(method ?? HttpMethod.Get, endpoint);
            GetHeaders(request);

            if (content != null)
                request.Content = content;

            if (withToken)
                request.Headers.Add("X-GLB-Token", Token.Value);

            return request;
        }

        public HttpClient GetClient()
        {
            return _clientFactory.CreateClient();
        }

        public T Request<T>(string endpoint, HttpMethod method = null, bool withToken = false, StringContent content = null) where T : class
        {
            using var client = GetClient();
            var request = GetRequest(endpoint, method, withToken, content);
            using var response = client.SendAsync(request);
            var responseJson = response.Result.Content.ReadAsStringAsync().Result;
            response.Dispose();
            return JsonSerializer.Deserialize<T>(responseJson);
        }

        private void GetHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("Accept", "application/json, text/plain, */*");
            request.Headers.Add("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.87 Safari/537.36");
        }

        #region [Login]
        private string Logar(Login login)
        {
            var json = JsonSerializer.Serialize(login);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = Request<AuthenticationJson>(_authentication, HttpMethod.Post, false, stringContent);

            return result.GlobalId;
        }

        public string GetToken()
        {
            return Token.Value;
        }
        #endregion
    }
}
