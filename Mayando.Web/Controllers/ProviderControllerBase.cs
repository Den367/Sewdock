using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Mayando.Web.Infrastructure;
using Mayando.Web.Properties;
using Mayando.Web.Providers;

namespace Mayando.Web.Controllers
{
    [AuthorizeAdministrator]
    public abstract class ProviderControllerBase : SiteControllerBase
    {
        #region Helper Methods

        protected static IEnumerable<SelectListItem> GetProviderList(IEnumerable<ProviderInfo> availableProviders, string selectedProviderGuid)
        {
            var allowedValues = new List<SelectListItem>();
            allowedValues.Add(new SelectListItem { Value = string.Empty, Text = Resources.SelectListItemNone });
            foreach (var provider in availableProviders)
            {
                var selected = string.Equals(selectedProviderGuid, provider.Guid, StringComparison.OrdinalIgnoreCase);
                allowedValues.Add(new SelectListItem { Value = provider.Guid, Text = provider.Name, Selected = selected });
            }
            return allowedValues;
        }

        #endregion
    }
}