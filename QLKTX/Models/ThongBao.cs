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
    
    public partial class ThongBao
    {
        public string MaThongBao { get; set; }
        public string NguoiNhan { get; set; }
        public System.DateTime NgayTao { get; set; }
        public string MaLoaiThongBao { get; set; }
        public string NoiDung { get; set; }
        public string ChuoiBien { get; set; }
        public bool DaXem { get; set; }
    
        public virtual LoaiThongBao LoaiThongBao { get; set; }
        public virtual SinhVien SinhVien { get; set; }
    }
}
