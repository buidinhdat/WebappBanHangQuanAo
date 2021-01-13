using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using PagedList.Mvc;
using PagedList;
using System.Data.Entity;
using System.Data;
using System.Net;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private ModelUser ur = new ModelUser();
        public ActionResult Bando()
        {
            return View();
        }
        public ActionResult Xulydangky()

        {
            string hoten = Request.Form["hoten"];
            string diachi = Request.Form["diachi"];
            string email = Request.Form["email"];
            string sodienthoai = Request.Form["sodienthoai"];
            string tendangnhap = Request.Form["tendangnhap"];
            string matkhau = Request.Form["matkhau"];
            string nhaplaimatkhau = Request.Form["nhaplaimatkhau"];
            if (matkhau == nhaplaimatkhau)
            {
                if (hoten == "" || diachi =="" || email == "" ||sodienthoai == ""  || tendangnhap == "" || matkhau == "")
                {
                    ViewBag.mess = "Không được để trống ";
                    return RedirectToAction("Dangky");
                   
                }
                else
                {
                    WebApplication3.Models.khachhang moi = new WebApplication3.Models.khachhang();
                    moi.diachi = diachi;
                    moi.hoten = hoten;
                    moi.email = email;
                    moi.sodienthoai = sodienthoai;
                    moi.tendangnhap = tendangnhap;
                    moi.matkhau = matkhau;
                    moi.quyen = 0;
                    ur.khachhang.Add(moi);
                    ur.SaveChanges();

                    ViewBag.mess = "Đăng ký thành công";
                    return RedirectToAction("Index","Sanphamuser");
                }

            }

            else
            {
                ViewBag.mess = "Mật khẩu không khớp";
                return RedirectToAction("Dangky");

            }
        }

        public ActionResult Dangky()
        {
            
            return View();
        }
        
        public bool xulydangnhap(string username, string pass)
        {
            int dem = 0;
            var kh = ur.khachhang.ToList();
            foreach(WebApplication3.Models.khachhang k in kh)
            {
                if (k.tendangnhap == username && k.matkhau == pass)
                dem = dem + 1;
            }
            if (dem == 0)
                return false;
            else
                return true;

            
        }
        public ActionResult Dangnhapuser()
        {
            string user = Request.Form["mini-username"];
            string pass = Request.Form["mini-password"];

            if (xulydangnhap(user, pass) == true)
            {
                WebApplication3.Models.khachhang nguoidung = new WebApplication3.Models.khachhang();
                var kh = ur.khachhang.ToList();
                try
                {
                    foreach (WebApplication3.Models.khachhang k in kh)
                        if (k.tendangnhap == user &&k.matkhau==pass)
                        {

                            nguoidung.hoten = k.hoten;
                            nguoidung.diachi = k.diachi;
                            nguoidung.sodienthoai = k.sodienthoai;
                            nguoidung.email = k.email;
                            nguoidung.makhachhang = k.makhachhang;
                            nguoidung.tendangnhap = k.tendangnhap;
                            nguoidung.matkhau = k.matkhau;
                            Session.Add("nguoidung", nguoidung);

                        }

                   return  Redirect(Request.UrlReferrer.ToString()); //quay trở lại chính trang vừa rồi
                }
                catch
                {
                    return RedirectToAction("Index","Sanphamuser");
                }
               
            }
            else
                return RedirectToAction("Dangky");
        }
        public ActionResult Taikhoan()
        {
           return View();

        }
        public ActionResult Dangxuat()
        {
            Session.Remove("nguoidung");
            Session.Remove("giohang");
            return RedirectToAction("Index","Sanphamuser");
        }
        public ActionResult Xulycapnhattaikhoan()
        {

            string hoten = Request.Form["hoten"];
            string diachi = Request.Form["diachi"];
            string email = Request.Form["email"];
            string sodienthoai = Request.Form["sodienthoai"];
            string tendangnhap = Request.Form["tendangnhap"];
            if (hoten == "" || diachi == "" || email == "" || sodienthoai == "" || tendangnhap == "")
            {
                Session["thongbao"] = "Không được cập nhật do để trống!";
                return RedirectToAction("/Taikhoan");
                
               
            }
            else
            {
                WebApplication3.Models.khachhang kh = new WebApplication3.Models.khachhang();
                kh = (WebApplication3.Models.khachhang)Session["nguoidung"];
                var khachhang = ur.khachhang.ToList();
                foreach (WebApplication3.Models.khachhang i in khachhang)
                {
                    if (i.tendangnhap == kh.tendangnhap && i.matkhau == kh.matkhau)
                    {
                        i.hoten = hoten;
                        i.diachi = diachi;
                        i.email = email;
                        i.sodienthoai = sodienthoai;
                        i.tendangnhap = tendangnhap;
                        kh.hoten = i.hoten;
                        kh.diachi = i.diachi;
                        kh.email = i.email;
                        kh.sodienthoai = i.sodienthoai;
                        kh.tendangnhap = i.tendangnhap;
                        ur.SaveChanges();
                    }
                }
                Session["thongbao"] = "Cập nhật dữ liệu thành công!";
            }

            return RedirectToAction("/Taikhoan");


        }
       
        

        public ActionResult Datmuathanhcong()
        {
            return View();
        }


    }
}