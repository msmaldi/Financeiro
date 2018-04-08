using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Msmaldi.Financeiro.Website.Data.Seeders;

namespace Msmaldi.Financeiro.Website.Data
{
    public class SeedData
    {
        public static void Initialize(FeriadoSeeder seeder)
        {
            try
            {
                seeder.AtualizarAsync().Wait();
            }
            catch {}
        }
    }
}