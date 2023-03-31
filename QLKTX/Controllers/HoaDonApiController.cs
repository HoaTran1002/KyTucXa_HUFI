using QLKTX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLKTX.Controllers
{
    public class HoaDonApiController : ApiController
    {
        KyTucXaEntities db = new KyTucXaEntities();
        public List<HoaDon> getHoaDons(bool tatCa, bool daThanhToan, bool chuaThanhToan, bool thangHienTai, int nam, int thang, string khu = null, string tang = null, string phong = null)
        {
            List<HoaDon> showHD = new List<HoaDon>();
            if (tatCa)
            {
                foreach(var item in db.HoaDons)
                    showHD.Add(new HoaDon()
                    {
                        MaHoaDon = item.MaHoaDon,
                        MaPhong = item.MaPhong,
                        NgayTao = item.NgayTao,
                        NguoiTao = item.NguoiTao,
                        ThanhTien = item.ThanhTien,
                        DaThanhToan = item.DaThanhToan
                    });
                return showHD; 
            }    
            if (phong == null)
            {
                phong = "";
                if (tang == null)
                {
                    tang = "";
                    if (khu == null)
                        khu = "";
                }
            }
            List<HoaDon> lHoaDon = db.HoaDons.Where(hd => hd.Phong.Tang.Khu.Ma.Contains(khu) && hd.Phong.Tang.Ma.Contains(tang) && hd.Phong.Ma.Contains(phong)).ToList();
            if (!thangHienTai)
            {
                lHoaDon = lHoaDon.Where(hd => hd.NgayTao.Year == nam).ToList();
                if (thang !=  0)
                    lHoaDon = lHoaDon.Where(hd => hd.NgayTao.Month == thang).ToList();                
            }
            else
            {
                int namHT = DateTime.Now.Year;
                int thangHT = DateTime.Now.Month;
                lHoaDon = lHoaDon.Where(hd => hd.NgayTao.Year == namHT && hd.NgayTao.Month == thangHT).ToList();
            }
            if ((daThanhToan && !chuaThanhToan) || (!daThanhToan && chuaThanhToan))
                lHoaDon = lHoaDon.Where(hd => hd.DaThanhToan == daThanhToan).ToList();            
            lHoaDon.ForEach(item =>
            {
                showHD.Add(new HoaDon()
                {
                    MaHoaDon = item.MaHoaDon,
                    MaPhong = item.MaPhong,
                    NgayTao = item.NgayTao,
                    NguoiTao = item.NguoiTao,
                    ThanhTien = item.ThanhTien,
                    DaThanhToan = item.DaThanhToan
                });
            });
            return showHD;
        }
    }
}
