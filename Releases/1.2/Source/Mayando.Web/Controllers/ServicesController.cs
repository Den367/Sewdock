using System;
using System.ComponentModel;
using System.Net;
using System.Web.Mvc;
using JelleDruyts.Web.Mvc;
using Mayando.ServiceModel;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with the Service API.")]
    public class ServicesController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "services";

        #endregion

        #region Actions

        [AuthorizeAdministrator]
        [Description("Shows the Service API page.")]
        public ActionResult Index()
        {
            return View(ViewName.Index, new ServicesViewModel(this.SiteData.Settings.ServiceApiEnabled, this.SiteData.Settings.ApiKey));
        }

        [AuthorizeAdministrator]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EnableServiceApi()
        {
            this.SiteData.Settings.ServiceApiEnabled = true;
            if (string.IsNullOrEmpty(this.SiteData.Settings.ApiKey))
            {
                this.SiteData.Settings.ApiKey = Guid.NewGuid().ToString();
            }
            SaveServiceApiSettings();
            SetPageFlash("The Service API is now enabled.");
            return RedirectToAction(ActionName.Index);
        }

        [AuthorizeAdministrator]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DisableServiceApi()
        {
            this.SiteData.Settings.ServiceApiEnabled = false;
            SaveServiceApiSettings();
            SetPageFlash("The Service API is now disabled.");
            return RedirectToAction(ActionName.Index);
        }

        [AuthorizeAdministrator]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RequestNewApiKey()
        {
            this.SiteData.Settings.ApiKey = Guid.NewGuid().ToString();
            SaveServiceApiSettings();
            SetPageFlash("A new API Key was created; make sure to update your clients with the new key.");
            return RedirectToAction(ActionName.Index);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PhotoProviderSynchronize(ApiAuthentication auth, DateTimeOffset? syncStartTime, string tagList)
        {
            // Verify authentication.
            if (!this.SiteData.Settings.ServiceApiEnabled || auth == null || !auth.IsValid(this.SiteData.Settings.ApiKey))
            {
                Logger.Log(LogLevel.Warning, "An attempt to call a Service API failed authentication. Requested URL: " + this.HttpContext.Request.RawUrl);
                return new HttpStatusResult(HttpStatusCode.Forbidden);
            }

            // Take the specified sync start time if present, otherwise take the last sync time.
            var actualSyncStartTime = (syncStartTime.HasValue ? syncStartTime.Value : (this.SiteData.Settings.PhotoProviderLastSyncTime.HasValue ? this.SiteData.Settings.PhotoProviderLastSyncTime.Value : DateTimeOffsetExtensions.MinValue));

            // Synchronize.
            var status = Tasks.SynchronizePhotoProvider(actualSyncStartTime, tagList, false, RequestOrigin.ServiceApi);

            // Convert and return the result.
            var result = new PhotoProviderSynchronizationResult
            {
                Succeeded = status.LastSyncSucceeded ?? false,
                LastSyncNewComments = status.LastSyncNewComments,
                LastSyncNewPhotos = status.LastSyncNewPhotos,
                LastSyncTimeUtc = (status.LastSyncTime.HasValue ? status.LastSyncTime.Value.UtcDateTime : (DateTime?)null),
                LastSyncStatusHtml = status.LastSyncStatus
            };
            return new ContentResult { Content = SerializationProvider.Serialize(result), ContentType = "text/xml" };
        }

        #endregion

        #region Helper Methods

        private void SaveServiceApiSettings()
        {
            using (var repository = GetRepository(true))
            {
                repository.SaveSettingValues(SettingsScope.Application, this.SiteData.Settings.GetChangedSettings());
                repository.CommitChanges();
            }
        }

        #endregion
    }
}