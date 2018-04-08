using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msmaldi.Financeiro.Website.Data;
using Msmaldi.Financeiro.Website.Data.Seeders;

namespace Msmaldi.Financeiro.Website.Controllers
{
    public class FeriadosController : Controller
    {
        private readonly FinanceiroDbContext _db;
        public FeriadosController(FinanceiroDbContext context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var feriados = await _db.Feriados.AsNoTracking().ToListAsync();
            return View(feriados);
        }
    }
}