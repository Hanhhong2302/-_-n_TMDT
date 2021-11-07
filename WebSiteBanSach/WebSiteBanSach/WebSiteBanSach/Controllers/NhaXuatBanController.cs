using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class NhaXuatBanController : Controller
    {
        // GET: NhaXuatBan
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public PartialViewResult NhaXuatBanPartial()
        {
            return PartialView(db.NhaXuatBans.Take(10).OrderBy(n => n.TenNXB).ToList());
        }
        //hiển thị sách theo nhà xuất bản
        public ViewResult SachTheoNXB(int MaNXB=0)
        {
            //ktra chu đề tồn tại hay không
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                //trả về trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            //truy xuất danh sách các quyển sách theo Nhà xuất bản
            List<Sach> lstSach = db.Saches.Where(n => n.MaNXB == MaNXB && n.SoLuongTon > 0).OrderBy(n => n.GiaBan).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "Không có Sách nào thuộc Chủ đề này!";
            }
            return View(lstSach);
        }
        //hiển thị nhà xuất bản
        public ViewResult DanhMucNXB()
        {
            return View(db.NhaXuatBans.ToList());
        }
    }
}