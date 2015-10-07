using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Web.Mvc;
using Myembro.Infrastructure;
using Myembro.Models;
using Myembro.Properties;
using Newtonsoft.Json;

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
            if (Request.IsAjaxRequest()) return PartialView("Main");
            return View(ViewName.Index);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult SendMessage(ContactForm form)
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6LdY4w0TAAAAAPzpKikpQgjAYDYf1ZvPuGwQgf1L";

            var client = new WebClient();
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count > 0)
                {
                    var error = captchaResponse.ErrorCodes[0].ToLower();
                    string errorMessage = string.Empty;
                    switch (error)
                    {
                        case ("missing-input-secret"):
                            errorMessage = "The secret parameter is missing.";                                                      
                            break;
                        case ("invalid-input-secret"):
                            errorMessage = "The secret parameter is invalid or malformed.";                                   
                            break;
                        case ("missing-input-response"):
                            errorMessage = "The response parameter is missing.";                                                               
                            break;
                        case ("invalid-input-response"):
                            errorMessage = "The response parameter is invalid or malformed.";                            
                            break;
                        default:
                            errorMessage = "Error occured. Please try again";
                            break;
                    }
                     ModelState.AddModelError("g-Captcha", errorMessage);
                    ViewBag.Message = errorMessage;
                }
            }
          
            if (!this.ModelState.IsValid || !captchaResponse.Success)
            {
                return Index();
            }
            var subject = string.Format(CultureInfo.CurrentCulture, "Contact from {0} on your \"{1}\" {2} website.", 
                form.AuthorName, this.SiteData.Settings.Title, this.SiteData.ApplicationName);
            var body = string.Format("Message:\r\n{0}\r\nFrom:\r\n{1}",form.Text,form.AuthorEmail);
            Mailer.SendNotificationMail(this.SiteData.Settings, subject, body, false, false, form.AuthorEmail, form.AuthorName);
            RememberPreferences(form.AuthorName, form.AuthorEmail, null, form.RememberMe);
            //SetPageFlash(Myembro.Properties.Resources.PageFlashMessageSent);
            return RedirectToAction("Index","ResultInfo", new {@Title = "Contact",@Header = "Contact me",@Message= "Message has been sent."});
        }


       
        #endregion
    }
}