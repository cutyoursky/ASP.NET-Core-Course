using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using System.Text.Json;

namespace Repositories
{
    public class FinnhubRepository : IFinnhubRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FinnhubRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }  
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
            };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            return responseDictionary;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}") 
            };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            return responseDictionary;
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_configuration["FinnhubToken"]}") //URI includes the secret token
            };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            List<Dictionary<string, string>>? responseDictionary = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            return responseDictionary;
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_configuration["FinnhubToken"]}") //URI includes the secret token
            };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            return responseDictionary;
        }
    }
}
