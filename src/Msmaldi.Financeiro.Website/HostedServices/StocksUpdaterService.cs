using System.Threading;
using System.Threading.Tasks;
using Msmaldi.Financeiro.Website.Data.Seeders;

namespace Msmaldi.Financeiro.Website.HostedServices
{
    public class StocksUpdaterService : BackgroundService
    {
        private readonly StockQuotesDailySeeder _seeder;
        public StocksUpdaterService(StockQuotesDailySeeder seeder)
        {
            _seeder = seeder;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                tryagain:
                System.Console.WriteLine("Atualizando Stocks");
                try
                {
                    await _seeder.AtualizarAsync(stoppingToken);
                }
                catch 
                {
                    goto tryagain;
                }
                await Task.Delay(60*1000, stoppingToken);
            }
        }
    }
}