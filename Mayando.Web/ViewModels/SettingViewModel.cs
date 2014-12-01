using System.Collections.Generic;
using System.Web.Mvc;
using Myembro.Models;

namespace Myembro.ViewModels
{
    public class SettingViewModel
    {
        public Setting Setting { get; private set; }
        public IEnumerable<SelectListItem> AllowedValues { get; private set; }
        public string DeleteUrl { get; private set; }

        public SettingViewModel(Setting setting, IEnumerable<SelectListItem> allowedValues)
            : this(setting, allowedValues, null)
        {
        }

        public SettingViewModel(Setting setting, IEnumerable<SelectListItem> allowedValues, string deleteUrl)
        {
            this.Setting = setting;
            this.AllowedValues = allowedValues;
            this.DeleteUrl = deleteUrl;
        }
    }
}