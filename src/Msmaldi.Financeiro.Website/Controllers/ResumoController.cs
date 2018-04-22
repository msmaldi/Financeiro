using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msmaldi.Financeiro.Website.BusinessLogic.CDB;
using Msmaldi.Financeiro.Website.BusinessLogic.CryptoCurrencies;
using Msmaldi.Financeiro.Website.BusinessLogic.SwingTrade;
using Msmaldi.Financeiro.Website.Data;
using Msmaldi.Financeiro.Website.Entities;
using Msmaldi.Financeiro.Website.Models.ResumoViewModels;

namespace Msmaldi.Financeiro.Website.Controllers
{
    public class ResumoController : Controller
    {
        private readonly FinanceiroDbContext _db;
        private readonly PosicaoConsolidadaCDBComCDIFactory _cdbFactory;
        private readonly PosicaoConsolidadaSwingTradeFactory _swingTradeFactory;
        private readonly PosicaoConsolidadaCryptoCurrencyFactory _cryptoCurrencyFactory;

        public ResumoController(FinanceiroDbContext context,
            PosicaoConsolidadaCDBComCDIFactory cdbFactory,
            PosicaoConsolidadaSwingTradeFactory swingTradeFactory,
            PosicaoConsolidadaCryptoCurrencyFactory cryptoCurrencyFactory)
        {
            _db = context;
            _cdbFactory = cdbFactory;
            _swingTradeFactory = swingTradeFactory;
            _cryptoCurrencyFactory = cryptoCurrencyFactory; 
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await GetUserAsync();

            var ativos = new Ativos(
                user, 
                _cdbFactory,
                _swingTradeFactory,
                _cryptoCurrencyFactory);

            return View(ativos);
        }

        private async Task<User> GetUserAsync()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = await _db.Users.AsNoTracking()
                                .Include(u => u.CDBsComCDI)
                                .Include(u => u.SwingTrades)
                                .Include(u => u.CryptoWallets)
                                .FirstOrDefaultAsync(u => u.UserName == userName);

            return user;
        }
    }
}