namespace WebApplication3.Areas.Areas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        [DisplayName("Mã đơn hàng")]
        public int madonhang { get; set; }

        [DisplayName("Mã phương thức thanh toán")]

        public int maphuongthucthanhtoan { get; set; }

        [StringLength(100)]
        [DisplayName("Tên khách hàng")]

        public string tenkhachhang { get; set; }

        [DisplayName("Ngày đặt hàng")]

        public DateTime? ngaydathang { get; set; }

        [StringLength(100)]
        [DisplayName("Địa chỉ")]

        public string diachi { get; set; }

        [StringLength(100)]
        [DisplayName("Email")]

        public string email { get; set; }

        [StringLength(100)]
        [DisplayName("Tình trạng")]

        public string tinhtrang { get; set; }

        [StringLength(100)]
        [DisplayName("Ghi chú")]

        public string ghichu { get; set; }
        [Column(TypeName = "money")]
        
        [DisplayName("Tổng tiền")]
        public decimal? tongtienthanhtoan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietdonhang> chitietdonhang { get; set; }

        public virtual thanhtoan thanhtoan { get; set; }
    }
}
