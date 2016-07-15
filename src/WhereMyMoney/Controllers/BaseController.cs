using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereMyMoney.Models;

namespace WhereMyMoney.Controllers
{
    public class BaseController : Controller
    {
        protected WhereMyMoneyContext _context;

        public BaseController(WhereMyMoneyContext dbContext)
        {
            _context = dbContext;
            //HttpContext.Session.SetString("UserName", "dchao");
            //HttpContext.Session.SetString("UserId", "1");

        }
    }
}