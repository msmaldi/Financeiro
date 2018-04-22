using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Msmaldi.Financeiro.Website.BusinessLogic.CryptoCurrencies;
using Msmaldi.Financeiro.Website.Data;
using Msmaldi.Financeiro.Website.Entities;
using Msmaldi.Financeiro.Website.Models.CryptoWalletViewModels;

namespace Msmaldi.Financeiro.Website.Controllers
{
    [Authorize]
    public class CryptoWalletController : Controller
    {
        private readonly FinanceiroDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly PosicaoConsolidadaCryptoCurrencyFactory _factory;

        public CryptoWalletController(FinanceiroDbContext context,
            PosicaoConsolidadaCryptoCurrencyFactory factory,
            UserManager<User> userManager)
        {
            _db = context;
            _userManager = userManager;
            _factory = factory;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var wallets = await _db.CryptoWallets.AsNoTracking()
                .Where(c => c.UserId == user.Id)
                .ToListAsync();
            var posWallets = wallets
                .Select(st => _factory.ObterPosicaoConsolidada(st)).ToList();

            return View(posWallets);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetCryptoCurrenciesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CryptoWalletViewModel m)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var cryptoWallet = new CryptoWallet(m.Label, m.Quantidade, m.ValorDeAquisicao, m.CriptoCurrencyId, user.Id);
                _db.CryptoWallets.Add(cryptoWallet);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 

            await GetCryptoCurrenciesAsync(m.CriptoCurrencyId);
            return View(m);
        }

        private async Task GetCryptoCurrenciesAsync(string selectedOption = null)
        {
            var cryptos = await _db.CryptoCurrencies.ToListAsync();
            var selected = await _db.CryptoCurrencies.FirstOrDefaultAsync(c => c.Id == selectedOption);
            ViewBag.CryptoCurrencies = new SelectList(cryptos, "Id", "Name", selected);
        }



        private Task<User> GetCurrentUserAsync()
            => _userManager.GetUserAsync(HttpContext.User);
    }
}