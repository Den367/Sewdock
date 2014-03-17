using System.Web.Mvc;
using Mayando.Web.Extensions;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// Provides a base class for master pages.
    /// </summary>
    public abstract class SiteViewMasterPage : ViewMasterPage
    {
        #region Properties

        /// <summary>
        /// Gets the master view model.
        /// </summary>
        public MasterViewModel MasterModel
        {
            get
            {
                return this.ViewData.GetMasterViewModel();
            }
        }

        #endregion
    }
}