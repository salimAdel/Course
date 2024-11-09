using Course.Data;
using Course.Model;
using Microsoft.AspNetCore.Mvc;

namespace Course.Areas.dashboard.Controllers
{
    [Area("dashboard")]

    public class BookController : Controller
    {
        private ApplicationDbContext db;
        public BookController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var s = db.Books.ToList();
            return View(s);
        }
        public IActionResult Update(int Id) 
        {
            if (Id == 0)
                return View(new Book { Id = 0 });

            var md = db.Books.Single(a => a.Id == Id);
            var x = 1;
            return View(md);
        }
        [HttpPost]
        public IActionResult Save(Book md)
        {
            if (md.Id == 0)
            {
                try
                {
                    db.Books.Add(md);
                    db.SaveChanges();
                    TempData["Success"] = "تم الأضافة بنجاح..!";
                }
                catch (Exception ee)
                {
                    TempData["error"] = "حدث خطأ أثناء اضافة العنصر..!";
                }
            }
            else
            {
                try
                {
                    db.Update(md);
                    db.SaveChanges();
                    TempData["Success"] = "تم تعديل البيانات بنجاح..!";
                }
                catch (Exception ee)
                {
                    TempData["error"] = "حدث خطأ أثناء تعديل العنصر..!";
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var md = db.Books.Single(a => a.Id == id);
            db.Remove(md);
            db.SaveChanges();
            TempData["Success"] = "تم حذف البيانات بنجاح..!";
            return RedirectToAction("Index");
        }
    }
}
