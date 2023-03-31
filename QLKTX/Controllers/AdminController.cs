using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLKTX.Models;

namespace QLKTX.Controllers
{
    public class AdminController : Controller, IController
    {
        string acc = "admin1";
        KyTucXaEntities db = new KyTucXaEntities();
        // GET: Admin
        ////public ActionResult DangNhap(NhanVien nv)
        ////{
        ////    string encryptedPwd = nv.MatKhau;
        ////    var userPassword = Convert.ToString(ConfigurationManager.AppSettings["config:Password"]);
        ////    var UserName = Convert.ToString(ConfigurationManager.AppSettings["config:Username"]);
        ////    if(encryptedPwd.Equals(userPassword))
        ////    {
        ////        var roles = new string[] { };
        ////        var jwtSecurityToken = 
        ////    }
        ////    return View();
        ////}        
        public ActionResult DanhSachXetDuyet()
        {
            List<DangKyNoiTru> lXetDuyet = db.DangKyNoiTrus.Where(l => l.TrangThai.Equals("Chờ xét duyệt")).ToList();
            return View(lXetDuyet);
        }
        public ActionResult XemThongTinXetDuyet(string maSV)
        {
            return View(db.SinhViens.Single(s => s.MaSV == maSV));
        }
        public ActionResult PhanHoiXetDuyet(string maSV, string trangThai, string ghiChu = null)
        {
            DangKyNoiTru dk = db.DangKyNoiTrus.FirstOrDefault(d => d.MaSV == maSV);
            if (dk == null)
                return RedirectToAction("DanhSachXetDuyet");
            dk.TrangThai = trangThai;
            dk.GhiChu = ghiChu;
            if (trangThai == "Đã xét duyệt")
            {
                try
                {
                    db.TaoHopDong(maSV, acc);
                }
                catch
                {
                    return RedirectToAction("TaoKhongThanhCong");
                }
            }
            db.DangKyNoiTrus.Attach(dk);
            db.Entry(dk).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachXetDuyet");
        }
        public ActionResult DanhSachHopDong()
        {
            return View(db.HopDongs);
        }
        [HttpPost]
        public ActionResult DanhSachHopDong(List<string> loc, string showAll = null)
        {
            List<HopDong> hds = db.HopDongs.ToList();
            if (showAll == "All")
                return View(hds);
            else
            {
                if (loc == null)
                    return View(hds);
                List<HopDong> lhds = new List<HopDong>();
                loc.ForEach(l => lhds.AddRange((from hd in db.HopDongs where hd.TrangThai == l select hd).ToList()));
                ViewBag.l = loc;
                return View(lhds);
            }
        }
        public ActionResult ChiTietHopDong(string maHD)
        {
            return View(db.HopDongs.FirstOrDefault(d => d.MaHopDong == maHD));
        }
        public ActionResult TaoKhongThanhCong()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThanhToanHopDong(string url, string maDH)
        {
            HopDong hd = db.HopDongs.FirstOrDefault(h => h.MaHopDong == maDH);
            hd.DaThanhToan = true;
            db.HopDongs.Attach(hd);
            db.Entry(hd).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Redirect(url);
        }
        public ActionResult XemThongTinSinhVien(string maSV)
        {
            return View(db.SinhViens.FirstOrDefault(s => s.MaSV == maSV));
        }
        public ActionResult DanhSachPhong()
        {
            return View(db.Phongs);
        }
        public ActionResult XemThongTinPhong(string maPhong)
        {
            return View(db.Phongs.FirstOrDefault(p => p.Ma == maPhong));
        }
        [HttpPost]
        public ActionResult DoiTruongPhong(string maPhong, string maSV, string url)
        {
            Phong p = db.Phongs.FirstOrDefault(i => i.Ma == maPhong);
            p.TruongPhong = maSV;
            db.Phongs.Attach(p);
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Redirect(url);
        }

        public ActionResult TaoHoaDon()
        {
            return View(db.DichVus.Where(dv => dv.BatBuoc));
        }
        [HttpPost]
        public ActionResult TaoHoaDon(FormCollection f)
        {
            string maPhong = f["phong"].Trim();
            HoaDon moi = new HoaDon();
            moi.MaHoaDon = "HD" + maPhong + db.HoaDons.Count(hd => hd.MaPhong == maPhong).ToString();
            moi.MaPhong = maPhong;
            moi.NgayTao = DateTime.Now;
            moi.NguoiTao = acc;
            moi.ThanhTien = 0;
            moi.DaThanhToan = false;
            List<ChiTietHoaDon> lCTHD = new List<ChiTietHoaDon>();
            foreach (var item in db.DichVus.Where(dv => dv.BatBuoc))
            {
                int sl = int.Parse(f["Sl" + item.MaDichVu]);
                lCTHD.Add(new ChiTietHoaDon()
                {
                    MaHoaDon = moi.MaHoaDon,
                    MaDichVu = item.MaDichVu,
                    DonGia = item.GiaHienTai,
                    SoLuong = sl,
                });
            }
            db.HoaDons.Add(moi);
            db.ChiTietHoaDons.AddRange(lCTHD);
            db.SaveChanges();
            return View(db.DichVus.Where(dv => dv.BatBuoc));
        }
        public ActionResult DanhSachHoaDon()
        {
            return View();
        }
        public ActionResult ThanhToanHoaDon(string maHoaDon, string url)
        {
            HoaDon hd = db.HoaDons.FirstOrDefault(h => h.MaHoaDon == maHoaDon);
            if (hd != null)
            {
                hd.DaThanhToan = true;
                db.HoaDons.Attach(hd);
                db.Entry(hd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(url);
        }
        public ActionResult XemChiTietHoaDon(string maHoaDon, string url)
        {
            HoaDon hd = db.HoaDons.FirstOrDefault(h => h.MaHoaDon == maHoaDon);
            if (hd != null)
                return View(hd);
            return Redirect(url);
        }
        public ActionResult QuanLyNhanVien()
        {
            return View(db.NhanViens);
        }
        public ActionResult DatLaiMatKhauMatDinh(string tenDangNhap)
        {
            NhanVien nv = db.NhanViens.FirstOrDefault(n => n.TenDangNhap == tenDangNhap);
            if(nv!=null)
            {
                nv.MatKhau = "123";
                db.NhanViens.Attach(nv);
                db.Entry(nv).State = System.Data.Entity.EntityState.Modified;
            }
            return RedirectToAction("QuanLyNhanVien");
        }
        public ActionResult QuanLyViPham()
        {
            return View(db.ViPhams);
        }
    }
}