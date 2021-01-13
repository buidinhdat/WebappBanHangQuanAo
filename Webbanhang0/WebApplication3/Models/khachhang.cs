namespace WebApplication3.Models
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
        public int makhachhang { get; set; }

        [StringLength(100)]
        public string tendangnhap { get; set; }

        [StringLength(100)]
        public string matkhau { get; set; }

        [StringLength(100)]
        public string hoten { get; set; }

        [StringLength(100)]
        public string sodienthoai { get; set; }

        [StringLength(100)]
        public string diachi { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        public int? quyen { get; set; }
    }
}
