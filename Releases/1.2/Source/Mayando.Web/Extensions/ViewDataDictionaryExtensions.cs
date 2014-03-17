using System.Collections.Generic;
using System.Web.Mvc;
using Mayando.Web.Controllers;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ViewDataDictionary"/> instances.
    /// </summary>
    public static class ViewDataDictionaryExtensions
    {
        #region Extension Methods

        /// <summary>
        /// Gets the master view model.
        /// </summary>
        /// <param name="viewData">The view data.</param>
        /// <returns>The master view model stored in the view data, or <see langword="null"/> if it doesn't exist.</returns>
        public static MasterViewModel GetMasterViewModel(this ViewDataDictionary viewData)
        {
            return GetViewDataItem<MasterViewModel>(viewData, SiteControllerBase.ViewDataKeyMasterViewModel);
        }

        /// <summary>
        /// Gets the photos.
        /// </summary>
        /// <param name="viewData">The view data.</param>
        /// <returns>The list of photos stored in the view data, or <see langword="null"/> if it doesn't exist.</returns>
        public static IEnumerable<SelectListItem> GetPhotos(this ViewDataDictionary viewData)
        {
            return GetViewDataItem<IEnumerable<SelectListItem>>(viewData, SiteControllerBase.ViewDataKeyPhotos);
        }

        /// <summary>
        /// Gets the galleries.
        /// </summary>
        /// <param name="viewData">The view data.</param>
        /// <returns>The list of galleries stored in the view data, or <see langword="null"/> if it doesn't exist.</returns>
        public static IEnumerable<SelectListItem> GetGalleries(this ViewDataDictionary viewData)
        {
            return GetViewDataItem<IEnumerable<SelectListItem>>(viewData, SiteControllerBase.ViewDataKeyGalleries);
        }

        /// <summary>
        /// Gets the available log levels.
        /// </summary>
        /// <param name="viewData">The view data.</param>
        /// <returns>The list of available log levels.</returns>
        public static IEnumerable<SelectListItem> GetLogLevels(this ViewDataDictionary viewData)
        {
            return GetViewDataItem<IEnumerable<SelectListItem>>(viewData, SiteControllerBase.ViewDataKeyLogLevels);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets an item from the view data.
        /// </summary>
        /// <typeparam name="T">The type of the item to retrieve.</typeparam>
        /// <param name="viewData">The view data.</param>
        /// <param name="key">The view data key.</param>
        /// <returns>The item from the view data, or <see langword="null"/> if it doesn't exist.</returns>
        private static T GetViewDataItem<T>(ViewDataDictionary viewData, string key)
        {
            if (viewData != null && viewData.ContainsKey(key))
            {
                return (T)viewData[key];
            }
            return default(T);
        }

        #endregion
    }
}