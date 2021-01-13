namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("donhang")]
    public partial class donhang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public donhang()
        {
            chitietdonhang = new HashSet<chitietdonhang>();
        }

        [Key]
        public int madonhang { get; set; }

        public int maphuongthucthanhtoan { get; set; }

        [StringLength(100)]
        public string tenkhachhang { get; set; }

        public DateTime? ngaydathang { get; set; }

        [StringLength(100)]
        public string diachi { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(100)]
        public string tinhtrang { get; set; }

        [StringLength(100)]
        public string ghichu { get; set; }
        [Column(TypeName = "money")]
        public decimal? tongtienthanhtoan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietdonhang> chitietdonhang { get; set; }

        public virtual thanhtoan thanhtoan { get; set; }
    }
}
