using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLKTX.Models;


namespace QLKTX.Controllers
{
    public class ThietBiApiController : ApiController
    {
        KyTucXaEntities db = new KyTucXaEntities();
        // GET: api/ThietBiApi
        [HttpGet]
        public List<ThietBi> GetThietBis()
        {
            List<ThietBi> thietBis = new List<ThietBi>();
            foreach (var item in db.ThietBis)
                thietBis.Add(new ThietBi()
                {
                    MaThietBi = item.MaThietBi,
                    TenThietBi = item.TenThietBi,
                    TongSoLuong = item.TongSoLuong,
                    SoLuongMoiPhong = item.SoLuongMoiPhong
                });
            return thietBis;
        }

        //[HttpGet]
        //public ThietBi GetThietBi(String maThietBi)
        //{
        //    return db.ThietBis.FirstOrDefault(t => t.MaThietBi == maThietBi);
        //}

        [HttpGet]
        public IHttpActionResult GetSoLuongDaYeuCauChuaXuLy(String maThietBi)
        {
            try
            {
                SinhVien sv = SessionLoginUser.GetSession;
                ThietBi tb = db.ThietBis.FirstOrDefault(t => t.MaThietBi == maThietBi);
                int sl = sv.Phong.KhaiBaoHuHongs.Where(t => !t.DaXuLy && t.MaThietBi.Equals(maThietBi)).Sum(t => t.TongSoLuong);
                return Ok(new
                {

                    Success = true,
                    ThietBi = tb,
                    sl = sl,
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {

                    Success = false,
                    msg = "Đã xảy ra lỗi"
                });
            }
        }
        [HttpGet]
        public IHttpActionResult KhaiBaoHuHong(String maThietBi, int soLuong)
        {

            SinhVien sv = SessionLoginUser.GetSession;
            if (sv.MaPhong != null)
            {
                String msg;
                KhaiBaoHuHong khaiBao = sv.Phong.KhaiBaoHuHongs
                    .FirstOrDefault(h => h.MaThietBi.Equals(maThietBi) && !h.DaXuLy);
                if (khaiBao == null)
                {
                    string maKB;
                    string maPhong = sv.Phong.Ma.Trim()+"S";
                    int coutKB = sv.Phong.KhaiBaoHuHongs.Count + 1;
                    do
                    {
                        maKB = "KBHH" + maPhong + (coutKB++).ToString();
                    }
                    while (sv.Phong.KhaiBaoHuHongs.Count(k => k.MaKhaiBao.Equals(maKB)) != 0);
                    khaiBao = new KhaiBaoHuHong()
                    {
                        MaKhaiBao = maKB,
                        MaPhong = sv.MaPhong,
                        MaThietBi = maThietBi,
                        NgayYeuCau = DateTime.Now,
                        TongSoLuong = soLuong,
                        DaXuLy = false,

                    };
                    db.KhaiBaoHuHongs.Add(khaiBao);
                    db.SaveChanges();
                    msg = "Khai báo hư hỏng thiết bị thành công!!!";
                }
                else
                {
                    db.CapNhatSoLuongKhaiBaoHuHong(maThietBi, sv.MaPhong, soLuong);
                    msg = "Cập nhật khai báo thành công " + soLuong + " thiết bị '"
                        + khaiBao.ThietBi.TenThietBi + "' bị hư hỏng tại phòng " + khaiBao.Phong.Ten;
                }
                return Ok(new
                {
                    Success = true,
                    msg = msg
                });
            }
            return Ok(new
            {
                Success = false,
                msg = "Đã xảy ra lỗi, vui lòng thử lại!!!"
            });
        }
    }
}
