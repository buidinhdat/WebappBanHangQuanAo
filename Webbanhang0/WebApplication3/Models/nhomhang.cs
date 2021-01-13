namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("nhomhang")]
    public partial class nhomhang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public nhomhang()
        {
            sanpham = new HashSet<sanpham>();
        }

        [Key]
        public int manhomhang { get; set; }

        public int maloai { get; set; }

        [StringLength(100)]
        public string tennhomhang { get; set; }

        public virtual loaihang loaihang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sanpham> sanpham { get; set; }
    }
}
