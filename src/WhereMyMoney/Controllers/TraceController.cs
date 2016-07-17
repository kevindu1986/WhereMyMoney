using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WhereMyMoney.Models;
using Microsoft.AspNetCore.DataProtection;

namespace WhereMyMoney.Controllers
{
    public class TraceController : BaseController
    {
        public TraceController(WhereMyMoneyContext context, IDataProtectionProvider provider) : base(context, provider)
        {
        }

        public IActionResult Index()
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            return View(_context.Tbl_Trace.Include(c=>c.Currency).Where(c => c.IsActive && c.UserId == Session.UserId).OrderBy(c=>c.TraceDate).ToList());
        }

        public IActionResult Create()
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            ViewBag.CurrencyList = _context.Tbl_Currency.Where(c=>c.IsActive).OrderBy(c => c.CurrencyShortName).ToList();
            ViewBag.ServerTraceDate = string.Empty;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tbl_Trace trace)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            if (ModelState.IsValid)
            {
                trace.IsActive = true;
                trace.UserId = Session.UserId;
                _context.Tbl_Trace.Add(trace);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServerTraceDate = trace.TraceDate == DateTime.MinValue ? string.Empty : trace.TraceDate.ToString("dd-MMM-yyyy");
            ViewBag.CurrencyList = _context.Tbl_Currency.Where(c => c.IsActive).OrderBy(c => c.CurrencyShortName).ToList();
            return View(trace);
        }

        public IActionResult Update(int id)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            Tbl_Trace trace = _context.Tbl_Trace.Where(c => c.Id == id).FirstOrDefault();
            if (trace != null)
            {
                ViewBag.CurrencyList = _context.Tbl_Currency.Where(c => c.IsActive).OrderBy(c => c.CurrencyShortName).ToList();
                ViewBag.ServerTraceDate = trace.TraceDate == DateTime.MinValue ? string.Empty : trace.TraceDate.ToString("dd-MMM-yyyy");

                return View(trace);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(Tbl_Trace trace)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            if (ModelState.IsValid)
            {
                Tbl_Trace dbTrace = _context.Tbl_Trace.Where(c => c.Id == trace.Id).FirstOrDefault();
                dbTrace.Amount = trace.Amount;
                dbTrace.TraceDate = trace.TraceDate;
                dbTrace.Description = trace.Description;
                dbTrace.CurrencyId = trace.CurrencyId;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServerTraceDate = trace.TraceDate == DateTime.MinValue ? string.Empty : trace.TraceDate.ToString("dd-MMM-yyyy");
            ViewBag.CurrencyList = _context.Tbl_Currency.Where(c => c.IsActive).OrderBy(c => c.CurrencyShortName).ToList();
            return View(trace);
        }

        public IActionResult Delete(int id)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            Tbl_Trace trace = _context.Tbl_Trace.Where(c => c.Id == id).FirstOrDefault();
            trace.IsActive = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}