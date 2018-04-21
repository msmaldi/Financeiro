using System;
using System.Threading;
using System.Threading.Tasks;
using Msmaldi.Financeiro.Website.Data.Seeders;

namespace Msmaldi.Financeiro.Website.HostedServices
{
    public class CryptoCurrencyUpdaterService : BackgroundService
    {
        private readonly CryptoCurrencySeeder _seeder;
        public CryptoCurrencyUpdaterService(CryptoCurrencySeeder seeder)
        {
            _seeder = seeder;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                System.Console.WriteLine("Atualizando CryptoCurrency");
                try
                {
                    await _seeder.AtualizarAsync(stoppingToken);
                    await _seeder.AtualizarBTCTickerAsync(stoppingToken);
                    await _seeder.AtualizarETHTickerAsync(stoppingToken);
                }
                catch (Exception e)
                {        
                    System.Console.WriteLine(e);          
                }
                await Task.Delay(3*60*1000, stoppingToken);
            }
        }
    }
}