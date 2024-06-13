using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetCompanyProfile(stockSymbol);

            return responseDictionary;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetStockPriceQuote(stockSymbol);

            return responseDictionary;
        }
        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            List<Dictionary<string, string>>? responseDictionary = await _finnhubRepository.GetStocks();

            return responseDictionary;
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbol)
        {
            Dictionary<string, object>? responseDictionary = await _finnhubRepository.SearchStocks(stockSymbol);

            return responseDictionary;
        }
    }
}
