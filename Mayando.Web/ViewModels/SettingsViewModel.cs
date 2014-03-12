using System.Collections.Generic;
using System.Linq;

namespace Mayando.Web.ViewModels
{
    public class SettingsViewModel
    {
        public ICollection<SettingViewModel> Settings { get; private set; }
        public bool AllowDelete { get; private set; }
        public IList<SettingViewModel> UserVisibleSettings { get; private set; }

        public SettingsViewModel(ICollection<SettingViewModel> settings, bool allowDelete)
        {
            this.Settings = settings;
            this.AllowDelete = allowDelete;
            this.UserVisibleSettings = this.Settings.Where(s => s.Setting.UserVisible).ToList();
        }
    }
}