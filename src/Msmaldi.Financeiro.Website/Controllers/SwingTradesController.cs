using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msmaldi.Financeiro.Website.BusinessLogic.SwingTrade;
using Msmaldi.Financeiro.Website.Data;
using Msmaldi.Financeiro.Website.Entities;
using Msmaldi.Financeiro.Website.Models.SwingTradeViewModels;

namespace Msmaldi.Financeiro.Website.Controllers
{
    [Authorize]
    public class SwingTradesController : Controller
    {
        private readonly FinanceiroDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly PosicaoConsolidadaSwingTradeFactory _swingTradePosFactory;
        
        public SwingTradesController(FinanceiroDbContext context,
            PosicaoConsolidadaSwingTradeFactory swingTradePosFactory,
          UserManager<User> userManager)
        {
            _db = context;
            _userManager = userManager;
            _swingTradePosFactory = swingTradePosFactory;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var swingTrades = await _db.SwingTrades.AsNoTracking()
                .Where(c => c.UserId == user.Id)
                .ToListAsync();
            var posicoesSwingTrade = swingTrades
                .Select(st => _swingTradePosFactory.ObterPosicaoConsolidada(st)).ToList();

            return View(posicoesSwingTrade);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SwingTradeViewModel m)
        {
            if (ModelState.IsValid)
            {
                var swingTrade = new SwingTrade(m.Symbol, m.Quantidade,
                    m.ValorDeAquisicao, userId: (await GetCurrentUserAsync()).Id);
                _db.SwingTrades.Add(swingTrade);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(m);
        }

        // GET: SwingTrades/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            SwingTrade swingTrade;
            if (id == null ||
                (swingTrade = await GetSwingTradeAsync(id.Value)) == null)
                return NotFound();

            return View(swingTrade);
        }

        // POST: SwingTrades/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var swingTrade = await GetSwingTradeAsync(id);
            _db.SwingTrades.Remove(swingTrade);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<SwingTrade> GetSwingTradeAsync(long id)
        {
            var userId = (await GetCurrentUserAsync()).Id;
            return await _db.SwingTrades.Where(m => m.UserId == userId && m.Id == id).SingleOrDefaultAsync();
        }

        private Task<User> GetCurrentUserAsync()
            => _userManager.GetUserAsync(HttpContext.User);
    }
}