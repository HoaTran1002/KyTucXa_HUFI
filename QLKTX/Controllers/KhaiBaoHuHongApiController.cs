using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLKTX.Models;

namespace QLKTX.Controllers
{
    public class KhaiBaoHuHongApiController : ApiController
    {
        KyTucXaEntities db = new KyTucXaEntities();
        public IHttpActionResult getKhaiBaoHuHongs(bool tatCa, bool daXuLy, bool chuaXuLy, bool thangHienTai, int nam, int thang, string khu = null, string tang = null, string phong = null)
        {
            try
            {
                List<KhaiBaoHuHong> baoHuHongs = new List<KhaiBaoHuHong>();
                if (tatCa)
                {
                    foreach (var item in db.KhaiBaoHuHongs)
                    {
                        baoHuHongs.Add(new KhaiBaoHuHong()
                        {
                            MaKhaiBao = item.MaKhaiBao,
                            MaPhong = item.MaPhong,
                            DaXuLy = item.DaXuLy,
                            MaThietBi = item.MaThietBi,
                            NgayYeuCau = item.NgayYeuCau,
                            TongSoLuong = item.TongSoLuong,
                            TenPhong = item.TenPhong,
                            TenThietBi = item.TenThietBi,
                        });
                    };
                    return Ok(new
                    {
                        Success = true,
                        data = baoHuHongs
                    });
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
                List<KhaiBaoHuHong> lHuHong = db.KhaiBaoHuHongs.Where(h => h.Phong.Tang.Khu.Ma.Contains(khu) && h.Phong.Tang.Ma.Contains(tang) && h.Phong.Ma.Contains(phong)).ToList();
                if (!thangHienTai)
                {
                    lHuHong = lHuHong.Where(hd => hd.NgayYeuCau.Year == nam).ToList();
                    if (thang != 0)
                        lHuHong = lHuHong.Where(hd => hd.NgayYeuCau.Month == thang).ToList();
                }
                else
                {
                    int namHT = DateTime.Now.Year;
                    int thangHT = DateTime.Now.Month;
                    lHuHong = lHuHong.Where(hd => hd.NgayYeuCau.Year == namHT && hd.NgayYeuCau.Month == thangHT).ToList();
                }
                if ((daXuLy && !chuaXuLy) || (!daXuLy && chuaXuLy))
                    lHuHong = lHuHong.Where(hd => hd.DaXuLy == daXuLy).ToList();
                lHuHong.ForEach(item =>
                {
                    baoHuHongs.Add(new KhaiBaoHuHong()
                    {
                        MaKhaiBao = item.MaKhaiBao,
                        MaPhong = item.MaPhong,
                        DaXuLy = item.DaXuLy,
                        MaThietBi = item.MaThietBi,
                        NgayYeuCau = item.NgayYeuCau,
                        TongSoLuong = item.TongSoLuong,
                        TenPhong = item.TenPhong,
                        TenThietBi = item.TenThietBi,
                    });
                });
                return Ok(new
                {
                    Success = true,
                    data = baoHuHongs
                });
            }
            catch
            {
                return Ok(new
                {
                    Success = false,
                    msg = "Đã xảy ra lỗi, vui lòng kiểm tra lại!!!"

                });
            }

        }

        [HttpGet]
        public IHttpActionResult TaoXuLyKhaiBaoHuHong(string maKhaiBao, int soLuong, string nguyenNhan, bool thayMoi, int? chiPhiPhatSinh = null)
        {
            try
            {
                db.XuLyKhaiBaoHuHongs.Add(new XuLyKhaiBaoHuHong()
                {
                    MaKhaiBao = maKhaiBao,
                    SoLuong = soLuong,
                    NguyenNhan = nguyenNhan,
                    ThayMoi = thayMoi,
                    ChiPhiPhatSinh = chiPhiPhatSinh
                });
                db.SaveChanges();
                return Ok(new
                {
                    Success = true,
                    msg = "Xử lý khai báo hư hỏng thành công!!!"
                });
            }
            catch
            {
                return Ok(new
                {
                    Success = false,
                    msg = "Đã xảy ra lỗi, vui lòng kiểm tra lại!!!"
                });
            }
        }

        [HttpGet]
        public IHttpActionResult XacNhanDaXyLyKhaiBaoHuHong(string maKhaiBao)
        {
            try
            {
                KhaiBaoHuHong kbhh = db.KhaiBaoHuHongs.FirstOrDefault(k => k.MaKhaiBao.Equals(maKhaiBao));
                if(kbhh!=null)
                {
                    kbhh.DaXuLy = true;
                    kbhh.NguoiXuLy = "admin1";
                    db.KhaiBaoHuHongs.Attach(kbhh);
                    db.Entry(kbhh).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(new
                    {
                        Success = true,
                        msg = "Xử lý khai báo hư hỏng thành công!!!",
                        type = "success"
                    });
                }
                return Ok(new
                {
                    Success = false,
                    msg = "Khai báo hư hỏng này không tồn tại!!!",
                    type = "error"
                });
            }
            catch{
                return Ok(new
                {
                    Success = false,
                    msg = "Đã xảy ra lỗi, vui lòng kiểm tra lại!!!",
                    type = "error"
                });
            }            
        }
    }
}
