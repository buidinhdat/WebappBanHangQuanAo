namespace WebApplication3.Areas.Areas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("thanhtoan")]
    public partial class thanhtoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public thanhtoan()
        {
            donhang = new HashSet<donhang>();
        }

        [Key]
        [DisplayName("Mã phương thức thanh toán")]
        public int maphuongthucthanhtoan { get; set; }

        [StringLength(100)]
        [DisplayName("Tên phương thức thanh toán")]
        public string tenphuongthucthanhtoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<donhang> donhang { get; set; }
    }
}
