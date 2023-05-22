using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLKTX.Models;

namespace QLKTX.Controllers
{
    public class XetDuyetApiController : ApiController
    {
        static KyTucXaEntities db = new KyTucXaEntities();
        [HttpGet]
        public IHttpActionResult PhanHoiXetDuyet(string maSV, string trangThai, string ghiChu = null)
        {
            DangKyNoiTru dk = db.DangKyNoiTrus.FirstOrDefault(d => d.MaSV == maSV);
            if (dk == null)
                return Ok(new
                {
                    Success = false,
                    msg = "Yêu cầu xét duyệt này không còn tồn tại!!!",
                    type = "error"
                });
            dk.TrangThai = trangThai;
            dk.GhiChu = ghiChu;
            db.DangKyNoiTrus.Attach(dk);
            db.Entry(dk).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            if (trangThai == "Đã xét duyệt")
            {
                try
                {
                    db.TaoHopDong(maSV, "admin1");
                }
                catch
                {
                    return Ok(new
                    {
                        Success = false,
                        msg = "Xét duyệt đăng ký thành công, nhưng xảy ra lỗi trong quá trình tạo hợp đồng, vui lòng tạo hợp đồng thủ công!!!",
                        type = "info"
                    });
                }
            }
            else if(trangThai == "Bị hủy")
                return Ok(new
                {
                    Success = true,
                    msg = "Bạn đã huỷ yêu cầu đăng ký nội trú, lý do: "+ghiChu,
                    type = "success"
                });
            return Ok(new
            {
                Success = true,
                msg = "Xét duyệt đăng ký thành công, hợp đồng đã được tạo tự động!!!",
                type = "success"
            });
        }
    }

}
