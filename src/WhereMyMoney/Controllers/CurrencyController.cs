using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereMyMoney.Models;
using Microsoft.AspNetCore.DataProtection;

namespace WhereMyMoney.Controllers
{
    public class CurrencyController : BaseController
    {
        public CurrencyController(WhereMyMoneyContext context, IDataProtectionProvider provider) : base(context, provider)
        {
        }

        public IActionResult Index()
        {
            if(Session == null)
            {
                return RedirectToLogIn();
            }
            if (!Session.IsAdmin)
            {
                return RedirectToTrace();
            }

            return View(_context.Tbl_Currency.Where(c=>c.IsActive).ToList());
        }

        public IActionResult Create()
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }
            if (!Session.IsAdmin)
            {
                return RedirectToTrace();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Tbl_Currency currency)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }
            if (!Session.IsAdmin)
            {
                return RedirectToTrace();
            }

            if (ModelState.IsValid)
            {
                currency.IsActive = true;
                _context.Tbl_Currency.Add(currency);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currency);
        }

        public IActionResult Update(int id)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }
            if (!Session.IsAdmin)
            {
                return RedirectToTrace();
            }

            Tbl_Currency currency = _context.Tbl_Currency.Where(c => c.Id == id).FirstOrDefault();
            return View(currency);
        }

        [HttpPost]
        public IActionResult Update(Tbl_Currency currency)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }
            if (!Session.IsAdmin)
            {
                return RedirectToTrace();
            }

            if (ModelState.IsValid)
            {
                Tbl_Currency dbCurrency = _context.Tbl_Currency.Where(c => c.Id == currency.Id).FirstOrDefault();
                dbCurrency.CurrencyName = currency.CurrencyName;
                dbCurrency.CurrencyShortName = currency.CurrencyShortName;
                dbCurrency.Description = currency.Description;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currency);
        }

        public IActionResult Delete(int id)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }
            if (!Session.IsAdmin)
            {
                return RedirectToTrace();
            }

            Tbl_Currency currency = _context.Tbl_Currency.Where(c => c.Id == id).FirstOrDefault();
            currency.IsActive = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}