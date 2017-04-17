using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoRedux.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoRedux.Controllers
{
    public class ItemsController : Controller
    {
        private ToDoReduxContext db = new ToDoReduxContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(db.Items.Include(items => items.Category).ToList());
        }

        public IActionResult Details(int id)
        {
            var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.TestString = "hey";
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Delete(Item item)
        {
            //var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
