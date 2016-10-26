using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Oasis.Business;
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
        private readonly UserManager _userManager;
        private readonly AuthenticationManager _authenticationManager;

        public AccountController()
        {
            _userManager = new UserManager();
            _authenticationManager = new AuthenticationManager();
        }


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
            if (!_userManager.VerifyCredentials(loginViewModel.UserName, loginViewModel.Password))
            {
                return RedirectToAction("Index", "Account");
            }

            _authenticationManager.Authenticate(loginViewModel.UserName);
            return RedirectToAction("Index", "Home");
        }
    }
}