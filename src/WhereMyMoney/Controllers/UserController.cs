using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereMyMoney.Models;

namespace WhereMyMoney.Controllers
{
    public class UserController : BaseController
    {
        public UserController(WhereMyMoneyContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View(_context.Tbl_User.Where(c=>c.IsActive).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tbl_User user)
        {
            if (ModelState.IsValid)
            {
                user.IsActive = true;
                _context.Tbl_User.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public IActionResult Update(int id)
        {
            Tbl_User user = _context.Tbl_User.Where(c => c.Id == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(Tbl_User user)
        {
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
            Tbl_User user = _context.Tbl_User.Where(c => c.Id == id).FirstOrDefault();
            user.IsActive = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}