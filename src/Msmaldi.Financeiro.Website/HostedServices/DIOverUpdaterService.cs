using System.Threading;
using System.Threading.Tasks;
using Msmaldi.Financeiro.Data.Seeder;

namespace Msmaldi.Financeiro.Website.HostedServices
{
    public class DIOverUpdaterService : BackgroundService
    {
        private readonly TaxasDIOverSeeder _seeder;
        public DIOverUpdaterService(TaxasDIOverSeeder seeder)
        {
            _seeder = seeder;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                System.Console.WriteLine("Atualizando TaxasDIOver");
                try
                {
                    await _seeder.AtualizarAsync(stoppingToken);
                }
                catch 
                {                    
                }
                await Task.Delay(1*60*1000, stoppingToken);
            }
        }
    }
}