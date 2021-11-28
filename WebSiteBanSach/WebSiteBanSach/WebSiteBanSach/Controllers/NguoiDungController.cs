using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLyBanSachEntities1 db = new QuanLyBanSachEntities1();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet] 
        public ActionResult DangKy()
        {
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                //Insert Data into KhachHang Table
                db.KhachHang.Add(kh);
                //Save to Database
                db.SaveChanges();

            }
                return View();
        }
        
        [HttpPost]

        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f.Get("txtTaiKhoan").ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            KhachHang kh = db.KhachHang.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Đăng Nhập Thành Công";
                Session["TaiKhoan"] = kh;
                return View();
            }
            ViewBag.ThongBao = "Đăng Nhập Thất Bại";
            return View();
           

        }
    }
}