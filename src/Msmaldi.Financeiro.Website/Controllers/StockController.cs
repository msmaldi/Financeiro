using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msmaldi.Financeiro.Website.Data;
using Msmaldi.Financeiro.Website.Data.Seeders.ExternalProviders;
using Msmaldi.Financeiro.Website.Entities;
using Msmaldi.Financeiro.Website.Models.SwingTradeViewModels;

namespace Msmaldi.Financeiro.Website.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
        private readonly FinanceiroDbContext _db;
        private readonly StockQuotesProvider _provider;
        public StockController(FinanceiroDbContext context)
        {
            _db = context;
            _provider = new StockQuotesProvider();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stocks = await _db.Stocks.ToListAsync();
            return View(stocks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewStock m)
        {
            if (!ModelState.IsValid)
                return View(m);

            
            if (await _db.Stocks.AllAsync(c => c.Symbol == m.Symbol))
            {
                ModelState.AddModelError("Symbol", "Está Ação já está cadastrada.");
                return View(m);
            }

            try
            {
                var stockQuoteDaily = await _provider.GetStockQuoteDailyAsync(m.Symbol);
                _db.Stocks.Add(new Stock(m.Symbol));
                await _db.SaveChangesAsync();
                _db.StockQuotesDaily.AddRange(stockQuoteDaily);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("Symbol", "Está Ação não foi encontrada, tente adicionar .SA ao final do código.");
                return View(m);
            }
        }

        public new void Dispose()
        {
            base.Dispose();
        } 
    }
}