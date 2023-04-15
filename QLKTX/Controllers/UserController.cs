using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using QLKTX.Models;

namespace QLKTX.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        KyTucXaEntities db = new KyTucXaEntities();
        // GET: User
        public ActionResult TaiKhoanCaNhan()
        {
            return View();
        }
        public ActionResult ThongTinSinhVien()
        {
            SinhVien sv = SessionLoginUser.GetSession;
            return View(sv);
        }
        [HttpPost]
        public ActionResult ThongTinSinhVien(SinhVien sv)
        {
            db.SinhViens.Attach(sv);
            db.Entry(sv).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            SessionLoginUser.Clear();
            return View(SessionLoginUser.GetSession);
        }
        public ActionResult DangKyNoiTru()
        {
            SinhVien sv = SessionLoginUser.GetSession;
            if (sv.GioiTinh == null || sv.QueQuan == null || sv.SoCanCuoc == null || sv.SoDienThoai == null || sv.Lop == null)
            {
                ViewBag.ThongBao = "Vui Lòng cập nhật đầy đủ thông tin của bạn để tiến hành đăng ký!";
            }
            List<Phong> lPhong;
            string makhu = "A";
            if (sv.GioiTinh != null && sv.GioiTinh == "Nữ")
                makhu = "B";
            lPhong = db.Phongs.Where(p => p.SoLuongTrong > 0 && p.Tang.MaKhu == makhu).ToList();
            Session["lPhong"] = lPhong;
            return View(sv);
        }
        public ActionResult XacNhanDangKy(string MaPhong)
        {
            SinhVien sv = SessionLoginUser.GetSession;
            if (ModelState.IsValid)
            {
                Session.Remove("lPhong");
                ViewBag.Phong = db.Phongs.Single(p => p.Ma == MaPhong);
                return View(sv);
            }
            else
            {
                return RedirectToAction("DangKyNoiTru");
            }
        }
        [HttpPost]
        public ActionResult XacNhanDangKy(string MaPhong, string checkXacNhan)
        {
            SinhVien sv = SessionLoginUser.GetSession;
            if (checkXacNhan == null)
                return View(sv);
            db.TaoDangKyNoiTru(sv.MaSV, MaPhong);
            return RedirectToAction("DangKyThanhCong");
        }
        public ActionResult DangKyThanhCong()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HuyDangKyNoiTru(string ghiChu, string url = null)
        {
            SinhVien sv = SessionLoginUser.GetSession;
            DangKyNoiTru dk = db.DangKyNoiTrus.FirstOrDefault(d => d.MaSV == sv.MaSV);
            if (dk != null)
            {
                dk.TrangThai = "Bị hủy";
                dk.GhiChu = ghiChu;
                db.DangKyNoiTrus.Attach(dk);
                db.Entry(dk).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            if (url == null)
                return RedirectToAction("ThongTinSinhVien");
            return Redirect(url);
        }
        public ActionResult TrangChu()
        {
            return View(SessionLoginUser.GetSession);
        }
        public ActionResult QuanLyPhong()
        {
            return View();
        }
        public ActionResult DangKyLuuTruHe()
        {

            return View();
        }
    }
}