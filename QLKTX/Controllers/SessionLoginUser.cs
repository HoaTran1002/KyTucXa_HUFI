using QLKTX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace QLKTX.Controllers
{
    public static class SessionLoginUser
    {
        private static string name = "SessionUser";
        public static SinhVien GetSession
        {
            get
            {
                SinhVien sv = HttpContext.Current.Session[name] as SinhVien;                
                if (sv == null && HttpContext.Current.Request.IsAuthenticated)
                {
                    if(sv==null)
                    {
                        var cookie = HttpContext.Current.Request.Cookies["TaiKhoanDangNhap"];
                        string maSV = FormsAuthentication.Decrypt(cookie.Value).UserData;
                        sv = new Models.KyTucXaEntities().SinhViens.FirstOrDefault(s => s.MaSV == maSV);
                        HttpContext.Current.Session[name] = sv;
                    }    
                }                                                 
                return sv;
            }
        }
        public static bool Create(SinhVien sv)
        {
            if (HttpContext.Current.Session[name] == null)
            {
                HttpContext.Current.Session[name] = sv;
                return true;                    
            }
            return false;

        }
        public static void Clear()
        {
            HttpContext.Current.Session.Remove(name);            
        }
    }
}