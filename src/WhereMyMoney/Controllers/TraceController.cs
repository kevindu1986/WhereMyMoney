using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WhereMyMoney.Models;

namespace WhereMyMoney.Controllers
{
    public class TraceController : BaseController
    {
        public TraceController(WhereMyMoneyContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View(_context.Tbl_Trace.Include(c=>c.Currency).Where(c => c.IsActive).OrderBy(c=>c.TraceDate).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}