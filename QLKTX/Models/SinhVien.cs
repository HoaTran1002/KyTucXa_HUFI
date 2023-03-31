//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLKTX.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SinhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SinhVien()
        {
            this.HopDongs = new HashSet<HopDong>();
            this.Phongs = new HashSet<Phong>();
            this.SinhVienViPhams = new HashSet<SinhVienViPham>();
            this.SuDungDichVuDons = new HashSet<SuDungDichVuDon>();
            this.YeuCauSuaChuas = new HashSet<YeuCauSuaChua>();
        }
    
        public string MaSV { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string AnhDaiDien { get; set; }
        public string Email { get; set; }
        public string GioiTinh { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string QueQuan { get; set; }
        public string SoCanCuoc { get; set; }
        public string SoDienThoai { get; set; }
        public string Lop { get; set; }
        public string MaPhong { get; set; }
        public string TrangThai { get; set; }
        public string MatKhau { get; set; }
    
        public virtual DangKyNoiTru DangKyNoiTru { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HopDong> HopDongs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phong> Phongs { get; set; }
        public virtual Phong Phong { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SinhVienViPham> SinhVienViPhams { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuDungDichVuDon> SuDungDichVuDons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YeuCauSuaChua> YeuCauSuaChuas { get; set; }
    }
}
