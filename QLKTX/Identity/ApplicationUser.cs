using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using QLKTX.Models;

namespace QLKTX.Identity
{
    public class ApplicationUser : IdentityUser
    {
      NhanVien nhanVien;
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {          
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public ApplicationUser()
        {
            this.HoaDons = new HashSet<HoaDon>();
            this.HopDongs = new HashSet<HopDong>();
        }
        public ApplicationUser(NhanVien nhanVien)
        {
            this.nhanVien = nhanVien;
            this.HoaDons = new HashSet<HoaDon>();
            this.HopDongs = new HashSet<HopDong>();
        }

        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string SoDienThoai { get; set; }
        public string ChucVu { get; set;  }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HopDong> HopDongs { get; set; }
    }
}