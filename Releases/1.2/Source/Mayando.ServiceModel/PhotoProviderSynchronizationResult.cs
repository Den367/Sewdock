using System;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Mayando.ServiceModel
{
    /// <summary>
    /// The result of a synchronization with the photo provider.
    /// </summary>
    [DataContract(Namespace = "http://schemas.mayando.codeplex.com/2010/06/")]
    public class PhotoProviderSynchronizationResult
    {
        /// <summary>
        /// Gets or sets a value that determines if synchronization was successfull.
        /// </summary>
        [DataMember]
        public bool Succeeded { get; set; }

        /// <summary>
        /// The last synchronization time (UTC), or <see langword="null"/> if synchronization was never performed yet.
        /// </summary>
        [DataMember]
        public DateTime? LastSyncTimeUtc { get; set; }

        /// <summary>
        /// Gets the description of the last synchronization status, formatted as HTML.
        /// </summary>
        [DataMember]
        public string LastSyncStatusHtml { get; set; }

        /// <summary>
        /// The number of new photos that were synchronized, or <see langword="null"/> if synchronization was never performed yet.
        /// </summary>
        [DataMember]
        public int? LastSyncNewPhotos { get; set; }

        /// <summary>
        /// The number of new comments that were synchronized, or <see langword="null"/> if synchronization was never performed yet.
        /// </summary>
        [DataMember]
        public int? LastSyncNewComments { get; set; }
    }
}