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
        private readonly AcccountManager _accountManager;

        public AccountController()
        {
            _userManager = new UserManager();
            _accountManager = new AcccountManager();
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

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }

        [AllowAnonymous]
        public ActionResult UserPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePassword(ChangeUserPasswordViewModel changeUserPasswordViewModel)
        {
            if (!ModelState.IsValid) 
            {
                return View("UserPassword", changeUserPasswordViewModel);
            }

            var userId = AppSecurityContext.UserId;
            if (!_accountManager.CheckOldPassword(changeUserPasswordViewModel.OldPassword, userId.Value))
            {
                throw new Exception("Wrong old password");
            }

            if (!_accountManager.IsPasswordNew(changeUserPasswordViewModel.NewPassword, userId.Value))
            {
                throw new Exception("Please create another password. Such password existed");
            }

            if (!string.Equals(changeUserPasswordViewModel.NewPassword, changeUserPasswordViewModel.ConfirmNewPassword))
            {
                throw new Exception("Passwords are different");
            }

            _accountManager.SaveUserPassword(changeUserPasswordViewModel.NewPassword, userId.Value);

            return RedirectToAction("Index", "Home");
        }
    }
}