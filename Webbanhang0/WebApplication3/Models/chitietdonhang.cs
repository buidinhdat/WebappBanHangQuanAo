namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("chitietdonhang")]
    public partial class chitietdonhang
    {
        [Key]
        public int machitietdonhang { get; set; }

        public int masanpham { get; set; }

        public int madonhang { get; set; }

        public int? soluongmua { get; set; }

        [Column(TypeName = "money")]
        public decimal? dongia { get; set; }

        [Column(TypeName = "money")]
        public decimal? tongtien { get; set; }

        [StringLength(100)]
        public string tensanpham { get; set; }

        public virtual donhang donhang { get; set; }

        public virtual sanpham sanpham { get; set; }
    }
}
