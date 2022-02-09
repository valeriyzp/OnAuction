using OnAuction.Filters;
using OnAuction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnAuction.Controllers
{
    public class AccountController : Controller
    {
        [NotAuthorize]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                User user = null;
                using (DataContext db = new DataContext())
                {
                    user = db.Users.FirstOrDefault(x => x.Username == model.Username);
                }
                if (user == null)
                {
                    using (DataContext db = new DataContext())
                    {
                        db.Users.Add(new User {Username = model.Username, Password = model.Password, Email = model.Email, PhoneNumber = model.PhoneNumber, RoleId = model.IsSeller ? 2 : 1});
                        db.SaveChanges();

                        user = db.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
                    }

                    if (user != null)
                    {
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Користувач з таким логіном вже існує");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("List", "Lot");
        }

        [NotAuthorize]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DataContext db = new DataContext())
                {
                    user = db.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, true);
                    return RedirectToAction("List", "Lot");
                }
                else
                {
                    ModelState.AddModelError("", "Не правильний логін чи пароль");
                }
            }
            return View(model);
        }
    }
}
