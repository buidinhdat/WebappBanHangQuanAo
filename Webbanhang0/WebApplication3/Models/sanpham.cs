namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sanpham")]
    public partial class sanpham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sanpham()
        {
            chitietdonhang = new HashSet<chitietdonhang>();
        }

        [Key]
        public int masanpham { get; set; }

        public int manhasanxuat { get; set; }

        public int manhomhang { get; set; }

        [StringLength(100)]
        public string tensanpham { get; set; }

        [Column(TypeName = "money")]
        public decimal? dongia { get; set; }

        public int? soluong { get; set; }

        [StringLength(100)]
        public string anhsanpham { get; set; }

        public string chitiet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietdonhang> chitietdonhang { get; set; }

        public virtual nhasanxuat nhasanxuat { get; set; }

        public virtual nhomhang nhomhang { get; set; }
    }
}
