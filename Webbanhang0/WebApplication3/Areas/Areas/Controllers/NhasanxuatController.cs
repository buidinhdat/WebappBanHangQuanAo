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
    public class NhasanxuatController : Controller
    {
        private ModelAdmin db = new ModelAdmin();

        // GET: Areas/Nhasanxuat
        public ActionResult Index(int? page)
        {
            try
            {
                if (page == null) page = 1;
                var links = (from l in db.nhasanxuat select l).OrderBy(x => x.manhasanxuat);
                int pageSize = 7;
                int pageNumber = (page ?? 1);

                return View(links.ToPagedList(pageNumber, pageSize));
            }
            catch
            {
                return RedirectToAction("Adminlogin", "Admin");

            }
        }

        // GET: Areas/Nhasanxuat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nhasanxuat nhasanxuat = db.nhasanxuat.Find(id);
            if (nhasanxuat == null)
            {
                return HttpNotFound();
            }
            return View(nhasanxuat);
        }

        // GET: Areas/Nhasanxuat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Areas/Nhasanxuat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "manhasanxuat,tennhasanxuat,diachinhasanxuat")] nhasanxuat nhasanxuat)
        {
            if (ModelState.IsValid)
            {
                db.nhasanxuat.Add(nhasanxuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhasanxuat);
        }

        // GET: Areas/Nhasanxuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nhasanxuat nhasanxuat = db.nhasanxuat.Find(id);
            if (nhasanxuat == null)
            {
                return HttpNotFound();
            }
            return View(nhasanxuat);
        }

        // POST: Areas/Nhasanxuat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "manhasanxuat,tennhasanxuat,diachinhasanxuat")] nhasanxuat nhasanxuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhasanxuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhasanxuat);
        }

        // GET: Areas/Nhasanxuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nhasanxuat nhasanxuat = db.nhasanxuat.Find(id);
            if (nhasanxuat == null)
            {
                return HttpNotFound();
            }
            return View(nhasanxuat);
        }

        // POST: Areas/Nhasanxuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            nhasanxuat nhasanxuat = db.nhasanxuat.Find(id);
            db.nhasanxuat.Remove(nhasanxuat);
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
