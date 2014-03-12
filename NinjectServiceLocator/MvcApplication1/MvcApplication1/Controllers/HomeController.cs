using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        private IHelloWorldService _helloWorldService;

        public HomeController()
        {
            _helloWorldService = ServiceLocator.Current.GetInstance<IHelloWorldService>();
        }

        public ActionResult Index()
        {
            ViewBag.Message = _helloWorldService.Hello(@"Welcome to ASP.NET MVC!");
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
