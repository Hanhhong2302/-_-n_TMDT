using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList.Mvc;
using PagedList;

namespace WebSiteBanSach.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: TimKiem

        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            string key = f["txtTimkiem"].ToString();
            List<Sach> lstKQTK = db.Saches.Where(n => n.TenSach.Contains(key)).ToList();

            int pagenum = (page ?? 1);
            int pagesize = 9;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Khong Tim Thay San Pham";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pagenum,pagesize));
            }
            //Phan Trang

            ViewBag.ThongBao = "Số Kết Quả Tìm Kiểm: " + lstKQTK.Count();
            return View(lstKQTK.OrderBy(n=>n.TenSach).ToPagedList(pagenum,pagesize));
        }
    }
}