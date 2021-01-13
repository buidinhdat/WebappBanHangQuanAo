namespace WebApplication3.Areas.Areas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("nhasanxuat")]
    public partial class nhasanxuat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public nhasanxuat()
        {
            sanpham = new HashSet<sanpham>();
        }

        [Key]
        [DisplayName("Mã nhà sản xuất")]
        public int manhasanxuat { get; set; }

        [StringLength(100)]
        [DisplayName("Tên nhà sản xuất")]
        public string tennhasanxuat { get; set; }

        [StringLength(100)]
        [DisplayName("Địa chỉ nhà sản xuất")]
        public string diachinhasanxuat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sanpham> sanpham { get; set; }
    }
}
