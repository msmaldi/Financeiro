using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Msmaldi.Financeiro.Website.Data.Seeders;

namespace Msmaldi.Financeiro.Website.HostedServices
{
    public class FeriadosUpdaterService : BackgroundService
    {
        private readonly FeriadoSeeder _seeder;
        public FeriadosUpdaterService(FeriadoSeeder seeder)
        {
            _seeder = seeder;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                System.Console.WriteLine("Atualizando Feriados");
                try
                {
                    await _seeder.AtualizarAsync(stoppingToken);
                }
                catch {}
                await Task.Delay(15*60*1000, stoppingToken);
            }
        }
    }
}