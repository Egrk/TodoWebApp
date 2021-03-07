using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TodoWA.Models;

namespace TodoWA.Controllers
{
    public class HomeController : Controller
    {
        TaskContext db;
        public HomeController(TaskContext context)
        {
            db = context;
        }
        
        public IActionResult Index()
        {
            return View(db.Tasks.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Models.Task task)
        {
            db.Tasks.Add(task);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }
            Models.Task task = db.Tasks.Find(Id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(Models.Task task)
        {
            db.Tasks.Update(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }
            Models.Task task = db.Tasks.Find(Id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(Models.Task task)
        {
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
