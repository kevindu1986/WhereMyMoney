using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereMyMoney.Models;

namespace WhereMyMoney.Controllers
{
    public class BaseController : Controller
    {
        protected WhereMyMoneyContext _context;

        public BaseController(WhereMyMoneyContext context)
        {
            _context = context;
        }
    }
}