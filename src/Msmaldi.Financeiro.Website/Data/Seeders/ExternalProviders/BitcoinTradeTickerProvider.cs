using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Msmaldi.Financeiro.Website.Data.Seeders.ExternalProviders.Model;
using Msmaldi.Financeiro.Website.Entities;
using Newtonsoft.Json;

namespace Msmaldi.Financeiro.Website.Data.Seeders.ExternalProviders
{
    public class BitcoinTradeTickerProvider
    {
        private static HttpClient _httpClient;
        public BitcoinTradeTickerProvider()
        {            
            if (_httpClient == null)
            {
                _httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(15),
                    BaseAddress = new Uri("https://api.bitcointrade.com.br")
                };
            }
        }

        public Task<CryptoCurrencyLastTicker> GetBTCTickerAsync(
            CancellationToken cancellationToken = default(CancellationToken))
            => GetTickerAsync("BTC", cancellationToken);

        public Task<CryptoCurrencyLastTicker> GetETHTickerAsync(
            CancellationToken cancellationToken = default(CancellationToken))
            => GetTickerAsync("ETH", cancellationToken);

        public async Task<CryptoCurrencyLastTicker> GetTickerAsync(string cryptoCurrency,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var source = $"/v2/public/{"BRL" + cryptoCurrency}/ticker";
            var httpResponse = await _httpClient.GetAsync(source, cancellationToken);
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                var m = JsonConvert.DeserializeObject<BitcoinTradeTicker>(content);
                return new CryptoCurrencyLastTicker(cryptoCurrency, m.data.date, m.data.last, m.data.buy, m.data.sell, source);
            }
            throw new Exception($"{httpResponse.StatusCode}");
        }
    }
}