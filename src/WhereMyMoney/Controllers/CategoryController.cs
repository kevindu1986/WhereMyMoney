using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereMyMoney.Models;
using Microsoft.AspNetCore.DataProtection;

namespace WhereMyMoney.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(WhereMyMoneyContext context, IDataProtectionProvider provider) : base(context, provider)
        {
        }

        public IActionResult Index()
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            return View(_context.Tbl_Category.Where(c => c.IsActive && c.UserID == Session.UserId).ToList());
        }

        public IActionResult Create()
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Tbl_Category category)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            if (ModelState.IsValid)
            {
                category.IsActive = true;
                category.UserID = Session.UserId;
                _context.Tbl_Category.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Update(int id)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            Tbl_Category category = _context.Tbl_Category.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Tbl_Category category)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            if (ModelState.IsValid)
            {
                Tbl_Category dbCategory = _context.Tbl_Category.Where(c => c.Id == category.Id).FirstOrDefault();
                dbCategory.CategoryName = category.CategoryName;
                dbCategory.Description = category.Description;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            if (Session == null)
            {
                return RedirectToLogIn();
            }

            Tbl_Category category = _context.Tbl_Category.Where(c => c.Id == id).FirstOrDefault();
            category.IsActive = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}