using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public PartialViewResult SachMoiPartial()
        {
            var lstSachMoi = db.Saches.Take(3).ToList();
            return PartialView(lstSachMoi);
        }
        //xem chi tiết
        public ViewResult XemChiTiet(int MaSach=0)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if(sach == null)
            {
                //trả về trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
    }
}