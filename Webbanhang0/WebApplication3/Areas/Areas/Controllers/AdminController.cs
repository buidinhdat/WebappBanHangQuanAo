using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Areas.Areas.Models;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication3.Areas.Areas.Controllers
{
    public class AdminController : Controller
    {
        // GET: Areas/Admin
        private ModelAdmin db = new ModelAdmin();
        public ActionResult Adminlogin()
        {
            Session["Tendangnhap"] = null;
            Session["Tennguoidung"] = null;
            return View();
        }
       
        //Hiển thị danh sách sản phẩm 
        
       
       

        // Sau đây là các hàm xử lý việc đăng nhập cho admin

        public bool Dangnhapadmin(string user, string pass)
        {
            var khach = db.khachhang.ToList();
            int dem = 0;
            int? quyen = 1;
            foreach(khachhang k in khach)
            {

                if (k.tendangnhap == user && k.matkhau == pass && k.quyen==quyen)
                    dem = dem + 1;
                
            }
            if (dem == 1)
                return true;
            else
                return false;
        }
        public ActionResult Xulydangnhap()
        {
            string user = Request.Form["Tendangnhap"];
            string password = Request.Form["Matkhau"];

            if (Dangnhapadmin(user, password) == true)
            {
                Session["Tendangnhap"] = user;


                return RedirectToAction("Trangchuadmin");
            }

            else
            {
               

                return RedirectToAction("Adminlogin");
            }
        }
        public ActionResult Timkiemdoanhthutheongay()
        {
            try
            {
                string startdate = Request.QueryString["startdate"];
                string enddate = Request.QueryString["enddate"];
                List<donhang> lidh = new List<donhang>();
                lidh = db.donhang.ToList();
                List<donhang> lidhsearch = new List<donhang>();
                foreach (donhang i in lidh)
                {
                    DateTime start = Convert.ToDateTime(startdate);
                    DateTime end = Convert.ToDateTime(enddate);
                    if (DateTime.Compare((DateTime)i.ngaydathang, start) > 0 && DateTime.Compare((DateTime)i.ngaydathang, end) < 0)
                    {
                        lidhsearch.Add(i);
                    }

                }
                double doanhthu = 0.0;
                foreach (donhang d in lidhsearch)
                {
                    doanhthu = doanhthu + (double)d.tongtienthanhtoan;
                }
                ViewData["doanhthu"] = doanhthu.ToString();
                List<chitietdonhang> ctdh = new List<chitietdonhang>();
                List<chitietdonhang> lictdh = new List<chitietdonhang>();
                ctdh = db.chitietdonhang.ToList();
                foreach (chitietdonhang item in ctdh)
                {
                    foreach (donhang dh in lidhsearch)
                    {
                        if (item.madonhang == dh.madonhang)
                        {
                            lictdh.Add(item);
                        }
                    }
                }
                ViewBag.listdonhanghomnay = lidhsearch;
                ViewBag.listchitietdonhanghomnay = lictdh;

                return View();
            }
            catch
            {
                return RedirectToAction("Trangchuadmin");
            }

        }
        public ActionResult Timkiemdoanhthutheothang()
        {
            try
            {

                int month = int.Parse(Request.QueryString["month"]);
               
                List<donhang> lidh = new List<donhang>();
                lidh = db.donhang.ToList();
                List<donhang> lidhsearch = new List<donhang>();
                foreach (donhang i in lidh)
                {
                    DateTime date = (DateTime)i.ngaydathang;
                    if (date.Month==month)
                    {
                        lidhsearch.Add(i);
                    }

                }
                double doanhthu = 0.0;
                foreach (donhang d in lidhsearch)
                {
                    doanhthu = doanhthu + (double)d.tongtienthanhtoan;
                }
                ViewData["doanhthu"] = doanhthu.ToString();
                List<chitietdonhang> ctdh = new List<chitietdonhang>();
                List<chitietdonhang> lictdh = new List<chitietdonhang>();
                ctdh = db.chitietdonhang.ToList();
                foreach (chitietdonhang item in ctdh)
                {
                    foreach (donhang dh in lidhsearch)
                    {
                        if (item.madonhang == dh.madonhang)
                        {
                            lictdh.Add(item);
                        }
                    }
                }
                ViewBag.listdonhanghomnay = lidhsearch;
                ViewBag.listchitietdonhanghomnay = lictdh;

                return View();
            }
            catch
            {
                return RedirectToAction("Trangchuadmin");
            }
        }


        public ActionResult Trangchuadmin()
        {
           
       
                try
                {
                    var khach = db.khachhang.ToList();
                    foreach (khachhang k in khach)
                    {
                        if (k.tendangnhap == Session["Tendangnhap"].ToString())
                        {
                            Session["Tennguoidung"] = k.hoten;
                        }

                    }
                    // Xử lý hiển thị  các đơn hàng trong ngày
                    string daynow = DateTime.Now.ToString("dd/MM/yyyy");
                    ViewBag.date = daynow;
                    List<donhang> lidhhomnay = new List<donhang>();
                    var donhang = db.donhang.ToList();
                    foreach (donhang dh in donhang)
                    {
                        string date = ((DateTime)dh.ngaydathang).ToString("dd/MM/yyyy");

                        if (daynow == date)
                        {
                            lidhhomnay.Add(dh);
                        }

                    }
                    float doanhthu = 0;
                    foreach (donhang d in lidhhomnay)
                    {
                        doanhthu = doanhthu + (float)d.tongtienthanhtoan;
                    }
                    ViewData["doanhthu"] = doanhthu;
                    ViewBag.listdonhanghomnay = lidhhomnay;

                    // Xử lý hiển thị các chi tiết đơn hàng trong ngày
                    List<chitietdonhang> lictdh = new List<chitietdonhang>();
                    var chitietdonhang = db.chitietdonhang.ToList();
                    foreach (chitietdonhang ctdh in chitietdonhang)
                    {
                        foreach (donhang dh in lidhhomnay)
                        {
                            if (ctdh.madonhang == dh.madonhang)
                            {
                                lictdh.Add(ctdh);
                            }
                        }
                    }
                    ViewBag.listchitietdonhanghomnay = lictdh;
                    return View();
                }
                catch
                {
                    return RedirectToAction("Adminlogin");
                }

            



        }

        //Sau đây là xử lý các vấn đề về quản lý tài khoản như thêm sửa xóa
      
        public ActionResult Dangxuat()
        {
            Session["Tennguoidung"] = null;
            return RedirectToAction("Adminlogin");
        }

        

       
    }
}