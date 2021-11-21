using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace WebSiteBanSach.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        QuanLyBanSachEntities1 db = new QuanLyBanSachEntities1();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Saches.ToList().OrderBy(n=>n.MaSach).ToPagedList(pageNumber,pageSize));
        }
        // thêm mới dữ liệu
        [HttpGet]
        public ActionResult ThemMoi()
        {
            //đưa dlieu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(Sach sach, HttpPostedFileBase FileUpload)
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            //ktra đường dẫn ảnh bìa
            if (FileUpload == null)
            {
                ViewBag.ThongBao = "Chưa chọn Ảnh bìa";
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View(sach);
            }
            var FileName = Path.GetFileName(FileUpload.FileName);
            var DuongDan = Path.Combine(Server.MapPath("~/HinhAnhSP"), FileName);
            // kiểm tra hình ảnh đã tồn tại chưa
            if (!System.IO.File.Exists(DuongDan))
            {
                ViewBag.ThongBao = "hình ảnh đã tồn tại";

            }
            else
            {
                FileUpload.SaveAs(DuongDan);
            }
            sach.AnhBia = FileUpload.FileName;
            sach.NgayCapNhat = DateTime.Now;
            db.Saches.Add(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult ChinhSua(int MaSach)
        {
            //lấy ra đối tượng sách theo mã
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //đưa dlieu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe),"MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(Sach sach, HttpPostedFileBase FileUpload)
        {
            // đưa dlieu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            //ktra đường dẫn ảnh bìa
            if (FileUpload == null)
            {
                ViewBag.ThongBao = "Chưa chọn Ảnh bìa";
                return View(sach);
            }
            // cập nhập csdl
            if (!ModelState.IsValid)
            {
                return View(sach);
            }
            var FileName = Path.GetFileName(FileUpload.FileName);
            // lưu đường dẫn
            var DuongDan = Path.Combine(Server.MapPath("~/HinhAnhSP"), FileName);
            // kiểm tra hình ảnh đã tồn tại chưa
            if (!System.IO.File.Exists(DuongDan))
            {
                ViewBag.ThongBao = "hình ảnh đã tồn tại";
               
            }
            else
            {
                FileUpload.SaveAs(DuongDan);
            }
            sach.AnhBia = FileUpload.FileName;
            db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // hiển thị
        public ActionResult HienThi(int MaSach)
        {
            //lấy ra đối tượng sách theo mã
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        // xóa 
        public ActionResult Xoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost,ActionName("Xoa")]

        public ActionResult XacNhanXoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}