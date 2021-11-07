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
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                //Insert Data into KhachHang Table
                db.KhachHangs.Add(kh);
                //Save to Database
                db.SaveChanges();

            }
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection fc)
        {
            string textTaiKhoan = fc["txtTaiKhoan"].ToString();
            string textMatKhau = fc["txtMatKhau"].ToString();
            KhachHang _KhachHang = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == textTaiKhoan && n.MatKhau == textMatKhau);
            if (_KhachHang != null)
            {
                Session["TaiKhoan"] = _KhachHang;
                ViewBag.ThongBao = "Đăng nhập thành công";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ThongBao = "Sai Tài khoản hoặc Mật khẩu";
            return View();
        }

    }
}
