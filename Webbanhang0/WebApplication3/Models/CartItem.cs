using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class CartItem
    {
        public string anhsanpham { get; set; }
        public int masanpham { get; set; }
        public string tensanpham { get; set; }
        public float dongia { get; set; }
        public int soluong { get; set; }
        public float thanhtien
        {
            get
            {
                return soluong * dongia;
            }
        }
    }
}