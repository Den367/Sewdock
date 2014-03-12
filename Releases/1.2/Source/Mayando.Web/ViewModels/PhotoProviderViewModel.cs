using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Mayando.ProviderModel.PhotoProviders;
using Mayando.Web.Providers;

namespace Mayando.Web.ViewModels
{
    public class PhotoProviderViewModel
    {
        public IEnumerable<SelectListItem> AvailableProviders { get; private set; }
        public PhotoProviderInfo Provider { get; private set; }
        public PhotoProviderStatus ProviderStatus { get; private set; }
        public SynchronizationStatus SyncStatus { get; private set; }
        public SettingsViewModel Settings { get; private set; }
        public DateTimeOffset ProposedSyncStartTime { get; private set; }
        public bool PhotoProviderIsSynchronizing { get; private set; }
        public bool AutoSyncEnabled { get; private set; }
        public int? AutoSyncIntervalMinutes { get; private set; }
        public string TagList { get; private set; }

        public PhotoProviderViewModel(IEnumerable<SelectListItem> availableProviders, PhotoProviderInfo provider, PhotoProviderStatus providerStatus, SynchronizationStatus syncStatus, SettingsViewModel settings, DateTimeOffset proposedSyncStartTime, bool photoProviderIsSynchronizing, bool autoSyncEnabled, int? autoSyncIntervalMinutes, string autoSyncTags)
        {
            this.AvailableProviders = availableProviders;
            this.Provider = provider;
            this.ProviderStatus = providerStatus;
            this.SyncStatus = syncStatus;
            this.Settings = settings;
            this.ProposedSyncStartTime = proposedSyncStartTime;
            this.PhotoProviderIsSynchronizing = photoProviderIsSynchronizing;
            this.AutoSyncEnabled = autoSyncEnabled;
            this.AutoSyncIntervalMinutes = autoSyncIntervalMinutes;
            this.TagList = autoSyncTags;
        }
    }
}