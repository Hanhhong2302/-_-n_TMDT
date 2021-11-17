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
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyBanSachEntities1 db = new QuanLyBanSachEntities1();
        public ActionResult Index(int? page)
        {
            //tạo biến số sản phẩm trên trang
            int pageSize = 9;
            //tạo biến số trang
            int pageNumber = (page ?? 9);
            return View(db.Saches.Where(n=>n.Moi==1).OrderBy(n=>n.GiaBan).ToPagedList(pageNumber,pageSize));
        }
        
    }
}