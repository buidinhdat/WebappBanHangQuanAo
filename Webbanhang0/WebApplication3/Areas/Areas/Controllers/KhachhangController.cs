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
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;

namespace WebApplication3.Areas.Areas.Controllers
{
    public class KhachhangController : Controller
    {
        private ModelAdmin db = new ModelAdmin();

        // GET: Areas/Khachhang
        //public ActionResult Index()
        //{
        //    return View(db.khachhang.ToList());
        //}
        public ActionResult Index(int? page)
        {
            try
            {
                
                if (page == null) page = 1;
                var links = (from l in db.khachhang select l).OrderBy(x => x.makhachhang);
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                PagedList<khachhang> model = new PagedList<khachhang>(links, pageNumber, pageSize);
                return View(model);
            }

            catch
            {
                return RedirectToAction("Adminlogin", "Admin");

            }

        }


        // GET: Areas/Khachhang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khachhang khachhang = db.khachhang.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // GET: Areas/Khachhang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Areas/Khachhang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "makhachhang,tendangnhap,matkhau,hoten,sodienthoai,diachi,email,quyen")] khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.khachhang.Add(khachhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachhang);
        }

        // GET: Areas/Khachhang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khachhang khachhang = db.khachhang.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // POST: Areas/Khachhang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "makhachhang,tendangnhap,matkhau,hoten,sodienthoai,diachi,email,quyen")] khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachhang);
        }

        // GET: Areas/Khachhang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khachhang khachhang = db.khachhang.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // POST: Areas/Khachhang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            khachhang khachhang = db.khachhang.Find(id);
            db.khachhang.Remove(khachhang);
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

        public FileResult Download()
        {

            List<khachhang> likh = new List<khachhang>();
            likh = db.khachhang.ToList();
            XSSFWorkbook wb = new XSSFWorkbook();

            // Tạo ra 1 sheet
            ISheet sheet = wb.CreateSheet();

            // Bắt đầu ghi lên sheet

            // Tạo row

            // Ghi tên cột ở row 1
            var row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("Mã khách hàng");
            row1.CreateCell(1).SetCellValue("Tên đăng nhập");
            row1.CreateCell(2).SetCellValue("Mật khẩu");
            row1.CreateCell(3).SetCellValue("Họ tên");
            row1.CreateCell(4).SetCellValue("Số điện thoại");
            row1.CreateCell(5).SetCellValue("Địa chỉ");
            row1.CreateCell(6).SetCellValue("Email");
            row1.CreateCell(7).SetCellValue("Quyền");

            // bắt đầu duyệt mảng và ghi tiếp tục
            int rowIndex = 2;
            foreach (var item in likh)
            {
                // tao row mới
                var newRow = sheet.CreateRow(rowIndex);

                // set giá trị
                newRow.CreateCell(0).SetCellValue(item.makhachhang);
                newRow.CreateCell(1).SetCellValue(item.tendangnhap);
                newRow.CreateCell(2).SetCellValue(item.matkhau);
                newRow.CreateCell(3).SetCellValue(item.hoten);
                newRow.CreateCell(4).SetCellValue(item.sodienthoai);
                newRow.CreateCell(5).SetCellValue(item.diachi);
                newRow.CreateCell(6).SetCellValue(item.email);
                newRow.CreateCell(7).SetCellValue((double)item.quyen);

                // tăng index
                rowIndex++;
            }

            // xong hết thì save file lại
            String filename = "D:\\FIleDoAn\\khachhang.xlsx";
            FileInfo fi = new FileInfo(filename);
            // kiểm tra tệp
            if (fi.Exists)
            {
                fi.Delete();
            }

            FileStream fs = new FileStream(filename, FileMode.CreateNew);
            wb.Write(fs);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filename);
            string fileName = "khachhang.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);




        }
    }
}
