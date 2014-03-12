using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JelleDruyts.Web.Mvc;
using Mayando.ProviderModel;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Properties;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with settings.")]
    [AuthorizeAdministrator]
    public class SettingsController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "settings";

        #endregion

        #region Actions

        [Description("Allows the user to edit the application settings.")]
        public ActionResult Index()
        {
            using (var repository = GetRepository())
            {
                var settings = repository.GetSettings(SettingsScope.Application);
                var settingsViewModel = GetSettingsViewModel(settings, this.ControllerContext.HttpContext);
                return View(ViewName.Index, settingsViewModel);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(IDictionary<string, string> settings)
        {
            using (var repository = GetRepository(true))
            {
                repository.SaveSettingValues(SettingsScope.Application, settings);
                repository.CommitChanges();
                SetPageFlash("The application settings were saved.");
                return RedirectToAction(ActionName.Index, AdminController.ControllerName);
            }
        }

        [Description("Allows the user to edit the user-defined settings.")]
        [ActionName("User")]
        public ActionResult UserSettings()
        {
            using (var repository = GetRepository())
            {
                var settings = repository.GetSettings(SettingsScope.User);
                var settingsViewModel = GetSettingsViewModel(settings, this.ControllerContext.HttpContext, true, this.Url);
                return View(ViewName.User, settingsViewModel);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult EditUserSettings(IDictionary<string, string> settings)
        {
            using (var repository = GetRepository(true))
            {
                repository.SaveSettingValues(SettingsScope.User, settings);
                repository.CommitChanges();
                SetPageFlash("The user-defined settings were saved.");
                return RedirectToAction(ActionName.User, SettingsController.ControllerName);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Add(string name, bool isHtml)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("Name", "The name is required.");
            }
            if (!ModelState.IsValid)
            {
                return UserSettings();
            }
            using (var repository = GetRepository(true))
            {
                var setting = new Setting();
                setting.Name = name;
                setting.Title = name;
                setting.Type = (isHtml ? SettingType.Html : SettingType.Plaintext).ToString();
                setting.UserVisible = true;
                setting.Scope = SettingsScope.User.ToString();
                repository.AddSetting(setting);
                repository.CommitChanges();
                SetPageFlash("The user-defined setting was added.");
                return RedirectToAction(ActionName.User, SettingsController.ControllerName);
            }
        }

        [Description("Allows the user to delete a user-defined setting.")]
        [AuthorizeAdministrator]
        public ActionResult Delete([Description("The name of the user-defined setting to delete.")]string name)
        {
            return ViewForDelete("User-Defined Setting", s => name, r => r.GetSetting(SettingsScope.User, name));
        }

        [AuthorizeAdministrator]
        [ActionName(ActionNameDelete)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeletePost(string name)
        {
            return PerformDelete(r => r.DeleteSetting(SettingsScope.User, name), ActionName.User);
        }

        #endregion

        #region Helper Methods

        internal static SettingsViewModel GetSettingsViewModel(IEnumerable<Setting> settings, HttpContextBase httpContext)
        {
            return GetSettingsViewModel(settings, httpContext, false, null);
        }

        internal static SettingsViewModel GetSettingsViewModel(IEnumerable<Setting> settings, HttpContextBase httpContext, bool allowDelete, UrlHelper urlHelper)
        {
            var settingsModel = new List<SettingViewModel>();
            foreach (var setting in settings)
            {
                List<SelectListItem> allowedValues = null;
                if (setting.SettingType == SettingType.TimeZone)
                {
                    allowedValues = new List<SelectListItem>();
                    var allTimeZones = new List<TimeZoneInfo>(TimeZoneInfo.GetSystemTimeZones());
                    // Always add the UTC time zone since that isn't always installed in the system.
                    allTimeZones.Add(TimeZoneInfo.Utc);
                    foreach (var timeZone in allTimeZones.OrderBy(t => t.BaseUtcOffset))
                    {
                        var selected = string.Equals(setting.Value, timeZone.Id, StringComparison.OrdinalIgnoreCase);
                        allowedValues.Add(new SelectListItem { Value = timeZone.Id, Text = timeZone.DisplayName, Selected = selected });
                    }
                }
                else if (setting.SettingType == SettingType.Theme)
                {
                    var themes = ThemedWebFormViewEngine.GetAvailableThemes(httpContext);
                    allowedValues = new List<SelectListItem>();
                    allowedValues.Add(new SelectListItem { Value = string.Empty, Text = Resources.SelectListItemNone });
                    foreach (var theme in themes)
                    {
                        var selected = string.Equals(setting.Value, theme, StringComparison.OrdinalIgnoreCase);
                        allowedValues.Add(new SelectListItem { Value = theme, Text = theme, Selected = selected });
                    }
                }
                string deleteUrl = null;
                if (allowDelete)
                {
                    deleteUrl = urlHelper.Action(ActionName.Delete, SettingsController.ControllerName, new { name = setting.Name });
                }
                settingsModel.Add(new SettingViewModel(setting, allowedValues, deleteUrl));
            }
            return new SettingsViewModel(settingsModel, allowDelete);
        }

        #endregion
    }
}