using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using NguyenVietTien.SachOnline.Models;

namespace NguyenVietTien.SachOnline.Controllers
{
    public class NguyenVietTien_SachOnlineController : Controller
    {
        NguyenVietTien_SachOnlineEntities db_Tien = new NguyenVietTien_SachOnlineEntities();

        public List<SACH> LaySachMoi(int count)
        {
            return db_Tien.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        public List<SACH> LaySachBanNhieu(int count)
        {
            return db_Tien.SACHes.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();
        }

        // GET: SachOnline
        public ActionResult Index(int page = 1, int pageSize = 6)
        {
            var allBooks = db_Tien.SACHes.OrderByDescending(s => s.NgayCapNhat).ToList();

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
            return PartialView(db_Tien.CHUDEs);
        }

        [ChildActionOnly]
        public ActionResult NhaXuatBanPartial()
        {
            return PartialView(db_Tien.NHAXUATBANs);
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

        public ActionResult ChiTietSach(int id)
        {
            return View(db_Tien.SACHes.SingleOrDefault(sach => sach.MaSach == id));
        }

        public ActionResult SachTheoChuDe(int id)
        {
            return View(db_Tien.SACHes.Where(sach => sach.MaCD == id).ToList());
        }

        public ActionResult SachTheoNhaXuatBan(int id)
        {
            return View(db_Tien.SACHes.Where(sach => sach.MaNXB == id).ToList());
        }
    }
}