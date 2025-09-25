using System.Web.Mvc;

namespace NguyenVietTien.SachOnline.Areas.NVT_Admin
{
    public class NVT_AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NVT_Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NVT_Admin_default",
                "NVT_Admin/{controller}/{action}",
                new { action = "Index" }
            );
        }
    }
}