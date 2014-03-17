using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Mayando.ProviderModel.AntiSpamProviders;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Providers;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with the anti-spam provider.")]
    public class AntiSpamProviderController : ProviderControllerBase
    {
        #region Constants

        public const string ControllerName = "antispamprovider";

        #endregion

        #region Actions

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), Description("Shows the Anti-Spam Provider administration page.")]
        public ActionResult Index()
        {
            using (var repository = GetRepository())
            {
                // Get provider info.
                var availableProviders = GetProviderList(AntiSpamProviderFactory.GetAvailableProviders(), this.SiteData.Settings.AntiSpamProviderGuid);
                var providerInfo = AntiSpamProviderFactory.GetProviderInfo(this.SiteData.Settings.AntiSpamProviderGuid);

                // Get provider status.
                AntiSpamProviderStatus providerStatus = null;
                var provider = AntiSpamProviderFactory.CreateProvider(this.SiteData.Settings.AntiSpamProviderGuid);
                if (provider != null)
                {
                    try
                    {
                        providerStatus = provider.GetStatus();
                    }
                    catch (Exception exc)
                    {
                        Logger.LogException(exc);
                        providerStatus = new AntiSpamProviderStatus(exc.GetStatusDescriptionHtml(), true);
                    }
                }
                if (providerStatus == null)
                {
                    providerStatus = new AntiSpamProviderStatus(null, true);
                }

                // Get provider settings.
                var settings = repository.GetSettings(SettingsScope.AntiSpamProvider);
                var settingsViewModel = SettingsController.GetSettingsViewModel(settings, this.ControllerContext.HttpContext);

                // Create full view model with all details.
                var model = new AntiSpamProviderViewModel(availableProviders, providerInfo, providerStatus, settingsViewModel);
                return View(ViewName.Index, model);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SelectProvider(string selectedProvider)
        {
            if (!string.Equals(selectedProvider, this.SiteData.Settings.AntiSpamProviderGuid, StringComparison.OrdinalIgnoreCase))
            {
                using (var repository = GetRepository(true))
                {
                    this.SiteData.Settings.AntiSpamProviderGuid = selectedProvider;
                    repository.SaveSettingValues(SettingsScope.Application, this.SiteData.Settings.GetChangedSettings());
                    ResetProvider(selectedProvider);
                    repository.CommitChanges();
                    SetPageFlash("The anti-spam provider was changed.");
                }
            }
            return RedirectToAction(ActionName.Index);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Reset()
        {
            ResetProvider(this.SiteData.Settings.AntiSpamProviderGuid);
            SetPageFlash("The anti-spam provider was reset.");
            return RedirectToAction(ActionName.Index);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(IDictionary<string, string> settings)
        {
            using (var repository = GetRepository(true))
            {
                repository.SaveSettingValues(SettingsScope.AntiSpamProvider, settings);
                repository.CommitChanges();
                SetPageFlash("The anti-spam provider settings were saved.");
                return RedirectToAction(ActionName.Index);
            }
        }

        #endregion

        #region Helper Methods

        private static void ResetProvider(string providerGuid)
        {
            var provider = AntiSpamProviderFactory.CreateProvider(providerGuid);
            if (provider != null)
            {
                provider.Reset();
            }
        }

        #endregion
    }
}