using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenVietTien.SachOnline.Models;
using WebGrease;

namespace NguyenVietTien.SachOnline.Areas.NVT_Admin.Controllers
{
    public class NhaXuatBanController : Controller
    {
        NguyenVietTien_SachOnlineEntities tien_db = new NguyenVietTien_SachOnlineEntities();
        // GET: NVT_Admin/NhaXuatBan
        public ActionResult Index()
        {
            return View(tien_db.NHAXUATBANs);
        }

        public ActionResult ChiTietNXB()
        {
            int maNXB = int.Parse(Request["id"]);
            return View(GetNXB(maNXB));
        }

        [HttpGet]
        public ActionResult ThemNXB()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemNXB(FormCollection fields)
        {
            NHAXUATBAN nxb = new NHAXUATBAN();
            nxb.TenNXB = fields["tenNXB"];
            nxb.DiaChi = fields["dcNXB"];
            nxb.DienThoai = fields["dtNXB"];
            tien_db.NHAXUATBANs.Add(nxb);
            tien_db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult SuaNXB(int id)
        {
            return View(GetNXB(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaNXB()
        {
            if (ModelState.IsValid)
            {
                NHAXUATBAN nxb = GetNXB(int.Parse(Request.Form["MaNXB"]));
                nxb.TenNXB = Request.Form["TenNXB"];
                nxb.DiaChi = Request.Form["DiaChi"];
                nxb.DienThoai = Request.Form["DienThoai"];
                tien_db.NHAXUATBANs.AddOrUpdate(nxb);
                tien_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("SuaNXB");
        }

        [HttpGet]
        public ActionResult XoaNXB(int id)
        {
            Action XoaNXB = () =>
            {
                NHAXUATBAN nxb = GetNXB(id);
                tien_db.NHAXUATBANs.Remove(nxb);
                tien_db.SaveChanges();
            };
            XoaNXB?.Invoke();
            return RedirectToAction("Index");
        }

        public NHAXUATBAN GetNXB(int id)
        {
            return tien_db.NHAXUATBANs.Where(nxb => nxb.MaNXB == id).SingleOrDefault();
        }
    }
}