using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Oasis.Common;
using Oasis.DataAccess;
using Oasis.DataAccess.Contracts;
using Oasis.DataModel;
using Oasis.DataModel.Model;
using Oasis.Infrastructure;
using Oasis.Models;

namespace Oasis.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel, string returnUrl = "")
        {
            if (user != null)
            {
                if (!HashPasswordHelper.VerifyHashedPassword(user.PasswordHash, loginViewModel.Password))
                {
                    return RedirectToAction("Index", "Account");//TO DO: Add error message
                }

                var userCookieData = new UserCookieData
                {
                    UserId = user.Id,
                    Roles = new HashSet<string>(user.UserRoles.Select(ur => ur.Role.Name)),

                    PermissionIds = new HashSet<int>(user.UserRoles.SelectMany(ur => ur.Role.PermissionRoles)
                                                                   .Select(pr => pr.PermissionId)),
                    UserName = user.UserName
                };

                var userData = JsonConvert.SerializeObject(userCookieData);
                var authenticationTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now,
                    DateTime.Now.AddMinutes(20), false, userData);

                var encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }
    }
}