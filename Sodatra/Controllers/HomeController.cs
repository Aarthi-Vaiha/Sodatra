﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sodatra.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Commiting in Aarthi branch
            //Commiting in Aarthi branch
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