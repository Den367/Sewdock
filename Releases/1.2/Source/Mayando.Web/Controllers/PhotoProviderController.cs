using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Web.Mvc;
using Mayando.ProviderModel.PhotoProviders;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Providers;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with the photo provider.")]
    public class PhotoProviderController : ProviderControllerBase
    {
        #region Constants

        public const string ControllerName = "photoprovider";

        #endregion

        #region Actions

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [Description("Shows the Photo Provider administration page.")]
        public ActionResult Index()
        {
            using (var repository = GetRepository())
            {
                // Get provider info.
                var availableProviders = GetProviderList(PhotoProviderFactory.GetAvailableProviders(), this.SiteData.Settings.PhotoProviderGuid);
                var providerInfo = PhotoProviderFactory.GetProviderInfo(this.SiteData.Settings.PhotoProviderGuid);

                // Get provider status.
                PhotoProviderStatus providerStatus = null;
                var provider = PhotoProviderFactory.CreateProvider(this.SiteData.Settings.PhotoProviderGuid);
                if (provider != null)
                {
                    try
                    {
                        providerStatus = provider.GetStatus();
                    }
                    catch (Exception exc)
                    {
                        Logger.LogException(exc);
                        providerStatus = new PhotoProviderStatus(exc.GetStatusDescriptionHtml(), true, true);
                    }
                }
                if (providerStatus == null)
                {
                    providerStatus = new PhotoProviderStatus(null, true, true);
                }

                // Get provider settings.
                var settings = repository.GetSettings(SettingsScope.PhotoProvider);
                var settingsViewModel = SettingsController.GetSettingsViewModel(settings, this.ControllerContext.HttpContext);

                // Create full view model with all details.
                var syncStatus = new SynchronizationStatus(SiteData.Settings.PhotoProviderLastSyncSucceeded, SiteData.Settings.PhotoProviderLastSyncTime, SiteData.Settings.PhotoProviderLastSyncStatus, SiteData.Settings.PhotoProviderLastSyncNewPhotos, SiteData.Settings.PhotoProviderLastSyncNewComments, SiteData.Settings.PhotoProviderLastSyncWasSimulation);
                var proposedStartTime = (syncStatus.LastSyncTime.HasValue ? syncStatus.LastSyncTime.Value : DateTimeOffsetExtensions.MinValue);
                var model = new PhotoProviderViewModel(availableProviders, providerInfo, providerStatus, syncStatus, settingsViewModel, proposedStartTime, Tasks.PhotoProviderIsSynchronizing, this.SiteData.Settings.PhotoProviderAutoSyncEnabled, this.SiteData.Settings.PhotoProviderAutoSyncIntervalMinutes, this.SiteData.Settings.PhotoProviderAutoSyncTags);
                return View(ViewName.Index, model);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveAutoSync(bool autoSyncEnabled, int? autoSyncIntervalMinutes, string tagList)
        {
            using (var repository = GetRepository(true))
            {
                if (!autoSyncIntervalMinutes.HasValue || autoSyncIntervalMinutes.Value <= 0)
                {
                    autoSyncEnabled = false;
                    autoSyncIntervalMinutes = null;
                }
                this.SiteData.Settings.PhotoProviderAutoSyncEnabled = autoSyncEnabled;
                this.SiteData.Settings.PhotoProviderAutoSyncIntervalMinutes = autoSyncIntervalMinutes;
                this.SiteData.Settings.PhotoProviderAutoSyncTags = tagList;
                repository.SaveSettingValues(SettingsScope.Application, this.SiteData.Settings.GetChangedSettings());
                repository.CommitChanges();
                SetPageFlash("The automatic synchronization settings were saved.");
            }
            return RedirectToAction(ActionName.Index);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SelectProvider(string selectedProvider)
        {
            if (!string.Equals(selectedProvider, this.SiteData.Settings.PhotoProviderGuid, StringComparison.OrdinalIgnoreCase))
            {
                using (var repository = GetRepository(true))
                {
                    this.SiteData.Settings.PhotoProviderGuid = selectedProvider;
                    repository.SaveSettingValues(SettingsScope.Application, this.SiteData.Settings.GetChangedSettings());
                    ResetProvider(repository, this.SiteData);
                    repository.CommitChanges();
                    SetPageFlash("The photo provider was changed.");
                }
            }
            return RedirectToAction(ActionName.Index);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Synchronize(DateTimeOffset syncStartTime, string tagList, bool simulate)
        {
            // Start synchronization on a background thread.
            ThreadPool.QueueUserWorkItem(state =>
            {
                Tasks.SynchronizePhotoProvider(syncStartTime, tagList, simulate, RequestOrigin.User);
            });

            SetPageFlash("A new synchronization with the photo provider was requested and is running in the background.");
            return RedirectToAction(ActionName.Index);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Reset()
        {
            using (var repository = GetRepository(true))
            {
                ResetProvider(repository, this.SiteData);
                repository.CommitChanges();
                SetPageFlash("The photo provider was reset.");
                return RedirectToAction(ActionName.Index);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(IDictionary<string, string> settings)
        {
            using (var repository = GetRepository(true))
            {
                repository.SaveSettingValues(SettingsScope.PhotoProvider, settings);
                repository.CommitChanges();
                SetPageFlash("The photo provider settings were saved.");
                return RedirectToAction(ActionName.Index);
            }
        }

        #endregion

        #region Helper Methods

        private static void ResetProvider(MayandoRepository repository, SiteData siteData)
        {
            var provider = PhotoProviderFactory.CreateProvider(siteData.Settings.PhotoProviderGuid);
            if (provider != null)
            {
                provider.Reset();
            }
            Tasks.SavePhotoProviderSynchronizationResults(repository, siteData.Settings, new SynchronizationStatus(null, null, null, null, null, null));
        }

        #endregion
    }
}