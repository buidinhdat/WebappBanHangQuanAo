namespace WebApplication3.Areas.Areas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        [DisplayName("Mã sản phẩm")]
        public int masanpham { get; set; }
        [DisplayName("Mã nhà xuất")]
        public int manhasanxuat { get; set; }
        [DisplayName("Mã nhóm hàng")]
        public int manhomhang { get; set; }

        [StringLength(100)]
        [DisplayName("Tên sản phẩm")]
        public string tensanpham { get; set; }

        [DisplayName("Đơn giá")]
        [Column(TypeName = "money")]
        public decimal? dongia { get; set; }

        [DisplayName("Số lượng")]
        public int? soluong { get; set; }

        [DisplayName("Ảnh")]
        [StringLength(100)]
        public string anhsanpham { get; set; }

        [DisplayName("Chi Tiết")]
        public string chitiet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietdonhang> chitietdonhang { get; set; }

        public virtual nhasanxuat nhasanxuat { get; set; }

        public virtual nhomhang nhomhang { get; set; }
    }
}
