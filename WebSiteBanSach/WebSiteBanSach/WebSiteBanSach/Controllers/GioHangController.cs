using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
namespace WebSiteBanSach.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanSachEntities1 db = new QuanLyBanSachEntities1();
        #region GioHang
        // Lay gio hang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì tiến hành khởi tạo list giỏ hàng (SessionGioHang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang (int iMaSach, string strURL)
        {
            Sach sach = db.Sach.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //Lấy ra session giỏ hàng
           List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sách đã tồn tại trong session (giỏ hàng) chưa
            GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            if(sanpham == null)
            {
                sanpham = new GioHang(iMaSach);

                //Add sản phẩm mới thêm vào list
                lstGioHang.Add(sanpham);
                return Redirect(strURL);

            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //Cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //Kiểm tra Masp
            Sach sach = db.Sach.SingleOrDefault(n => n.MaSach == iMaSP);
            // Nếu get sai Masp thì sẽ trả về trang lỗi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //kiểm tra sp có tồn tại trong Giohang
            List<GioHang> lstGioHang = LayGioHang();
           
            GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSP);
            //nếu tồn tại thì cho sửa lại số lượng
            if(sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction ("GioHang");

        }
        //Xoa gio hang
        public ActionResult XoaGioHang (int iMaSP)
        {
            Sach sach = db.Sach.SingleOrDefault(n => n.MaSach == iMaSP);
            // Nếu get sai Masp thì sẽ trả về trang lỗi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSP);
            //nếu tồn tại thì cho sửa lại số lượng
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == sanpham.iMaSach);
               
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
      
        //Xây dựng trang giỏ hàng
        public ActionResult GioHang()
         {
            if (Session["GioHang"]== null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            
            return View(lstGioHang);
        }

        //Tính tổng số lượng và tổng tiền
        //Tíng tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        //Tao partical gio hang
        public ActionResult GioHangPartical()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xay dung 1 view cho nguoi dung sua gio hàng
        public ActionResult SuaGioHang()
        {

            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();

            return View(lstGioHang);
        }
        #endregion

        #region Dat Hang
        //xay dung chuwc nang dat hang
        [HttpPost]
        public ActionResult DatHang()
        {
            //kiem tra dang nhap
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            { 
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            //them dơn dat hang
            if(Session["GioHang"]== null)
            {
                RedirectToAction("Index", "Home");
            }
            //Them don hang
            DonHang ddh = new DonHang();
            KhachHang kh = (KhachHang) Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            db.DonHang.Add(ddh);
            db.SaveChanges();

               //them chi tiet don hang
               foreach(var item in gh)
               {
                   ChiTietDonHang ctDH = new ChiTietDonHang();
                   ctDH.MaDonHang = ddh.MaDonHang;
                   ctDH.MaSach = item.iMaSach;
                   ctDH.SoLuong = item.iSoLuong;
                   ctDH.DonGia = item.dDonGia.ToString();
                db.ChiTietDonHang.Add(ctDH);
               }
               db.SaveChanges();
               return RedirectToAction("Index", "Home");
           }
            #endregion
        }

    }

