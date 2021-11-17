using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class ChuDeController : Controller
    {
        // GET: ChuDe
        QuanLyBanSachEntities1 db = new QuanLyBanSachEntities1();
        public ActionResult ChuDePartial()
        {
            return PartialView(db.ChuDes.Take(3).ToList());
        }
        
        public ViewResult SachTheoChuDe(int MaChuDe = 0)
        {
            
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if (cd == null)
            {
                //trả về trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            //truy xuất danh sách các quyển sách theo chủ đề
            List<Sach> lstSach = db.Saches.Where(n => n.MaChuDe == MaChuDe && n.SoLuongTon > 0).OrderBy(n => n.GiaBan).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "Không có Sách nào thuộc Chủ đề này!";
            }
            return View(lstSach);
        }
        //hiển thị các chủ đề
        public ViewResult DanhMucChuDe()
        {
            return View(db.ChuDes.ToList());
        }
    }
}