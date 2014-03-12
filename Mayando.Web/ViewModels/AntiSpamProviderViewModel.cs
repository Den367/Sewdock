using System.Collections.Generic;
using System.Web.Mvc;
using Mayando.ProviderModel.AntiSpamProviders;
using Mayando.Web.Providers;

namespace Mayando.Web.ViewModels
{
    public class AntiSpamProviderViewModel
    {
        private AntiSpamProviderInfo providerInfo;
        private object settingsViewModel;

        public IEnumerable<SelectListItem> AvailableProviders { get; private set; }
        public AntiSpamProviderInfo Provider { get; private set; }
        public AntiSpamProviderStatus ProviderStatus { get; private set; }
        public SettingsViewModel Settings { get; private set; }

        public AntiSpamProviderViewModel(IEnumerable<SelectListItem> availableProviders, AntiSpamProviderInfo provider, AntiSpamProviderStatus providerStatus, SettingsViewModel settings)
        {
            this.AvailableProviders = availableProviders;
            this.Provider = provider;
            this.ProviderStatus = providerStatus;
            this.Settings = settings;
        }

        public AntiSpamProviderViewModel(IEnumerable<SelectListItem> availableProviders, AntiSpamProviderInfo providerInfo, AntiSpamProviderStatus providerStatus, object settingsViewModel)
        {
            // TODO: Complete member initialization
            this.AvailableProviders = availableProviders;
            this.providerInfo = providerInfo;
            this.ProviderStatus = providerStatus;
            this.settingsViewModel = settingsViewModel;
        }
    }
}