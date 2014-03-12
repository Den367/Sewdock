using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Mayando.ProviderModel.PhotoProviders;
using Mayando.Web.Extensions;
using Mayando.Web.Models;
using Mayando.Web.Providers;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// Performs common tasks.
    /// </summary>
    public static class Tasks
    {
        #region Fields

        /// <summary>
        /// The lock object to use when synchronizing with the photo provider.
        /// </summary>
        private static object photoProviderSynchronizationLock = new object();

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value that determins if a synchronization with the photo provider is currently running.
        /// </summary>
        public static bool PhotoProviderIsSynchronizing { get; private set; }

        #endregion

        #region SynchronizePhotoProvider

        /// <summary>
        /// Starts a synchronization with the photo provider.
        /// </summary>
        /// <returns>The result of the synchronization.</returns>
        /// <param name="syncStartTime">The start time from which to include all changes.</param>
        /// <param name="tagList">The tags that the photos must all have to be synchronized.</param>
        /// <param name="simulate">Determines if the synchronization is only a simulation (where no changes are actually saved).</param>
        /// <param name="origin">Determines the origin of the request.</param>
        /// <returns>The synchronization status.</returns>
        public static SynchronizationStatus SynchronizePhotoProvider(DateTimeOffset syncStartTime, string tagList, bool simulate, RequestOrigin origin)
        {
            if (Monitor.TryEnter(photoProviderSynchronizationLock))
            {
                try
                {
                    PhotoProviderIsSynchronizing = true;
                    return SynchronizePhotoProviderInternal(syncStartTime, tagList, simulate, origin);
                }
                finally
                {
                    PhotoProviderIsSynchronizing = false;
                    Monitor.Exit(photoProviderSynchronizationLock);
                }
            }
            else
            {
                return new SynchronizationStatus(false, null, "The requested synchronization with the photo provider could not be started because another synchronization is already running.", null, null, null);
            }
        }

        /// <summary>
        /// Starts a synchronization with the photo provider.
        /// </summary>
        /// <returns>The result of the synchronization.</returns>
        /// <param name="syncStartTime">The start time from which to include all changes.</param>
        /// <param name="tagList">The tags that the photos must all have to be synchronized.</param>
        /// <param name="simulate">Determines if the synchronization is only a simulation (where no changes are actually saved).</param>
        /// <param name="origin">Determines the origin of the request.</param>
        /// <returns>The synchronization status.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static SynchronizationStatus SynchronizePhotoProviderInternal(DateTimeOffset syncStartTime, string tagList, bool simulate, RequestOrigin origin)
        {
            // Log.
            var detail = new StringBuilder();
            detail.AppendFormat(CultureInfo.CurrentCulture, "Requested from: {0}", origin.ToString()).AppendLine();
            detail.AppendFormat(CultureInfo.CurrentCulture, "Requested Simulation: {0}", simulate).AppendLine();
            detail.AppendFormat(CultureInfo.CurrentCulture, "Requested Sync Start Time: {0}", syncStartTime).AppendLine();
            detail.AppendFormat(CultureInfo.CurrentCulture, "Requested Tags: {0}", tagList).AppendLine();
            Logger.Log(LogLevel.Information, "Synchronization with photo provider started.", detail.ToString().Trim());

            // Prepare.
            var tags = Converter.ToTags(tagList);
            IList<string> existingExternalPhotoIds;
           // IList<string> existingExternalCommentIds;
            ApplicationSettings settings;
            using (var repository = GetRepository())
            {
                existingExternalPhotoIds = repository.GetExternalPhotoIds();
                //existingExternalCommentIds = repository.GetExternalCommentIds();
                settings = new ApplicationSettings(repository.GetSettingValues(SettingsScope.Application));
            }

            SynchronizationStatus result;
            var provider = PhotoProviderFactory.CreateProvider(settings.PhotoProviderGuid);
            if (provider != null)
            {
                // Synchronize.
                SynchronizationResult providerResult = null;
                try
                {
                   // providerResult = provider.Synchronize(new SynchronizationRequest(existingExternalPhotoIds, existingExternalCommentIds, syncStartTime, tags));
                }
                catch (Exception exc)
                {
                    Logger.LogException(exc);
                    providerResult = new SynchronizationResult(false, exc.GetStatusDescriptionHtml(), null);
                }
                if (providerResult == null)
                {
                    providerResult = new SynchronizationResult(false, null, null);
                }

                // Update database.
                using (var repository = GetRepository())
                {
                    var success = providerResult.Success;
                    var lastSyncTime = settings.PhotoProviderLastSyncTime;
                    var lastSyncStatus = providerResult.StatusDescriptionHtml;
                    int? lastSyncNewPhotos = null;
                    int? lastSyncNewComments = null;
                    if (!simulate)
                    {
                        if (providerResult.Success)
                        {
                            if (providerResult.PhotosToAdd != null)
                            {
                                repository.AddPhotos(providerResult.PhotosToAdd);
                                lastSyncNewPhotos = providerResult.PhotosToAdd.Count;
                            }
                            else
                            {
                                lastSyncNewPhotos = 0;
                            }
                            if (providerResult.CommentsToAdd != null)
                            {
                                repository.AddComments(providerResult.CommentsToAdd);
                                lastSyncNewComments = providerResult.CommentsToAdd.Count;
                            }
                            else
                            {
                                lastSyncNewComments = 0;
                            }
                            lastSyncTime = DateTimeOffset.UtcNow;
                        }
                    }
                    result = new SynchronizationStatus(success, lastSyncTime, lastSyncStatus, lastSyncNewPhotos, lastSyncNewComments, simulate);
                    SavePhotoProviderSynchronizationResults(repository, settings, result);
                   // repository.CommitChanges();
                }
            }
            else
            {
                result = new SynchronizationStatus(false, null, "A photo provider was not configured, synchronization could not complete.", null, null, simulate);
            }

            // Log.
            detail = new StringBuilder();
            detail.AppendFormat(CultureInfo.CurrentCulture, "Synchronization Succeeded: {0}", result.LastSyncSucceeded == true).AppendLine();
            detail.AppendFormat(CultureInfo.CurrentCulture, "Synchronization Status: {0}", result.LastSyncStatus).AppendLine();
            detail.AppendFormat(CultureInfo.CurrentCulture, "Synchronized Photos: {0}", result.LastSyncNewPhotos ?? 0).AppendLine();
            detail.AppendFormat(CultureInfo.CurrentCulture, "Synchronized Comments: {0}", result.LastSyncNewComments ?? 0);
            Logger.Log(LogLevel.Information, "Synchronization with photo provider completed.", detail.ToString().Trim());

            return result;
        }

        private static IEmbroRepository GetRepository()
        {
            return new EmbroRepository();
        }

        #endregion

        #region SavePhotoProviderSynchronizationResults

        /// <summary>
        /// Saves the photo provider synchronization result to the repository.
        /// </summary>
        /// <param name="repository">The repository to save the result to.</param>
        /// <param name="settings">The application settings.</param>
        /// <param name="result">The synchronization result.</param>
        public static void SavePhotoProviderSynchronizationResults(IEmbroRepository repository, ApplicationSettings settings, SynchronizationStatus result)
        {
            settings.PhotoProviderLastSyncSucceeded = result.LastSyncSucceeded;
            settings.PhotoProviderLastSyncTime = result.LastSyncTime;
            settings.PhotoProviderLastSyncNewPhotos = result.LastSyncNewPhotos;
            settings.PhotoProviderLastSyncNewComments = result.LastSyncNewComments;
            settings.PhotoProviderLastSyncWasSimulation = result.LastSyncWasSimulation;
            settings.PhotoProviderLastSyncStatus = result.LastSyncStatus;
           // repository.SaveSettingValues(SettingsScope.Application, settings.GetChangedSettings());
        }

        #endregion
    }
}