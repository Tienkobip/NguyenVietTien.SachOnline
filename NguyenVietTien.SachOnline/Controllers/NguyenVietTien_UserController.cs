using NguyenVietTien.SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace NguyenVietTien.SachOnline.Controllers
{
    public class NguyenVietTien_UserController : Controller
    {
        NguyenVietTien_SachOnlineEntities db_Tien = new NguyenVietTien_SachOnlineEntities();
        // GET: NguyenVietTien_User
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];

            if (String.IsNullOrEmpty(sTenDN) || String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập hoặc chưa nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = db_Tien.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTenDN && n.MatKhau == sMatKhau);

                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    if (collection["remember"].Contains("true"))
                    {
                        Response.Cookies["TenDN"].Value = sTenDN;
                        Response.Cookies["MatKhau"].Value = sMatKhau;
                        Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(1);
                        Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(1);
                    }
                    else
                    {
                        Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(-1);
                    }
                    return RedirectToAction("Index", "NguyenVietTien_SachOnline");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "NguyenVietTien_SachOnline");
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DangKy(KHACHHANG khachHang)
        {
            // 1. Kiểm tra ModelState: Đảm bảo dữ liệu hợp lệ dựa trên Data Annotations
            if (ModelState.IsValid)
            {
                // 2. Kiểm tra xem tên tài khoản đã tồn tại chưa
                var checkTaiKhoan = db_Tien.KHACHHANGs.FirstOrDefault(k => k.TaiKhoan == khachHang.TaiKhoan);
                if (checkTaiKhoan != null)
                {
                    // Nếu đã tồn tại, thêm lỗi và trả về View
                    ModelState.AddModelError("TaiKhoan", "Tên tài khoản này đã được sử dụng.");
                    return View(khachHang);
                }

                // 3. Kiểm tra xem email đã tồn tại chưa
                var checkEmail = db_Tien.KHACHHANGs.FirstOrDefault(k => k.Email == khachHang.Email);
                if (checkEmail != null)
                {
                    ModelState.AddModelError("Email", "Địa chỉ email này đã được sử dụng.");
                    return View(khachHang);
                }

                // Nếu sau các bước kiểm tra thêm mà ModelState vẫn hợp lệ
                if (ModelState.IsValid)
                {
                    // 5. Thêm khách hàng mới vào DbContext
                    db_Tien.KHACHHANGs.Add(khachHang);

                    // 6. Lưu thay đổi vào database
                    db_Tien.SaveChanges();

                    // 7. Chuyển hướng đến trang Đăng nhập với thông báo thành công
                    // Bạn có thể dùng TempData để truyền thông báo qua các trang
                    TempData["SuccessMessage"] = "Đăng ký tài khoản thành công! Vui lòng đăng nhập.";
                    return RedirectToAction("DangNhap", "NguyenVietTien_User");
                }
            }

            // 8. Nếu ModelState không hợp lệ từ đầu hoặc sau khi kiểm tra thêm,
            // trả về View với dữ liệu người dùng đã nhập để họ sửa lỗi
            return View(khachHang);
        }
    }
}