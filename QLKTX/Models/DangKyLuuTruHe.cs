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
    
    public partial class DangKyLuuTruHe
    {
        public string MaHopDong { get; set; }
        public System.DateTime NgayBatDau { get; set; }
        public System.DateTime NgayKetThuc { get; set; }
        public bool DaThanhToan { get; set; }
    
        public virtual HopDong HopDong { get; set; }
    }
}
