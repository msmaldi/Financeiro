using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Msmaldi.Financeiro.Website.Data.Seeders.ExternalProviders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Seeders
{
    public class CryptoCurrencySeeder
    {
        private readonly FinanceiroDbContext _db;
        private readonly BitcoinTradeTickerProvider _provider;
        
        public CryptoCurrencySeeder(FinanceiroDbContext db)
        {
            _db = db;
            _provider = new BitcoinTradeTickerProvider();
        }


        public async Task AtualizarAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await AtualizarCryptoCurrencyListAsync(cancellationToken);
        }


        public async Task AtualizarBTCTickerAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var exist = _db.CryptoCurrencyLastTickers.Any(c => c.CriptoCurrencyId == "BTC");
            var ticker = await _provider.GetBTCTickerAsync(cancellationToken);
            EntityEntry<CryptoCurrencyLastTicker> entry;
            if (exist)
                entry = _db.CryptoCurrencyLastTickers.Update(ticker);
            else
                entry = _db.CryptoCurrencyLastTickers.Add(ticker);
            await _db.SaveChangesAsync(cancellationToken);
            entry.State = EntityState.Detached;                 
        }

        public async Task AtualizarETHTickerAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var exist = _db.CryptoCurrencyLastTickers.Any(c => c.CriptoCurrencyId == "ETH");
            var ticker = await _provider.GetETHTickerAsync(cancellationToken);
            EntityEntry<CryptoCurrencyLastTicker> entry;
            if (exist)
                entry = _db.CryptoCurrencyLastTickers.Update(ticker);
            else
                entry = _db.CryptoCurrencyLastTickers.Add(ticker);
            await _db.SaveChangesAsync(cancellationToken);
            entry.State = EntityState.Detached;         
        }

        public async Task AtualizarCryptoCurrencyListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var cryptoCurrenciesNoBanco = await _db.CryptoCurrencies.ToListAsync(cancellationToken);

            var lines = File.ReadAllLines(@"./Data/Seeds/CryptoCurrency.txt");
            var cryptoCurrencies = lines.Select(l => ObterCryptoCurrency(l));
            
            var cryptoCurrenciesParaAtualizar = cryptoCurrencies.Except(cryptoCurrenciesNoBanco, CryptoCurrencyComparer.Instance);
            
            await _db.CryptoCurrencies.AddRangeAsync(cryptoCurrenciesParaAtualizar, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public static CryptoCurrency ObterCryptoCurrency(string conteudo)
        {
            var split = conteudo.Split(" ");
            return new CryptoCurrency(split[0], split[1]);
        }

        class CryptoCurrencyComparer : IEqualityComparer<CryptoCurrency>
        {
            public bool Equals(CryptoCurrency x, CryptoCurrency y) => (x.Id == y.Id);
            public int GetHashCode(CryptoCurrency obj) => obj.Id.GetHashCode();

            public static CryptoCurrencyComparer Instance => new CryptoCurrencyComparer();
        }
    }
}