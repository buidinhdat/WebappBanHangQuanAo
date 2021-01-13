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
    public class LoaihangController : Controller
    {
        private ModelAdmin db = new ModelAdmin();

        // GET: Areas/Loaihang
        public ActionResult Index(int? page)
        {
            try
            {
                if (page == null) page = 1;
                var links = (from l in db.loaihang select l).OrderBy(x => x.maloai);
                int pageSize = 7;
                int pageNumber = (page ?? 1);

                return View(links.ToPagedList(pageNumber, pageSize));

            }
            catch
            {
                return RedirectToAction("AdminLogin", "Admin");

            }

        }

        // GET: Areas/Loaihang/Details/5
       

        // GET: Areas/Loaihang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Areas/Loaihang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maloai,tenloai")] loaihang loaihang)
        {
            if (ModelState.IsValid)
            {
                db.loaihang.Add(loaihang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaihang);
        }

        // GET: Areas/Loaihang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaihang loaihang = db.loaihang.Find(id);
            if (loaihang == null)
            {
                return HttpNotFound();
            }
            return View(loaihang);
        }

        // POST: Areas/Loaihang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maloai,tenloai")] loaihang loaihang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaihang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaihang);
        }

        // GET: Areas/Loaihang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaihang loaihang = db.loaihang.Find(id);
            if (loaihang == null)
            {
                return HttpNotFound();
            }
            return View(loaihang);
        }

        // POST: Areas/Loaihang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            loaihang loaihang = db.loaihang.Find(id);
            db.loaihang.Remove(loaihang);
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
