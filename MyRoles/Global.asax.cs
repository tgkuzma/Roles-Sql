using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Business.Interfaces;
using Ninject;

namespace MyRoles
{
    public class MvcApplication : System.Web.HttpApplication
    {
        [Inject]
        public IUserManager UserManager { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            if (Session["User"] == null)
            {
                var appCookie = Request.Cookies["MyRolesCookie"];
                if (!string.IsNullOrEmpty(appCookie?["UserId"]))
                {
                    Session["User"] = UserManager.GetUser();
                    appCookie.Expires = DateTime.Now.AddDays(1);
                }
                else
                {
                    Response.Redirect("/home/login");
                }
            }
        }
    }
}
