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
    
    public partial class DichVuPhongCoChiSo
    {
        public string MaDichVu { get; set; }
        public string MaPhong { get; set; }
        public int ChiSoHienTai { get; set; }
    
        public virtual DichVu DichVu { get; set; }
        public virtual Phong Phong { get; set; }
    }
}
