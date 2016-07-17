using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WhereMyMoney.Models;

namespace WhereMyMoney.Controllers
{
    public class BaseController : Controller
    {
        protected WhereMyMoneyContext _context;
        protected IDataProtectionProvider _provider;
        protected IDataProtector _protector;

        public BaseController(WhereMyMoneyContext context, IDataProtectionProvider provider)
        {
            _context = context;
            _provider = provider;
            _protector = _provider.CreateProtector("WhereMyMoney");
        }

        private ISession HttpSession {
            get
            {
                return HttpContext.Session;
            }
        }

        protected SessionObject Session {
            get
            {
                var value = HttpSession.GetString("SessionObject");
                SessionObject session = value == null ? default(SessionObject) : JsonConvert.DeserializeObject<SessionObject>(value);
                if(session != null)
                {
                    ViewBag.Session = session.UserName;
                }

                return value == null ? default(SessionObject) : JsonConvert.DeserializeObject<SessionObject>(value);
            }
            set
            {
                HttpSession.SetString("SessionObject", JsonConvert.SerializeObject(value));
            }
        }

        protected IActionResult RedirectToLogIn()
        {
            HttpSession.Clear();
            ViewBag.Session = null;
            return RedirectToAction("LogIn", "User");
        }

        protected IActionResult RedirectToTrace()
        {
            return RedirectToAction("Index", "Trace");
        }
    }
}