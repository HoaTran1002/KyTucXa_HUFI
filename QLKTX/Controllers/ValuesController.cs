using QLKTX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLKTX.Controllers
{
    public class ValuesController : ApiController
    {
        KyTucXaEntities db = new KyTucXaEntities();
        public List<Khu> getKhus()
        {
            List<Khu> lKhu = new List<Khu>();
            foreach (var k in db.Khus)
            {
                lKhu.Add(new Khu()
                {
                    Ma = k.Ma,
                    Ten = k.Ten                    
                });
            }
            return lKhu;
        }
        public List<Tang> getTangs(string maKhu)
        {
            List<Tang> lTang = new List<Tang>();
            foreach (var t in db.Tangs.Where(t=>t.MaKhu==maKhu))
                lTang.Add(new Tang()
                {
                    MaKhu = t.MaKhu,
                    Ma = t.Ma,
                    Ten = t.Ten
                });
            return lTang;
        }
        public List<Phong> getPhongs(string maTang)
        {
            List<Phong> lPhong = new List<Phong>();
            foreach (var p in db.Phongs.Where(p => p.MaTang == maTang))
                lPhong.Add(new Phong()
                {
                    Tang = p.Tang,
                    Ma = p.Ma,
                    MaTang = p.MaTang,
                    DangSuaChua = p.DangSuaChua,
                    TruongPhong = p.TruongPhong,
                    Ten = p.Ten
                });
            return lPhong;
        }
        public Phong getPhong(string maPhong)
        {
            Phong p = db.Phongs.FirstOrDefault(i => i.Ma == maPhong);
            Phong pp = new Phong()
            {                
                Ma = p.Ma,
                MaTang = p.MaTang,
                DangSuaChua = p.DangSuaChua,
                TruongPhong = p.TruongPhong,
                Ten = p.Ten
            };
            return pp;
        }
        public List<ThongKeDichVuDonMoiPhong> getThongKeDVDonPhong(string ma)
        {
            return db.ThongKeDichVuDonMoiPhongs.Where(p => p.MaPhong == ma).ToList();
        }
        public List<DichVuPhongCoChiSo> getDichVuPhongCoChiSo(string maP)
        {
            List<DichVuPhongCoChiSo> lDVPhong = new List<DichVuPhongCoChiSo>();
            foreach(var item in db.DichVuPhongCoChiSoes.Where(dv => dv.MaPhong == maP))
            {
                lDVPhong.Add(new DichVuPhongCoChiSo()
                {
                    DichVu = item.DichVu,
                    MaDichVu = item.MaDichVu,
                    ChiSoHienTai = item.ChiSoHienTai,
                    MaPhong = item.MaPhong
                });
            }
            return lDVPhong;
        }       
    }
}
