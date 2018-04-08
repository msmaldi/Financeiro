using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msmaldi.Financeiro.Website.BusinessLogic.CDB;
using Msmaldi.Financeiro.Website.Data;
using Msmaldi.Financeiro.Website.Entities;
using Msmaldi.Financeiro.Website.Models.CDBComCDIViewModels;

namespace Msmaldi.Financeiro.Website.Controllers
{
    [Authorize]
    public class CDBsComCDIController : Controller
    {        
        private readonly FinanceiroDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly PosicaoConsolidadaCDBComCDIFactory _factory;

        public CDBsComCDIController(FinanceiroDbContext context,
                                      UserManager<User> userManager,
                     PosicaoConsolidadaCDBComCDIFactory factory)
        {
            _db = context;
            _userManager = userManager;
            _factory = factory;
        }

        // GET: CDBsComCDI/Create
        public async Task<IActionResult> Index()
        {
            var cdbs = await GetAllCDBsComCDIAsync();
            var posCDBs = cdbs.Select(c => _factory.ObterPosicaoConsolidada(c));
            return View(posCDBs);
        }

        // GET: CDBsComCDI/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CDBsComCDI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CDBComCDIViewModel m)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var cdb = new CDBComCDI(m.DataDaAplicacao,
                                        m.DataDoVencimento,
                                        m.PrecoUnitario,
                                        m.Quantidade,
                                        m.Taxa / 100.0,
                                        user);
                _db.Add(cdb);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(m);
        }

        // GET: CDBsComCDI/RegistrarRegate/5
        public async Task<IActionResult> RegistrarResgate(int? id)
        {
            CDBComCDI cdbComCDI;
            if (id == null ||
                (cdbComCDI = await GetCDBComCDIAsync(id.Value)) == null)
                return NotFound();

            return View();
        }

        // POST: CDBsComCDI/RegistrarRegate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarResgate(int id, CDBComCDIResgateViewModel m)
        {
            CDBComCDI cdbComCDI;
            if ((cdbComCDI = await GetCDBComCDIAsync(id)) == null)
                return NotFound();
            if (!ModelState.IsValid)
                return View(m);
            var resgate = new ResgateCDBComCDI { Data = m.Data, Quantidade = m.Quantidade, CDBComCDIId = cdbComCDI.Id };

            var result = cdbComCDI.PodeAdicionarResgate(resgate);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.TryAddModelError(error.Item1, error.Item2);
                return View(m);
            }
            cdbComCDI.AdicionarResgate(resgate);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: CDBsComCDI/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            CDBComCDI cdbComCDI;
            if (id == null ||
                (cdbComCDI = await GetCDBComCDIAsync(id.Value)) == null)
                return NotFound();

            return View(cdbComCDI);
        }

        // POST: CDBsComCDI/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cdbComCDI = await GetCDBComCDIAsync(id);
            _db.CDBsComCDI.Remove(cdbComCDI);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        
        private async Task<CDBComCDI> GetCDBComCDIAsync(int id)
        {
            var userId = (await GetCurrentUserAsync()).Id;
            return await _db.CDBsComCDI.Include(c => c.Resgates).Where(m => m.UserId == userId && m.Id == id).SingleOrDefaultAsync();
        }
        
        private async Task<List<CDBComCDI>> GetAllCDBsComCDIAsync()
        {
            var userId = (await GetCurrentUserAsync()).Id;
            return await _db.CDBsComCDI.Include(c => c.Resgates).Where(c => c.UserId == userId).ToListAsync();
        }

        private Task<User> GetCurrentUserAsync()
            => _userManager.GetUserAsync(HttpContext.User);
    }
}