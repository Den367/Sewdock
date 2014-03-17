using System;

namespace Mayando.Web.ViewModels
{
    public class SynchronizationStatus
    {
        public bool? LastSyncSucceeded { get; private set; }
        public DateTimeOffset? LastSyncTime { get; private set; }
        public string LastSyncStatus { get; private set; }
        public int? LastSyncNewPhotos { get; private set; }
        public int? LastSyncNewComments { get; private set; }
        public bool? LastSyncWasSimulation { get; private set; }

        public SynchronizationStatus(bool? lastSyncSucceeded, DateTimeOffset? lastSyncTime, string lastSyncStatus, int? lastSyncNewPhotos, int? lastSyncNewComments, bool? lastSyncWasSimulation)
        {
            this.LastSyncSucceeded = lastSyncSucceeded;
            this.LastSyncTime = lastSyncTime;
            this.LastSyncStatus = lastSyncStatus;
            this.LastSyncNewPhotos = lastSyncNewPhotos;
            this.LastSyncNewComments = lastSyncNewComments;
            this.LastSyncWasSimulation = lastSyncWasSimulation;
        }
    }
}