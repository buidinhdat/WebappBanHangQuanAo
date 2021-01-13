using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    
    public class GiohangController : Controller
    {
        // GET: Giohang
        public ModelUser db = new ModelUser();

        public ActionResult Giohang()
        {
            // Xử lý dropdowlist phuong thuc thanh toán bằng selectlist
            List<thanhtoan> phuongttt = db.thanhtoan.ToList();
            SelectList pttt = new SelectList(phuongttt, "maphuongthucthanhtoan", "tenphuongthucthanhtoan");
            ViewBag.listpttt = pttt;

            if (Session["nguoidung"]!=null) // xử lý lỗi chưa khởi tạo session["nguoidung"]
            {
                khachhang kh = (khachhang)Session["nguoidung"];
                ViewData["kh"] = kh;
            }
            else
            {
                khachhang kh = new khachhang();
                kh.hoten = "";
                kh.sodienthoai = "";
                kh.email = "";
                kh.diachi = "";
                kh.makhachhang = 0;
                kh.matkhau = "";
                kh.tendangnhap = "";
                kh.quyen = 0;

                ViewData["kh"] = kh;
            }
            

            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;


            ViewData["lisp"] = giohang;
            float tongtien = 0;
            for(int i=0; i<giohang.Count; i++)
            {
                tongtien = tongtien + giohang[i].thanhtien;
            }
            ViewData["tongtien"] = tongtien;

            return View();
        }


        //Thêm sản phẩm vào giỏ hàng
        public ActionResult Themvaogiohang(int id)
        {


            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.masanpham == id) == null) // ko co sp nay trong gio hang
            {
                sanpham sp = db.sanpham.Find(id);  // tim sp theo mã sản phẩm

                CartItem newItem = new CartItem();

                newItem.masanpham = id;
                newItem.tensanpham = sp.tensanpham;
                newItem.soluong = 1;
                newItem.anhsanpham = sp.anhsanpham;
                newItem.dongia = float.Parse(sp.dongia.ToString());

                 // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.masanpham == id);
                cardItem.soluong++;
            }

            // return lại trang hiện tại
            return Redirect(Request.UrlReferrer.ToString());
        }
        
        public ActionResult Xoasanphamtronggiohang(int?id)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.masanpham == id);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
           

            return RedirectToAction("Giohang");
        }

        // Xóa giỏ hàng
        public ActionResult Xoagiohang()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            for(int i=0; i<giohang.Count; i++)
            {

                giohang.RemoveAt(i);
                i--;
            }

            return RedirectToAction("Giohang");

        }


        // Cập nhật giỏ hàng
        public ActionResult Capnhatsoluong() // lấy dữ liệu từ URL
        {
            int id = int.Parse(Request.QueryString["sanphamID"]);
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemupdate = giohang.FirstOrDefault(m => m.masanpham == id);
            if (itemupdate != null)
            {
                itemupdate.soluong = int.Parse(Request.QueryString["soluong"]);
            }
            return RedirectToAction("Giohang");
        }



        public ActionResult Datmua()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            List<sanpham> lisp = db.sanpham.ToList();
            var donhang = db.donhang.ToList();
            var chitietdonhang = db.chitietdonhang.ToList();
            donhang dh = new donhang();
            dh.email = Request.Form["email"];
            dh.diachi = Request.Form["diachi"];
            dh.ghichu = Request.Form["ghichu"];
            dh.ngaydathang = DateTime.Now;
            dh.maphuongthucthanhtoan =int.Parse( Request.Form["phuongthuc"].ToString());
            dh.tenkhachhang = Request.Form["hoten"];
            dh.tinhtrang = "Đang chờ";
            dh.tongtienthanhtoan = Convert.ToDecimal(Request.Form["tongtienthanhtoan"]);
            db.donhang.Add(dh);
            db.SaveChanges();
            for (int i = 0; i < giohang.Count; i++)
            {
                
                    chitietdonhang ctdh = new chitietdonhang();
                    ctdh.madonhang = dh.madonhang;
                    ctdh.masanpham = giohang[i].masanpham;
                    ctdh.soluongmua = giohang[i].soluong;
                    ctdh.tensanpham = giohang[i].tensanpham;
                    ctdh.dongia = (decimal)giohang[i].dongia;
                    ctdh.tongtien = (decimal)giohang[i].thanhtien;
                    db.chitietdonhang.Add(ctdh);
                // sửa lại số lượng sản phẩm trong csdl sau khi đặt mua
                sanpham sp = db.sanpham.Find(giohang[i].masanpham);
                sp.soluong = sp.soluong - giohang[i].soluong;
                    db.SaveChanges();
                
                
            }
            for (int i = 0; i < giohang.Count; i++)
            {

                giohang.RemoveAt(i);
                i--;
            }



            return RedirectToAction("Datmuathanhcong", "Home");

        }
       
    }
}