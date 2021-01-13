namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("danhgia")]
    public partial class danhgia
    {
        [Key]
        public int madanhgia { get; set; }

        public int? masanpham { get; set; }

        public int? makhachhang { get; set; }

        [StringLength(4000)]
        public string noidung { get; set; }
    }
}
