using System.IO;
using System.Net.Mail;
using NguyenVietTien.SachOnline.Models;
using System.Net;
using System.Net.Mime;
using System.Web.Mvc;

namespace NguyenVietTien.SachOnline.Controllers
{
    public class FileAndMailController : Controller
    {
        // GET: FileAndMail
        [HttpGet]
        public ActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMail(Mail model)
        {
            // Cấu hình thông tin Gmail (khai báo thư viện System.Net)
            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("2324802010369@student.tdmu.edu.vn", "zfmdfplbcezlmlmq"), // Email và mật khẩu ứng dụng
                EnableSsl = true
            };

            var message = new MailMessage();
            message.From = new MailAddress(model.From);
            message.ReplyToList.Add(model.From);
            message.To.Add(new MailAddress(model.To));
            message.Subject = model.Subject;
            message.Body = model.Notes;

            // Xử lý file đính kèm
            var file = Request.Files["attachment"];
            if (file != null && file.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/UploadFile"), file.FileName);
                if (!System.IO.File.Exists(path))
                {
                    file.SaveAs(path);
                }

                Attachment data = new Attachment(path, MediaTypeNames.Application.Octet);
                message.Attachments.Add(data);
            }

            mail.Send(message);
            return View("SendMail");
        }

    }
}