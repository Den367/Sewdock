using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;
using Myembro.Infrastructure;
using Myembro.Models;
using Myembro.Properties;

namespace Myembro.Controllers
{
    [Description("Handles actions that have to do with contacting the site owner.")]
    public class ContactController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "contact";

        #endregion

        #region Actions

        [Description("Allows the user to send an email to the website owner.")]
        public ActionResult Index()
        {
            return View(ViewName.Index);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult SendMessage(ContactForm form)
        {
            if (!this.ModelState.IsValid)
            {
                return Index();
            }
            var subject = string.Format(CultureInfo.CurrentCulture, "Contact from {0} on your \"{1}\" {2} website.", form.AuthorName, this.SiteData.Settings.Title, this.SiteData.ApplicationName);
            var body = form.Text;
            Mailer.SendNotificationMail(this.SiteData.Settings, subject, body, false, false, form.AuthorEmail, form.AuthorName);
            RememberPreferences(form.AuthorName, form.AuthorEmail, null, form.RememberMe);
            //SetPageFlash(Myembro.Properties.Resources.PageFlashMessageSent);
            return RedirectToAction("Index","ResultInfo", new {@Title = "Contact",@Header = "Contact me",@Message= "Message has been sent."});
        }

        #endregion
    }
}