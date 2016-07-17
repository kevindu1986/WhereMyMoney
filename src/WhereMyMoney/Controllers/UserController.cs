using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereMyMoney.Models;
using Microsoft.AspNetCore.DataProtection;

namespace WhereMyMoney.Controllers
{
    public class UserController : BaseController
    {
        public UserController(WhereMyMoneyContext context, IDataProtectionProvider provider) : base(context, provider)
        {
        }

        public IActionResult Index()
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }
            if (!Session.IsAdmin)
            {
                return RedirectToTrace();
            }

            return View(_context.Tbl_User.Where(c=>c.IsActive).ToList());
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
        public IActionResult Create(Tbl_User user)
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
                int checkUserExist = _context.Tbl_User.Where(c => c.UserName == user.UserName).Count();
                if (checkUserExist > 0)
                {
                    ViewBag.Message = "This user name is not available to use.";
                    goto EndCode;
                }

                user.IsActive = true;

                _context.Tbl_User.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            EndCode:
            return View(user);
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

            Tbl_User user = _context.Tbl_User.Where(c => c.Id == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(Tbl_User user)
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
                Tbl_User dbUser = _context.Tbl_User.Where(c => c.Id == user.Id).FirstOrDefault();
                dbUser.UserName = user.UserName;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
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

            Tbl_User user = _context.Tbl_User.Where(c => c.Id == id).FirstOrDefault();
            user.IsActive = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(string userName, string password)
        {
            Tbl_User user = _context.Tbl_User.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
            if(user != null)
            {
                Session = new SessionObject() { UserName = user.UserName, UserId = user.Id, IsAdmin = user.IsAdmin };
                return RedirectToAction("Index", "Trace");
            }
            ViewBag.Message = "Username or Password is not valid!";
            return View();
        }

        public IActionResult LogOut()
        {
            return RedirectToLogIn();
        }

        public IActionResult ChangePassword()
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            Tbl_User user = _context.Tbl_User.Where(c => c.Id == Session.UserId).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public IActionResult ChangePassword(Tbl_User user)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            Tbl_User dbUser = _context.Tbl_User.Where(c => c.Id == Session.UserId).FirstOrDefault();
            if (user.Password != dbUser.Password)
            {
                ModelState.AddModelError("Wrong Password", "Wrong password.");
            }

            if (ModelState.IsValid)
            {
                dbUser.Password = user.NewPassword;
                _context.SaveChanges();
                return RedirectToAction("Index", "Trace");
            }
            return View(user);
        }
    }
}