using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class SanphamuserController : Controller
    {
        private ModelUser db = new ModelUser();

        // GET: Sanphamuser
        public ActionResult Index(int? page ,int? id, string timkiem) // hien thi sản phẩm , theo danh mục, tìm kiếm theo tên

        {
            if(timkiem==null)
            {
                if (id == null)
                {

                    if (page == null) page = 1;
                    var links = (from l in db.sanpham select l).OrderBy(x => x.masanpham);
                    int pageSize = 12;
                    int pageNumber = (page ?? 1);
                    PagedList<sanpham> model = new PagedList<sanpham>(links, pageNumber, pageSize);

                    return View(model);
                }
                else
                {

                    if (page == null) page = 1;
                    var links = (from l in db.sanpham select l).Where(x => x.manhomhang == id).OrderBy(x => x.masanpham);
                    int pageSize = 12;
                    int pageNumber = (page ?? 1);
                    PagedList<sanpham> model = new PagedList<sanpham>(links, pageNumber, pageSize);

                    return View(model);
                }

            }
            else
            {
                if (page == null) page = 1;
                var links = (from l in db.sanpham select l).Where(s => s.tensanpham.Contains(timkiem)).OrderBy(x => x.masanpham);
                int pageSize = 12;
                int pageNumber = (page ?? 1);
                PagedList<sanpham> model = new PagedList<sanpham>(links, pageNumber, pageSize);

                return View(model);
            }
           
            
        }

        // GET: Sanphamuser/Details/5
        public ActionResult Details(int? id)
        {
            ViewData["lidanhgia"] = db.danhgia.ToList();
            ViewData["likhachhang"] = db.khachhang.ToList();
            ViewBag.masp = id;

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
       public ActionResult danhgiasanpham(int? id)
        {

             khachhang k = (khachhang)Session["nguoidung"];
                danhgia i = new danhgia();
                i.makhachhang = k.makhachhang;
                i.masanpham = id;
                i.noidung = Request.Form["noidung"];
                db.danhgia.Add(i);
                db.SaveChanges();



            return RedirectToAction("Details/"+id);
        }
        public ActionResult Sanphammoi()
        {
            ViewBag.lisp = db.sanpham.ToList();
                
               
                return View();
           
        }
        public ActionResult Themvaogiohang(int id)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa
            int soluong = int.Parse(Request.Form["qty"].ToString());
            sanpham x = db.sanpham.Find(id);
            if (soluong < x.soluong)
            {
                if (giohang.FirstOrDefault(m => m.masanpham == id) == null) // ko co sp nay trong gio hang
                {
                    sanpham sp = db.sanpham.Find(id);  // tim sp theo mã sản phẩm

                    CartItem newItem = new CartItem();

                    newItem.masanpham = id;
                    newItem.tensanpham = sp.tensanpham;
                    newItem.soluong = int.Parse(Request.Form["qty"].ToString());
                    newItem.anhsanpham = sp.anhsanpham;
                    newItem.dongia = float.Parse(sp.dongia.ToString());

                    // Tạo ra 1 CartItem mới

                    giohang.Add(newItem);  // Thêm CartItem vào giỏ 
                }
                else
                {
                    // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                    CartItem cardItem = giohang.FirstOrDefault(m => m.masanpham == id);
                    cardItem.soluong = cardItem.soluong + int.Parse(Request.Form["qty"].ToString());
                }
                return RedirectToAction("Giohang", "Giohang");


            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }


        // GET: Sanphamuser/Create

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
