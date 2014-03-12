using System;
using Mayando.ProviderModel;

namespace Mayando.Web.Models
{
    public partial class Setting
    {
        public string DisplayTitle
        {
            get
            {
                return (string.IsNullOrEmpty(this.Title) ? this.Name : this.Title);
            }
        }

        public SettingType SettingType
        {
            get
            {
                return (SettingType)Enum.Parse(typeof(SettingType), this.Type, true);
            }
        }
    }
}