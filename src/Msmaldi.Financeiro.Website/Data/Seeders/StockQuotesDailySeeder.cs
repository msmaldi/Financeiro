using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Msmaldi.Financeiro.Website.Data.Seeders.ExternalProviders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Seeders
{
    public class StockQuotesDailySeeder
    {
        private readonly FinanceiroDbContext _db;
        private readonly StockQuotesProvider _provider;
        public StockQuotesDailySeeder(FinanceiroDbContext db)
        {
            _db = db;
            _provider = new StockQuotesProvider();
        }

        public async Task AtualizarAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //await AtualizarStockAsync(cancellationToken);

            var stocksNoBanco = await _db.Stocks.ToListAsync(cancellationToken);
            foreach (var stock in stocksNoBanco)
            {
                try
                {
                    await AtualizarStockQuoteDailyAsync(stock.Symbol);
                    System.Threading.Thread.Sleep(1000 * 15);
                }
                catch (Exception e)
                { 
                    Console.WriteLine($"Erro para atualizar {stock.Symbol}");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public async Task AtualizarStockAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var stocksNoBanco = await _db.Stocks.ToListAsync(cancellationToken);

            var lines = File.ReadAllLines(@"./Data/Seeds/Stock.txt");
            var stocks = lines.Select(l => ObterStock(l));
            
            var  stocksParaAtualizar = stocks.Except(stocksNoBanco, StockComparer.Instance);
            
            await _db.Stocks.AddRangeAsync(stocksParaAtualizar, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task AtualizarStockQuoteDailyAsync(string symbol, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"\t\t{symbol}");
            var stocksQuotesNoBanco = await _db.StockQuotesDaily.Where(s => s.Symbol == symbol).ToListAsync(cancellationToken);

            var stocksQuotes = await _provider.GetStockQuoteDailyAsync(symbol);
            
            
            var stocksQuotesParaAtualizar = stocksQuotes.Except(stocksQuotesNoBanco, StockQuoteDailyComparer.Instance);
            System.Console.WriteLine(stocksQuotesParaAtualizar.Count());
            if (stocksQuotesParaAtualizar.Count() > 0)
            {
                await _db.StockQuotesDaily.AddRangeAsync(stocksQuotesParaAtualizar, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
            }
            else
            {
                var lastNoBanco = stocksQuotesNoBanco.Last();                
                var first = stocksQuotes.First();

                if (lastNoBanco.Date == first.Date)
                {
                    lastNoBanco.Close = first.Close;
                    lastNoBanco.Open = first.Open;
                    await _db.SaveChangesAsync(cancellationToken);
                }
            }
        }

        private static Stock ObterStock(string symbol)
        {
            return new Stock(symbol);
        }

        class StockComparer : IEqualityComparer<Stock>
        {
            public bool Equals(Stock x, Stock y) => (x.Symbol == y.Symbol);
            public int GetHashCode(Stock obj) => obj.Symbol.GetHashCode();

            public static StockComparer Instance => new StockComparer();
        }

        class StockQuoteDailyComparer : IEqualityComparer<StockQuoteDaily>
        {
            public bool Equals(StockQuoteDaily x, StockQuoteDaily y) => (x.Symbol == y.Symbol && x.Date == y.Date);
            public int GetHashCode(StockQuoteDaily obj) => obj.Symbol.GetHashCode();

            public static StockQuoteDailyComparer Instance => new StockQuoteDailyComparer();
        }
    }
}