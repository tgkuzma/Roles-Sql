using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Business.Interfaces;
using Models;

namespace MyRoles.Controllers
{
    [AuthorizePermission(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IUserManager _userManager;

        public HomeController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AuthorizePermission(Roles = "Something")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Message = "Your application description page.";

            return View(new Models.User());
        }

        [HttpPost]
        [AllowAnonymous]
        public void Login(User model)
        {
            var user = _userManager.LoginUser(model.UserName, model.Password);
            if (user != null)
            {
                PersistUserInfo(user);
                Response.Redirect("/home");
            }

            //If we got back a null user (or an error) then user was not vailidated
            //Return back to login page
        }

        private void PersistUserInfo(User user)
        {
            var cookie = new HttpCookie("MyRolesCookie");
            cookie.Values["UserId"] = user.Id.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);

            Session["User"] = user;
        }
    }
}