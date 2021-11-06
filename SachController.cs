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
        QuanLyBanSachEntities1 db = new QuanLyBanSachEntities1();
        public PartialViewResult SachMoiPartial()
        {
            var lstSachMoi = db.Sach.Take(3).ToList();
            return PartialView(lstSachMoi);
        }
        //xem chi tiết
        public ViewResult XemChiTiet(int MaSach=0)
        {
            Sach sach = db.Sach.SingleOrDefault(n => n.MaSach == MaSach);
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