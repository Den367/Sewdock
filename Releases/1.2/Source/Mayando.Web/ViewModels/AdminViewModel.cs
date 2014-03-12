using System.Collections.Generic;

namespace Mayando.Web.ViewModels
{
    public class AdminViewModel
    {
        public SettingsViewModel Settings { get; private set; }

        public AdminViewModel(SettingsViewModel settings)
        {
            this.Settings = settings;
        }
    }
}