namespace WebApplication3.Areas.Areas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("khachhang")]
    public partial class khachhang
    {
        [Key]
        [Display(Name ="Mã khách hàng")]
        public int makhachhang { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [StringLength(100)]
        public string tendangnhap { get; set; }
        
        [Display(Name = "Mật khẩu")]
        [StringLength(100)]
        public string matkhau { get; set; }

        [Display(Name = "Họ tên khách hàng")]
        [StringLength(100)]
        public string hoten { get; set; }

        [Display(Name = "Số điện thoại")]
        [StringLength(100)]
        public string sodienthoai { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(100)]
        public string diachi { get; set; }

        [Display(Name = "Email")]
        [StringLength(100)]
        public string email { get; set; }

        [Display(Name = "Quyền")]
        public int? quyen { get; set; }
    }
}
