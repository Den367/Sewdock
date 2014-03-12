using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;
using JelleDruyts.Web.Mvc.Discovery;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with administrative tasks.")]
    [AuthorizeAdministrator]
    public class AdminController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "admin";

        #endregion

        #region Actions

        [Description("Shows an overview of all administrative actions that can be performed.")]
        public ActionResult Index()
        {
            using (var repository = GetRepository())
            {
                var settings = repository.GetSettings(SettingsScope.Application);
                var settingsViewModel = SettingsController.GetSettingsViewModel(settings, this.ControllerContext.HttpContext);
                return View(ViewName.Index, new AdminViewModel(settingsViewModel));
            }
        }

        [Description("Shows information about the application.")]
        public ActionResult About()
        {
            using (var repository = GetRepository())
            {
                SiteStatistics statistics = repository.GetStatistics();
                return View(ViewName.About, new AboutViewModel(this.SiteData, statistics));
            }
        }

        [Description("Shows all available URL's in the application.")]
        public ActionResult Urls()
        {
            return View(ViewName.Urls, WebsiteInspector.GetWebsiteInfo());
        }

        [Description("Shows the event log.")]
        public ActionResult EventLog([Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page, [Description("The minimum log level to show.")]LogLevel? minLevel, [Description("The text to find.")]string logSearchText)
        {
            using (var repository = GetRepository())
            {
                this.SiteData.Settings.EventLogLastViewedTime = DateTimeOffset.UtcNow;
                repository.SaveSettingValues(SettingsScope.Application, this.SiteData.Settings.GetChangedSettings());
                //repository.CommitChanges();
            }
            this.StoreLogLevelListInViewData(minLevel);
            var actualCount = GetActualCount(count);
            var actualPage = GetActualPage(page);
            if ((minLevel.HasValue && minLevel.Value != LogLevel.Information) || !string.IsNullOrEmpty(logSearchText))
            {
                // When searching, display ALL entries to avoid paging issues.
                actualCount = InfinitePageSize;
                actualPage = 0;
            }
            return View(ViewName.EventLog, Logger.GetLogs(actualCount, actualPage, minLevel, logSearchText));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ClearEventLog(int minAge, LogLevel? maxLevel)
        {
            var actualMaxLevel = maxLevel ?? LogLevel.Information;
            var count = Logger.Clear(minAge, actualMaxLevel);
            SetPageFlash(string.Format(CultureInfo.CurrentCulture, "{0} event log entries older than {1} days and maximum log level {2} were deleted.", count, minAge, actualMaxLevel));
            return RedirectToAction(ActionName.EventLog);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmailTest()
        {
            var subject = string.Format(CultureInfo.CurrentCulture, "Test from your \"{0}\" {1} website.", this.SiteData.Settings.Title, this.SiteData.ApplicationName);
            var body = string.Format(CultureInfo.CurrentCulture, "This is a test email from your {1} website at {2}. Since you've received this message, this means that your notification emails are being sent correctly.{0}{0}Thanks for using {1} and enjoy your photo blog!{0}{0}--{0}The {1} Team", Environment.NewLine, this.SiteData.ApplicationName, this.SiteData.AbsoluteApplicationPath);
            try
            {
                var emailSent = Mailer.SendNotificationMail(this.SiteData.Settings, subject, body, false, false);
                if (emailSent)
                {
                    SetPageFlash("A test email was sent to your email address.");
                }
                else
                {
                    SetPageFlash("A test email was NOT sent. Make sure your owner email address is configured.");
                }
            }
            catch (Exception exc)
            {
                var error = string.Format(CultureInfo.CurrentCulture, "An error occurred while sending a test email to your email address: {0}", exc.Message);
                SetPageFlash(error);
            }
            return RedirectToAction(ActionName.Index);
        }

        #endregion
    }
}