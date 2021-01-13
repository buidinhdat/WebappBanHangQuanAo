using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Areas.Areas.Models;
using PagedList;
using PagedList.Mvc;

namespace WebApplication3.Areas.Areas.Controllers
{
    public class ThanhtoanController : Controller
    {
        private ModelAdmin db = new ModelAdmin();

        // GET: Areas/Thanhtoan
        public ActionResult Index(int? page)
        {
            try
            {
                if (page == null) page = 1;
                var links = (from l in db.thanhtoan select l).OrderBy(x => x.maphuongthucthanhtoan);
                int pageSize = 7;
                int pageNumber = (page ?? 1);

                return View(links.ToPagedList(pageNumber, pageSize));
            }
            catch
            {
                return RedirectToAction("Adminlogin", "Admin");

            }

        }

        // GET: Areas/Thanhtoan/Details/5
        

        // GET: Areas/Thanhtoan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Areas/Thanhtoan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maphuongthucthanhtoan,tenphuongthucthanhtoan")] thanhtoan thanhtoan)
        {
            if (ModelState.IsValid)
            {
                db.thanhtoan.Add(thanhtoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thanhtoan);
        }

        // GET: Areas/Thanhtoan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thanhtoan thanhtoan = db.thanhtoan.Find(id);
            if (thanhtoan == null)
            {
                return HttpNotFound();
            }
            return View(thanhtoan);
        }

        // POST: Areas/Thanhtoan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maphuongthucthanhtoan,tenphuongthucthanhtoan")] thanhtoan thanhtoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thanhtoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thanhtoan);
        }

        // GET: Areas/Thanhtoan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thanhtoan thanhtoan = db.thanhtoan.Find(id);
            if (thanhtoan == null)
            {
                return HttpNotFound();
            }
            return View(thanhtoan);
        }

        // POST: Areas/Thanhtoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            thanhtoan thanhtoan = db.thanhtoan.Find(id);
            db.thanhtoan.Remove(thanhtoan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
