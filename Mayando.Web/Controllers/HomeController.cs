﻿using System.Web.Mvc;
using Myembro.Filters;

namespace Myembro.Controllers
{
    public class HomeController : SiteControllerBase
    {
        [ClientInfoActionFiterAttribute(enableThrottling = false, notifyByEmail = true, eMail = "denis-mandrykin@yandex.ru")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [ClientInfoActionFiterAttribute(enableThrottling = true, notifyByEmail = true, eMail = "denis-mandrykin@yandex.ru")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
