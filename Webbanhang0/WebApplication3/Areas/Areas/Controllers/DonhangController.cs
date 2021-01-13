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
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using TuesPechkin;
using System.Drawing.Printing;
using iTextSharp.text.html.simpleparser;
using Microsoft.OpenPublishing.Build.HtmlConverters.HtmlToPdf;
using SelectPdf;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;
using GemBox.Spreadsheet;
namespace WebApplication3.Areas.Areas.Controllers
{
    public class DonhangController : Controller
    {
        private ModelAdmin db = new ModelAdmin();

        // GET: Areas/Donhang
        //public ActionResult Index()
        //{
        //    var donhang = db.donhang.Include(d => d.thanhtoan);
        //    return View(donhang.ToList());

        //}

        public int madonhang;
        public ActionResult Index(int ? page)
        {
            try
            {
                var donhang = db.donhang.Include(d => d.thanhtoan);
                if (page == null) page = 1;
                var links = (from l in db.donhang select l).OrderBy(x => x.madonhang);
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                PagedList<donhang> model = new PagedList<donhang>(links, pageNumber, pageSize);
                return View(model);
            }

            catch
            {
                return RedirectToAction("Adminlogin", "Admin");

            }
        }

        // GET: Areas/Donhang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            donhang donhang = db.donhang.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        // GET: Areas/Donhang/Create
        public ActionResult Create()
        {
            ViewBag.maphuongthucthanhtoan = new SelectList(db.thanhtoan, "maphuongthucthanhtoan", "tenphuongthucthanhtoan");
            return View();
        }

        // POST: Areas/Donhang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "madonhang,maphuongthucthanhtoan,tenkhachhang,ngaydathang,diachi,email,tinhtrang,ghichu,tongtienthanhtoan")] donhang donhang)
        {
            if (ModelState.IsValid)
            {
                db.donhang.Add(donhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maphuongthucthanhtoan = new SelectList(db.thanhtoan, "maphuongthucthanhtoan", "tenphuongthucthanhtoan", donhang.maphuongthucthanhtoan);
            return View(donhang);
        }

        // GET: Areas/Donhang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            donhang donhang = db.donhang.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            ViewBag.maphuongthucthanhtoan = new SelectList(db.thanhtoan, "maphuongthucthanhtoan", "tenphuongthucthanhtoan", donhang.maphuongthucthanhtoan);
            return View(donhang);
        }

        // POST: Areas/Donhang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "madonhang,maphuongthucthanhtoan,tenkhachhang,ngaydathang,diachi,email,tinhtrang,ghichu,tongtienthanhtoan")] donhang donhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maphuongthucthanhtoan = new SelectList(db.thanhtoan, "maphuongthucthanhtoan", "tenphuongthucthanhtoan", donhang.maphuongthucthanhtoan);
            return View(donhang);
        }

        // GET: Areas/Donhang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            donhang donhang = db.donhang.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        // POST: Areas/Donhang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            donhang donhang = db.donhang.Find(id);
            db.donhang.Remove(donhang);
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

            List<donhang> lidh = new List<donhang>();
            lidh = db.donhang.ToList();
            XSSFWorkbook wb = new XSSFWorkbook();

            // Tạo ra 1 sheet
            ISheet sheet = wb.CreateSheet();

            // Bắt đầu ghi lên sheet

            // Tạo row

            // Ghi tên cột ở row 1
            var row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("Mã đơn hàng");
            row1.CreateCell(1).SetCellValue("Phương thức thanh toán");
            row1.CreateCell(2).SetCellValue("Tên khách hàng");
            row1.CreateCell(3).SetCellValue("Ngày đặt hàng");
            row1.CreateCell(4).SetCellValue("Địa chỉ");
            row1.CreateCell(5).SetCellValue("Email");
            row1.CreateCell(6).SetCellValue("Tình trạng");
            row1.CreateCell(7).SetCellValue("Ghi chú");
            row1.CreateCell(8).SetCellValue("Tổng tiền");

            for (int i = 0; i < 9; i++)
            {
                row1.GetCell(i).CellStyle.WrapText = true;
                row1.GetCell(i).CellStyle.BorderBottom = BorderStyle.Thin;
                row1.GetCell(i).CellStyle.BorderTop = BorderStyle.Thin;
                row1.GetCell(i).CellStyle.BorderLeft = BorderStyle.Thin;
                row1.GetCell(i).CellStyle.BorderRight = BorderStyle.Thin;
                row1.GetCell(i).CellStyle.FillBackgroundColor = 400;
            }

            // bắt đầu duyệt mảng và ghi tiếp tục
            int rowIndex = 2;
            foreach (var item in lidh)
            {
                // tao row mới
                var newRow = sheet.CreateRow(rowIndex);
                
                // set giá trị
                newRow.CreateCell(0).SetCellValue(item.madonhang);
                newRow.CreateCell(1).SetCellValue(item.maphuongthucthanhtoan);
                newRow.CreateCell(2).SetCellValue(item.tenkhachhang);
                newRow.CreateCell(3).SetCellValue(item.ngaydathang.ToString());
                newRow.CreateCell(4).SetCellValue(item.diachi);
                newRow.CreateCell(5).SetCellValue(item.email);
                newRow.CreateCell(6).SetCellValue(item.tinhtrang);
                newRow.CreateCell(7).SetCellValue(item.ghichu);
                newRow.CreateCell(8).SetCellValue((double)item.tongtienthanhtoan);

                //Set style
                for(int i=0; i<9; i++)
                {
                    newRow.GetCell(i).CellStyle.WrapText = true;
                    newRow.GetCell(i).CellStyle.BorderBottom = BorderStyle.Thin;
                    newRow.GetCell(i).CellStyle.BorderTop = BorderStyle.Thin;
                    newRow.GetCell(i).CellStyle.BorderLeft = BorderStyle.Thin;
                    newRow.GetCell(i).CellStyle.BorderRight = BorderStyle.Thin;
                }
               

                // tăng index
                rowIndex++;
            }

            // xong hết thì save file lại

            String filePath = Application.PATH_EXPORT_FILE_EXCEL;
            DirectoryInfo dic = new DirectoryInfo(filePath);
            if (!dic.Exists)
            {
                dic.Create();
            }

             String fileName = "banhang"+ Application.CurrentTimeMillis().ToString()+ ".xlsx";
            

          
            FileStream fs = new FileStream(filePath+fileName, FileMode.CreateNew);
            wb.Write(fs);
            wb.Close();
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath + fileName);
           
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);




        }
        public List<chitietdonhang> lichitietdonhang = new List<chitietdonhang>();
        public ActionResult getChitietdonhang(int id)
        {
            ViewBag.madonhang = id;
            
            List < chitietdonhang > li = new List<chitietdonhang>();
            List<chitietdonhang> lictdh = new List<chitietdonhang>();
            List<donhang> lidh = new List<donhang>();
           
            lidh = db.donhang.ToList();
            foreach (donhang i in lidh)
            {
                if (i.madonhang == id)
                {
                    ViewBag.hoten = i.tenkhachhang;
                    ViewBag.tongtien = i.tongtienthanhtoan.ToString();
                    ViewBag.email = i.email;
                }
            }
            li = db.chitietdonhang.ToList();
            foreach(chitietdonhang item in li)
            {
                if (item.madonhang==id)
                {
                    lictdh.Add(item);
                }

            }
            lichitietdonhang = lictdh;
            ViewBag.chitietdonhang = lictdh;
            return View();
        }

        //Gửi email
        public ActionResult guiEmail(int id)
        {
            // Để gửi Email cầntạo đối tượng MailMessage: Đối tượng này chứa nội dung Email cần gửi
            // FromEmail là tài khoản gmail của bạn dùng để gửi Email
            // ToEmail là tài khoản gmail của người bạn muốn gửi
            String fromEmail = "buidinhdat08111997@gmail.com";
            String toEmail = Request.Form["email"].ToString();
            MailMessage mail = new MailMessage(fromEmail,toEmail);

            // Cho phép nội dung bên trong Email là thẻ HTML
            mail.IsBodyHtml = true;
            mail.Subject = "Thông báo đặt hàng từ Shop thời trang Long Hạnh";
            mail.Body = Request.Form["noidungemail"].ToString();

            //Đính kèm hóa đơn

            createFile(id);
            mail.Attachments.Add(new Attachment("D:\\FIleDoAn\\hoadon.pdf"));

            // Để gửi Email cần tạo sử dụng đối tượng SmtpClient
            SmtpClient smtp = new SmtpClient();

            // SMTP Server hiện tại của gmail là : "smtp.gmail.com"
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address "smtp.gmail.com"

            // Tạo xác thực của tài khoản gmail : User và pass của tài khoản gmail gửi
            smtp.Credentials = new System.Net.NetworkCredential(fromEmail, "01696296077");

            // "Cổng smtp của gmail": Thông thường là 587
            smtp.Port = 587;

            smtp.EnableSsl = true;

            // Thực hiện gửi email
            smtp.Send(mail);
            Session["statusSendMail"] = "Đã gửi mail";
            return Redirect(Request.UrlReferrer.ToString());
        }


        // Tạo hóa đơn
        public void createFile(int id)
        {
            List<chitietdonhang> li = new List<chitietdonhang>();
            List<chitietdonhang> lictdh = new List<chitietdonhang>();
            List<donhang> lidh = new List<donhang>();
            String ngaydathang = "";
            String hoten = "";
            String diachi = "";
            String email = "";
            String tongtien = "";
            lidh = db.donhang.ToList();
            foreach (donhang i in lidh)
            {
                if (i.madonhang == id)
                {
                    hoten = i.tenkhachhang;
                    tongtien = i.tongtienthanhtoan.ToString();
                    email = i.email;
                    ngaydathang = i.ngaydathang.ToString();
                    diachi = i.diachi;
                }
            }
            li = db.chitietdonhang.ToList();
            foreach (chitietdonhang item in li)
            {
                if (item.madonhang == id)
                {
                    lictdh.Add(item);
                }

            }
            lichitietdonhang = lictdh;

            XSSFWorkbook wb = new XSSFWorkbook();

            // Tạo ra 1 sheet
            ISheet sheet = wb.CreateSheet();

            // Bắt đầu ghi lên sheet

            var row1 = sheet.CreateRow(1);
            row1.CreateCell(5).SetCellValue("HÓA ĐƠN");
            var row2 = sheet.CreateRow(2);
            row2.CreateCell(0).SetCellValue("NGÀY ĐẶT HÀNG: ");

            row2.CreateCell(2).SetCellValue(ngaydathang);

            row2.CreateCell(6).SetCellValue("Shop thời trang Long Hạnh");
            var row3 = sheet.CreateRow(3);
            row3.CreateCell(0).SetCellValue("Họ tên: ");
            row3.CreateCell(2).SetCellValue(hoten);
            var row4 = sheet.CreateRow(4);
            row4.CreateCell(0).SetCellValue("Địa chỉ:");
            row4.CreateCell(2).SetCellValue(diachi);
            var row5 = sheet.CreateRow(5);
            row5.CreateCell(0).SetCellValue("Email:");
            row5.CreateCell(2).SetCellValue(email);

            // bắt đầu duyệt mảng và ghi tiếp tục
            var row6 = sheet.CreateRow(6);
            row6.CreateCell(0).SetCellValue("MÃ CHI TIẾT ĐƠN HÀNG");
            row6.CreateCell(2).SetCellValue("MÃ SẢN PHẨM");
            row6.CreateCell(4).SetCellValue("TÊN SẢN PHẨM");
            row6.CreateCell(6).SetCellValue("SỐ LƯỢNG");
            row6.CreateCell(8).SetCellValue("THÀNH TIỀN");

            int rowIndex = 7;
            foreach (var item in lichitietdonhang)
            {
                // tao row mới
                var newRow = sheet.CreateRow(rowIndex);

                // set giá trị
                newRow.CreateCell(0).SetCellValue(item.machitietdonhang);
                newRow.CreateCell(2).SetCellValue(item.masanpham);
                newRow.CreateCell(4).SetCellValue(item.tensanpham);
                newRow.CreateCell(6).SetCellValue(item.soluongmua.ToString());
                newRow.CreateCell(8).SetCellValue(item.tongtien.ToString());



                // tăng index
                rowIndex++;
            }
            var rowx = sheet.CreateRow(rowIndex + 1);
            rowx.CreateCell(0).SetCellValue("Tổng tiền: ");
            rowx.CreateCell(2).SetCellValue(tongtien);


            // xong hết thì save file lại

            String filename = "D:\\FIleDoAn\\hoadon.xlsx";
            FileInfo fi = new FileInfo(filename);
            // kiểm tra tệp
            if (fi.Exists)
            {
                fi.Delete();
            }
            FileStream fs = new FileStream(filename, FileMode.CreateNew);
            wb.Write(fs);

            wb.Close();

            //CHuyển file excel sang PDF 

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var wookbook = ExcelFile.Load("D:\\FIleDoAn\\hoadon.xlsx");
            wookbook.Save("D:\\FIleDoAn\\hoadon.pdf");
        }

        //download file hoa don

        public FileResult DownloadHoadon(int id)
        {

            createFile(id);

            byte[] fileBytes = System.IO.File.ReadAllBytes("D:\\FIleDoAn\\hoadon.pdf");
            string fileName = "hoadon.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

    }
}
