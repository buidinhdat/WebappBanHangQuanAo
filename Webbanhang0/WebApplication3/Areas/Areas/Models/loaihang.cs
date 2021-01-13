namespace WebApplication3.Areas.Areas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("loaihang")]
    public partial class loaihang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public loaihang()
        {
            nhomhang = new HashSet<nhomhang>();
        }

        [Key]
        [DisplayName("Mã loại")]
        public int maloai { get; set; }

        [StringLength(100)]
        [DisplayName("Tên loại")]
        public string tenloai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<nhomhang> nhomhang { get; set; }
    }
}
