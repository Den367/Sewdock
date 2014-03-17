using System;

namespace Mayando.ProviderModel.PhotoProviders
{
    /// <summary>
    /// Represents a EmbroideryItem Provider that can be used in Mayando.
    /// </summary>
    public interface IPhotoProvider : IProvider
    {
        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="host">The host managing this provider.</param>
        void Initialize(IPhotoProviderHost host);

        /// <summary>
        /// Gets the current status of the provider.
        /// </summary>
        /// <returns>A status description of the provider.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        PhotoProviderStatus GetStatus();
        
        /// <summary>
        /// Synchronizes photos between the provider and Mayando.
        /// </summary>
        /// <param name="request">The request that contains the details about the photos to be synchronized.</param>
        /// <returns>The results of the synchronization.</returns>
        SynchronizationResult Synchronize(SynchronizationRequest request);
    }
}