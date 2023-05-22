using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLKTX.Models;

namespace QLKTX.Controllers
{
    public class ThongBaoController : Controller
    {
        KyTucXaEntities db = new KyTucXaEntities();
        // GET: ThongBao
        public ActionResult ThongBaoHoaDon(string maHoaDon)
        {
            return RedirectToAction("XemChiTietHoaDon", "User", new { maHoaDon = maHoaDon });
        }

        public ActionResult ThongBaoXetDuyetDangKyThangCong()
        {
            return View();
        }

        public ActionResult ThongBaoMacLoiViPham()
        {
            return View();
        }

        public ActionResult ThongBaoXinGiaHanHopDongThanhCong()
        {
            return View();
        }

        public ActionResult ThongBaoTroThanhThanhVienCuaKyTucXa()
        {
            return View();
        }

        public ActionResult ThongBaoHopDongSapHetThoiHan()
        {
            return View();
        }
        public ActionResult ThongBaoBiDuoiKhoiKyTucXa()
        {
            return View();
        }
        public ActionResult ThongBaoDaXuLyKhaiBaoHong()
        {
            return View();
        }
    }
}