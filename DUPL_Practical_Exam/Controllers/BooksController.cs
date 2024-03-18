using DUPL_Practical_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using X.PagedList;

namespace DUPL_Practical_Exam.Controllers
{  
    public class BooksController : Controller
    {
        private readonly BookDbContext db;
        public BooksController()
        {
            db = new BookDbContext();
        }
        //GET: Books

        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }
        public ActionResult Create(string message ="")
        {
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book bk)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(bk);
                db.SaveChanges();
               
                return RedirectToAction("Create", new {message = "Data saved successfully" });
            }
            return View(bk);
        }
        public ActionResult Edit(int id)
        {
            return View(db.Books.FirstOrDefault(x => x.BookId == id));
        }
        [HttpPost]
        public ActionResult Edit(Book bk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bk).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Data Update successfully";
                return View("Edit",bk);
            }
            return View(bk);
        }
        public ActionResult Delete(int id)
        {
            var data = db.Books.FirstOrDefault(x => x.BookId == id);
            if (data != null)
            {
                db.Books.Remove(data);
                db.SaveChanges();
                return Json(new { success = true, message = "Record deleted successfully"});
            }
            else
            {
                return Json(new { success = false, message = "Record not found"});
            }
        }


    }
}