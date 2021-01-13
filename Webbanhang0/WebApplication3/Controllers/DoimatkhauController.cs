using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DoimatkhauController : Controller
    {
        private ModelUser ur = new ModelUser();

        // GET: Doimatkhau
        public void xulydoimatkhau()
        {
            String matkhaucu = Request.Form["matkhau"];
            String matkhaumoi = Request.Form["matkhaumoi"];
            String nhaplaimatkhau = Request.Form["nhaplaimatkhaumoi"];
            khachhang kh = new khachhang();
            kh = (khachhang)Session["nguoidung"];
            if (matkhaucu == kh.matkhau)
            {
                if (matkhaumoi == nhaplaimatkhau && matkhaumoi!="")
                {
                    var listkh = ur.khachhang.ToList();
                    foreach (khachhang i in listkh)
                    {
                        if (i.matkhau == matkhaucu &&i.tendangnhap==kh.tendangnhap)
                        {
                            i.matkhau = matkhaumoi;
                            kh.matkhau = matkhaumoi;
                            ur.SaveChanges();
                        }
                    }
                    ViewBag.mess = "Đổi mật khẩu thành công";

                }
                else
                {
                    ViewBag.mess = "Mật khẩu nhập không khớp!!!";
                }

            }
            else
            {
                ViewBag.mess = "Mật khẩu cũ không đúng";
            }

        }
    
    public ActionResult Doimatkhau()
        {
            xulydoimatkhau();
            return View();
        }
    }
}