namespace WebApplication3.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelUser : DbContext
    {
        public ModelUser()
            : base("name=ModelUser")
        {
        }
        public virtual DbSet<danhgia> danhgia { get; set; }
        public virtual DbSet<chitietdonhang> chitietdonhang { get; set; }
        public virtual DbSet<donhang> donhang { get; set; }
        public virtual DbSet<khachhang> khachhang { get; set; }
        public virtual DbSet<loaihang> loaihang { get; set; }
        public virtual DbSet<nhasanxuat> nhasanxuat { get; set; }
        public virtual DbSet<nhomhang> nhomhang { get; set; }
        public virtual DbSet<sanpham> sanpham { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<thanhtoan> thanhtoan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<chitietdonhang>()
                .Property(e => e.dongia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<chitietdonhang>()
                .Property(e => e.tongtien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<donhang>()
                .HasMany(e => e.chitietdonhang)
                .WithRequired(e => e.donhang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<loaihang>()
                .HasMany(e => e.nhomhang)
                .WithRequired(e => e.loaihang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<nhasanxuat>()
                .HasMany(e => e.sanpham)
                .WithRequired(e => e.nhasanxuat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<nhomhang>()
                .HasMany(e => e.sanpham)
                .WithRequired(e => e.nhomhang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sanpham>()
                .Property(e => e.dongia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<sanpham>()
                .HasMany(e => e.chitietdonhang)
                .WithRequired(e => e.sanpham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<thanhtoan>()
                .HasMany(e => e.donhang)
                .WithRequired(e => e.thanhtoan)
                .WillCascadeOnDelete(false);
        }
    }
}
