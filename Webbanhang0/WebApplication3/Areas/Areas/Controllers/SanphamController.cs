using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Areas.Areas.Models;
using PagedList;
using PagedList.Mvc;

namespace WebApplication3.Areas.Areas.Controllers
{
    public class SanphamController : Controller
    {
        private ModelAdmin db = new ModelAdmin();

        // GET: Areas/Sanpham
        public ActionResult Index(int ? page)
        {
            try
            {
                var sanpham = db.sanpham.Include(s => s.nhasanxuat).Include(s => s.nhomhang);

                if (page == null) page = 1;
                var links = (from l in db.sanpham select l).OrderBy(x => x.masanpham);
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                PagedList<sanpham> model = new PagedList<sanpham>(links, pageNumber, pageSize);
                return View(model);
            }
           
            catch
            {
                return RedirectToAction("Adminlogin", "Admin");

            }

        }

        // GET: Areas/Sanpham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanpham.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // GET: Areas/Sanpham/Create
        public ActionResult Create()
        {
            ViewBag.manhasanxuat = new SelectList(db.nhasanxuat, "manhasanxuat", "tennhasanxuat");
            ViewBag.manhomhang = new SelectList(db.nhomhang, "manhomhang", "tennhomhang");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "masanpham,manhasanxuat,manhomhang,tensanpham,dongia,soluong,chitiet")] sanpham sanpham)
        {
            var fileName = "";
            var path = "";


                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        fileName= Path.GetFileName(file.FileName);
                        path = Path.Combine(Server.MapPath("~/Content/anh"), fileName);
                        file.SaveAs(path);
                    }
                }
                else
                {
                return RedirectToAction("Adminlogin", "Admin");
                }
            
            if (ModelState.IsValid)
            {
                sanpham.anhsanpham = fileName;
                db.sanpham.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.manhasanxuat = new SelectList(db.nhasanxuat, "manhasanxuat", "tennhasanxuat", sanpham.manhasanxuat);
            ViewBag.manhomhang = new SelectList(db.nhomhang, "manhomhang", "tennhomhang", sanpham.manhomhang);
            return View(sanpham);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "masanpham,manhasanxuat,manhomhang,tensanpham,dongia,soluong,chitiet")] sanpham sanpham)
        //{
        //    var file = Request.Files["fileupload"];
        //    var fileName = "";

        //    if (file != null && file.ContentLength > 0)
        //    {
        //        // lấy tên tệp tin
        //        fileName = Path.GetFileName(file.FileName);
        //        // lưu trữ tệp tin vào folder ~/Content/anh
        //        var path = Path.Combine(Server.MapPath("~/Content/anh"), fileName);
        //        file.SaveAs(path);
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        sanpham.anhsanpham = fileName;
        //        db.sanpham.Add(sanpham);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
            
        //    ViewBag.manhasanxuat = new SelectList(db.nhasanxuat, "manhasanxuat", "tennhasanxuat", sanpham.manhasanxuat);
        //    ViewBag.manhomhang = new SelectList(db.nhomhang, "manhomhang", "tennhomhang", sanpham.manhomhang);
        //    return View(sanpham);
        //}


        // GET: Areas/Sanpham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanpham.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.manhasanxuat = new SelectList(db.nhasanxuat, "manhasanxuat", "tennhasanxuat", sanpham.manhasanxuat);
            ViewBag.manhomhang = new SelectList(db.nhomhang, "manhomhang", "tennhomhang", sanpham.manhomhang);
            return View(sanpham);
        }

        // POST: Areas/Sanpham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "masanpham,manhasanxuat,manhomhang,tensanpham,dongia,soluong,anhsanpham,chitiet")] sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.manhasanxuat = new SelectList(db.nhasanxuat, "manhasanxuat", "tennhasanxuat", sanpham.manhasanxuat);
            ViewBag.manhomhang = new SelectList(db.nhomhang, "manhomhang", "tennhomhang", sanpham.manhomhang);
            return View(sanpham);
        }

        // GET: Areas/Sanpham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanpham.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: Areas/Sanpham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sanpham sanpham = db.sanpham.Find(id);
            db.sanpham.Remove(sanpham);
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
