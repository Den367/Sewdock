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

        public string Name { get; set; }

        public string Scope { get; set; }

        public bool UserVisible { get; set; }

        public string Value { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public int Sequence { get; set; }
    }
}