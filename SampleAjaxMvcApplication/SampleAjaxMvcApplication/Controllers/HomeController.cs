using System.Threading;
using System.Web.Mvc;
using SampleAjaxMvcApplication.Models;

namespace SampleAjaxMvcApplication.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Welcome to ASP.NET MVC!";

			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Feedback()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Feedback(FeedbackModel model)
		{
			if (ModelState.IsValid)
			{
				//-------------------------------
				// реальная отправка сообщения
				// не показана в примере
				//-------------------------------

				if (Request.IsAjaxRequest())
				{
					return PartialView("FeedbackSent");
				}

				return View("FeedbackSent");
			}
			return View();
		}
	}
}
