using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Seeders.ExternalProviders
{
    public class StockQuotesProvider : IDisposable
    {
        private readonly HttpClient _httpClient;
        public StockQuotesProvider()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.alphavantage.co")
            };
        }

        public async Task<List<StockQuoteDaily>> GetStockQuoteDailyAsync(string symbol,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string result = await _httpClient.GetStringAsync($"/query?function=TIME_SERIES_DAILY&symbol={symbol}&interval=daily&datatype=csv&outputsize=compact&apikey=QERUFYVJKZMQB6J9");
            if (result.Contains("Invalid API call."))
                throw new Exception("Invalid API call.");
            string[] results = result.Split(Environment.NewLine);
            var resultToReturn = new List<StockQuoteDaily>();
            for (int i = 1; i < results.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(results[i]))
                    continue;
                string[] rr = results[i].Split(',');
                var date = DateTime.ParseExact(rr[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var open = double.Parse(rr[1], CultureInfo.InvariantCulture);
                var high = double.Parse(rr[2], CultureInfo.InvariantCulture);
                var low = double.Parse(rr[3], CultureInfo.InvariantCulture);
                var close = double.Parse(rr[4], CultureInfo.InvariantCulture);
                var volume = double.Parse(rr[5], CultureInfo.InvariantCulture);

                resultToReturn.Add(new StockQuoteDaily(symbol, date, open, close));
            }
            
            return resultToReturn;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}