//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeThongThueXe.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLHeThongThueXeEntities2 : DbContext
    {
        public QLHeThongThueXeEntities2()
            : base("name=QLHeThongThueXeEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DongXe> DongXes { get; set; }
        public virtual DbSet<HangXe> HangXes { get; set; }
        public virtual DbSet<HopDongThueXe> HopDongThueXes { get; set; }
        public virtual DbSet<LoaiUser> LoaiUsers { get; set; }
        public virtual DbSet<SoDatXe> SoDatXes { get; set; }
        public virtual DbSet<SoXe> SoXes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TrangThaiHopDong> TrangThaiHopDongs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Xe> Xes { get; set; }
        public virtual DbSet<ThamSo> ThamSoes { get; set; }
    }
}
