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
    public class ChitietdonhangsController : Controller
    {
        private ModelAdmin db = new ModelAdmin();

        // GET: Areas/Chitietdonhangs
        //public ActionResult Index()
        //{
        //    var chitietdonhang = db.chitietdonhang.Include(c => c.donhang).Include(c => c.sanpham);
        //    return View(chitietdonhang.ToList());
        //}
        public ActionResult Index(int? page)
        {
            try
            {
                var chitietdonhang = db.chitietdonhang.Include(c => c.donhang).Include(c => c.sanpham);
                if (page == null) page = 1;
                var links = (from l in db.chitietdonhang select l).OrderBy(x => x.machitietdonhang);
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                PagedList<chitietdonhang> model = new PagedList<chitietdonhang>(links, pageNumber, pageSize);
                return View(model);
            }

            catch
            {
                return RedirectToAction("Adminlogin", "Admin");

            }
        }

        // GET: Areas/Chitietdonhangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietdonhang chitietdonhang = db.chitietdonhang.Find(id);
            if (chitietdonhang == null)
            {
                return HttpNotFound();
            }
            return View(chitietdonhang);
        }

        // GET: Areas/Chitietdonhangs/Create
        public ActionResult Create()
        {
            ViewBag.madonhang = new SelectList(db.donhang, "madonhang", "tenkhachhang");
            ViewBag.masanpham = new SelectList(db.sanpham, "masanpham", "tensanpham");
            return View();
        }

        // POST: Areas/Chitietdonhangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "machitietdonhang,masanpham,madonhang,soluongmua,dongia,tongtien,tensanpham")] chitietdonhang chitietdonhang)
        {
            if (ModelState.IsValid)
            {
                db.chitietdonhang.Add(chitietdonhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.madonhang = new SelectList(db.donhang, "madonhang", "tenkhachhang", chitietdonhang.madonhang);
            ViewBag.masanpham = new SelectList(db.sanpham, "masanpham", "tensanpham", chitietdonhang.masanpham);
            return View(chitietdonhang);
        }

        // GET: Areas/Chitietdonhangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietdonhang chitietdonhang = db.chitietdonhang.Find(id);
            if (chitietdonhang == null)
            {
                return HttpNotFound();
            }
            ViewBag.madonhang = new SelectList(db.donhang, "madonhang", "tenkhachhang", chitietdonhang.madonhang);
            ViewBag.masanpham = new SelectList(db.sanpham, "masanpham", "tensanpham", chitietdonhang.masanpham);
            return View(chitietdonhang);
        }

        // POST: Areas/Chitietdonhangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "machitietdonhang,masanpham,madonhang,soluongmua,dongia,tongtien,tensanpham")] chitietdonhang chitietdonhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitietdonhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.madonhang = new SelectList(db.donhang, "madonhang", "tenkhachhang", chitietdonhang.madonhang);
            ViewBag.masanpham = new SelectList(db.sanpham, "masanpham", "tensanpham", chitietdonhang.masanpham);
            return View(chitietdonhang);
        }

        // GET: Areas/Chitietdonhangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietdonhang chitietdonhang = db.chitietdonhang.Find(id);
            if (chitietdonhang == null)
            {
                return HttpNotFound();
            }
            return View(chitietdonhang);
        }

        // POST: Areas/Chitietdonhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            chitietdonhang chitietdonhang = db.chitietdonhang.Find(id);
            db.chitietdonhang.Remove(chitietdonhang);
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
