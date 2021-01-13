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
    public class NhomhangController : Controller
    {
        private ModelAdmin db = new ModelAdmin();

        // GET: Areas/Nhomhang
        //public ActionResult Index()
        //{
        //    var nhomhang = db.nhomhang.Include(n => n.loaihang);
        //    return View(nhomhang.ToList());
        //}

        // GET: Areas/Nhomhang/Details/5
        // test
        public ActionResult Index(int? page)
        {
            try
            {
                var nhomhang = db.nhomhang.Include(n => n.loaihang);
                if (page == null) page = 1;
                var links = (from l in db.nhomhang select l).OrderBy(x => x.manhomhang);
                int pageSize = 7;
                int pageNumber = (page ?? 1);
                return View(links.ToPagedList(pageNumber, pageSize));

            }
            catch
            {
                return RedirectToAction("Adminlogin", "Admin");

            }

        }


        // GET: Areas/Nhomhang/Create
        public ActionResult Create()
        {
            ViewBag.maloai = new SelectList(db.loaihang, "maloai", "tenloai");
            return View();
        }

        // POST: Areas/Nhomhang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "manhomhang,maloai,tennhomhang")] nhomhang nhomhang)
        {
            if (ModelState.IsValid)
            {
                db.nhomhang.Add(nhomhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maloai = new SelectList(db.loaihang, "maloai", "tenloai", nhomhang.maloai);
            return View(nhomhang);
        }

        // GET: Areas/Nhomhang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nhomhang nhomhang = db.nhomhang.Find(id);
            if (nhomhang == null)
            {
                return HttpNotFound();
            }
            ViewBag.maloai = new SelectList(db.loaihang, "maloai", "tenloai", nhomhang.maloai);
            return View(nhomhang);
        }

        // POST: Areas/Nhomhang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "manhomhang,maloai,tennhomhang")] nhomhang nhomhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhomhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maloai = new SelectList(db.loaihang, "maloai", "tenloai", nhomhang.maloai);
            return View(nhomhang);
        }

        // GET: Areas/Nhomhang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nhomhang nhomhang = db.nhomhang.Find(id);
            if (nhomhang == null)
            {
                return HttpNotFound();
            }
            return View(nhomhang);
        }

        // POST: Areas/Nhomhang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            nhomhang nhomhang = db.nhomhang.Find(id);
            db.nhomhang.Remove(nhomhang);
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
