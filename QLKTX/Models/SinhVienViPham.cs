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
    
    public partial class SinhVienViPham
    {
        public string MaSV { get; set; }
        public string MaViPham { get; set; }
        public System.DateTime ThoiGianViPham { get; set; }
        public string MaHinhPhat { get; set; }
        public string NguoiTao { get; set; }
        public string GhiChu { get; set; }
        public bool DaGiaiQuyet { get; set; }
    
        public virtual SinhVien SinhVien { get; set; }
        public virtual ViPham ViPham { get; set; }
    }
}
