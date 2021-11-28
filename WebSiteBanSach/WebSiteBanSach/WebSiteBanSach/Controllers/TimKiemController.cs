using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList;
using PagedList.Mvc;

namespace WebSiteBanSach.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        QuanLyBanSachEntities1 db = new QuanLyBanSachEntities1();
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection fc, int? _Page)
        {
            string TuKhoa = fc["txtTimKiem"].ToString().Trim();
            ViewBag.TuKhoa = TuKhoa;
            List<Sach> lstSach = db.Saches.Where(n => n.TenSach.Contains(TuKhoa) && n.SoLuongTon > 0).ToList();
            int pageNumber = (_Page ?? 1);
            int pageSize = 9;
            if (lstSach.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy Sách bạn yêu cầu !";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstSach.Count.ToString() + " quyển Sách :";
            return View(lstSach.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult KetQuaTimKiem(string _TuKhoa, int? _Page)
        {
            ViewBag.TuKhoa = _TuKhoa;
            List<Sach> lstSach = db.Saches.Where(n => n.TenSach.Contains(_TuKhoa) && n.SoLuongTon > 0).ToList();
            int pageNumber = (_Page ?? 1);
            int pageSize = 9;
            if (lstSach.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy Sách bạn yêu cầu !";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstSach.Count.ToString() + " quyển Sách :";
            return View(lstSach.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }
    }
}