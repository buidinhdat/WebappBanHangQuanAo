namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
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
        public int manhasanxuat { get; set; }

        [StringLength(100)]
        public string tennhasanxuat { get; set; }

        [StringLength(100)]
        public string diachinhasanxuat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sanpham> sanpham { get; set; }
    }
}
