using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Interfaces;
using Models;
using Ninject;

namespace MyRoles
{
    public class AuthorizePermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Roles must be separated by commas ","
        /// </summary>
        public new string Roles { get; set; }

        [Inject]
        public IUserManager UserManager { get; set; }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            User user;
            if (httpContext.Session?["User"] == null)
            {
                user = GetUserAndAddToSession(httpContext);
            }
            else
            {
                user = (User) httpContext.Session["User"];
            }


            var allRoles = Roles.Split(Convert.ToChar(","));
            return allRoles.Any(role => user.Roles.Any(r => r.Name == role));
        }

        private User GetUserAndAddToSession(HttpContextBase httpContext)
        {
            var applicationUser = UserManager.GetUserByName(httpContext.User.Identity.Name);
            httpContext.Session["User"] = applicationUser;

            return applicationUser;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new ViewResult { ViewName = "Unauthorized" };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}