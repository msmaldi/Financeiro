using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Msmaldi.Financeiro.Website.Models;
using Msmaldi.Financeiro.Website.Entities;
using Msmaldi.Financeiro.Website.Data;
using Msmaldi.Financeiro.Website.BusinessLogic.CDB;
using Msmaldi.Financeiro.Website.BusinessLogic.SwingTrade;
using Msmaldi.Financeiro.Website.BusinessLogic.CryptoCurrencies;

namespace Msmaldi.Financeiro.Website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Financeiro.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
