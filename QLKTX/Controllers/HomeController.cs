using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QLKTX.Models;

namespace QLKTX.Controllers
{
    public class HomeController : Controller
    {
        KyTucXaEntities db = new KyTucXaEntities();
        [AllowAnonymous]
        public ActionResult TrangChu()
        {
            ViewBag.Title = "Home";
            return View();
        }
        public ActionResult DangNhap()
        {
            ViewBag.Title = "Đăng Nhập";
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string maSV, string matKhau,string ReturnUrl = "Home/TrangChu")
        {
            //////ViewBag.Title = "Đăng Nhập";
            try {
                var SV = db.SinhViens.FirstOrDefault(x => x.MaSV == maSV);
                if (SV != null)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                                           "TaiKhoanDangNhap", DateTime.Now,
                                                           DateTime.Now.AddDays(30), false, SV.MaSV,
                                                           FormsAuthentication.FormsCookiePath);
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    SessionLoginUser.Create(SV);
                    return Redirect(ReturnUrl);
                }
                ViewBag.loiDN = "Mã sinh viên hoặc mặt khẩu không đúng!";
                return Redirect(ReturnUrl);

            } catch (Exception E){
                return Json(new
                {
                    message = E.Message
                }); 
            }
            
        }

        public ActionResult DangXuat()
        {
            SessionLoginUser.Clear();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("TrangChu");
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection f)
        {
            string maSV  = ViewBag.maSV = f["maSV"].Trim();
            string ho = ViewBag.ho = f["ho"].Trim();
            string ten = ViewBag.ten = f["ten"].Trim();
            string matKhau = f["matKhau"].Trim();
            string nlMatKhau = f["nlMatKhau"].Trim();
            if(maSV==null || maSV==""||ho == null || ho == "" || ten==null|| ten == "" || matKhau == null || matKhau==""||nlMatKhau==null||nlMatKhau=="")
            {
                ViewBag.loiDK = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }
            if(maSV.Length !=10 || !maSV.All(char.IsDigit))
            {
                ViewBag.loiDK = "Mã sinh viên không đúng định dạng!";
                ViewBag.maSV = "";
                return View();
            }    
            if(db.SinhViens.FirstOrDefault(sv=>sv.MaSV==maSV)!=null)
            {
                ViewBag.loiDK = "Mã sinh viên đã tồn tại!";
                ViewBag.maSV = "";
                return View();
            }    
            if(matKhau!=nlMatKhau)
            {
                ViewBag.loiDK = "Mật khẩu nhập lại không trùng khớp!";
                return View();
            }
            SinhVien svMoi = new SinhVien()
            {
                MaSV = maSV,
                Ho = ho,
                Ten = ten,
                MatKhau = matKhau,
                TrangThai = "Chưa xét duyệt"
            };            
            db.SinhViens.Add(svMoi);
            db.SaveChanges();
            string url = Session["url"].ToString();
            Session.Remove("url");
            Session["SinhVien"] = svMoi;
            return Redirect(url);
        }
        public ActionResult DangKy(string url)
        {


            Session["url"] = url;
            return View();
        }

    }
}
