﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oasis.DataAccess;
using Oasis.DataModel;
using Oasis.DataModel.Enums;
using Oasis.DataModel.Model;
using Oasis.Infrastructure;

namespace Oasis.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize(Permissions.AdminPermission)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}