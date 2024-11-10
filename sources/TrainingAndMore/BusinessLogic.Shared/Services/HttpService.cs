using BusinessLogic.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace BusinessLogic.Shared.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpClient Client => _httpClient;

        public HttpService(IConfiguration config)
        {
            var apiUrl = config.GetRequiredSection("ApiBaseUrl").Value;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };
        }

        public async Task<T?> SendGetRequest<T>(string url, string? token = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url, UriKind.Relative),
                Version = HttpVersion.Version11
            };

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            if (token != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(content);
            }

            return default;
        }

        public async Task<T?> SendPostRequest<T>(string url, string body, string? token = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url, UriKind.Relative),
                Version = HttpVersion.Version11,
                Content = new StringContent(body, encoding: Encoding.UTF8, "application/json")
            };

            if (token != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(content);
            }

            return default;
        }
    }
}
