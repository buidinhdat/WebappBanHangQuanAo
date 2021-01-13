using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Areas.Areas.Models;
using PagedList.Mvc;
using PagedList;

namespace WebApplication3.Areas.Areas.Controllers
{
    public class DanhgiaController : Controller
    {
        private ModelAdmin db = new ModelAdmin();

        // GET: Areas/Danhgia
        public ActionResult Index(int ? page)
        {
            try
            {
                var danhgia = db.danhgia.Include(n => n.madanhgia);
                if (page == null) page = 1;
                var links = (from l in db.danhgia select l).OrderBy(x => x.madanhgia);
                int pageSize = 7;
                int pageNumber = (page ?? 1);
                return View(links.ToPagedList(pageNumber, pageSize));
            }
            catch
            {
                return RedirectToAction("Adminlogin", "Admin");
            }
            
        }

        // GET: Areas/Danhgia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            danhgia danhgia = db.danhgia.Find(id);
            if (danhgia == null)
            {
                return HttpNotFound();
            }
            return View(danhgia);
        }

        // GET: Areas/Danhgia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Areas/Danhgia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "madanhgia,masanpham,makhachhang,noidung")] danhgia danhgia)
        {
            if (ModelState.IsValid)
            {
                db.danhgia.Add(danhgia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhgia);
        }

        // GET: Areas/Danhgia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            danhgia danhgia = db.danhgia.Find(id);
            if (danhgia == null)
            {
                return HttpNotFound();
            }
            

            return View(danhgia);
        }

        // POST: Areas/Danhgia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "madanhgia,masanpham,makhachhang,noidung")] danhgia danhgia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhgia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhgia);
        }

        // GET: Areas/Danhgia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            danhgia danhgia = db.danhgia.Find(id);
            if (danhgia == null)
            {
                return HttpNotFound();
            }
            return View(danhgia);
        }

        // POST: Areas/Danhgia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            danhgia danhgia = db.danhgia.Find(id);
            db.danhgia.Remove(danhgia);
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
