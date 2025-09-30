using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenVietTien.SachOnline.Models;

namespace NguyenVietTien.SachOnline.Controllers
{
    public class NguyenVietTien_SachOnlineController : Controller
    {
        NguyenVietTien_SachOnlineEntities db_Tien_Long = new NguyenVietTien_SachOnlineEntities();

        public List<SACH> LaySachMoi(int count)
        {
            return db_Tien_Long.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        public List<SACH> LaySachBanNhieu(int count)
        {
            return db_Tien_Long.SACHes.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();
        }

        // GET: SachOnline
        public ActionResult Index(int page = 1, int pageSize = 6)
        {
            var allBooks = db_Tien_Long.SACHes.OrderByDescending(s => s.NgayCapNhat).ToList();

            int totalItems = allBooks.Count();
            var pagedBooks = allBooks.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(pagedBooks);
        }

        [ChildActionOnly]
        public ActionResult NavPartial()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult SliderPartial()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult ChuDePartial()
        {
            return PartialView(db_Tien_Long.CHUDEs);
        }

        [ChildActionOnly]
        public ActionResult NhaXuatBanPartial()
        {
            return PartialView(db_Tien_Long.NHAXUATBANs);
        }

        [ChildActionOnly]
        public ActionResult SachBanNhieuPartial()
        {
            var listSachBanNhieu = LaySachBanNhieu(6);
            return PartialView(listSachBanNhieu);
        }

        [ChildActionOnly]
        public ActionResult FooterPartial()
        {
            return PartialView();
        }
    }
}