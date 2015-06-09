
using System.Collections.Generic;
using System.ComponentModel;
using Myembro.Models;

namespace Myembro.ViewModels
{
    public class MenuViewModel : IDataErrorInfo
    {
        #region Fields

        private IDictionary<string, string> errors = new Dictionary<string, string>();

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (this.errors.ContainsKey(columnName))
                {
                    return this.errors[columnName];
                }
                return string.Empty;
            }
        }

        #endregion

        #region Validation

        //partial void OnTitleChanging(string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        this.errors["Title"] = Resources.ValidationMenuTitleEmpty;
        //    }
        //}

        //partial void OnUrlChanging(string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        this.errors["Url"] = Resources.ValidationMenuUrlEmpty;
        //    }
        //}

        #endregion
        public string Url { get; private set; }
        public string Title { get; private set; }
        public string ToolTip { get; private set; }
        public bool OpenInNewWindow { get; private set; }
        public bool Selected { get; set; }
        public string CssClass { get; set; }
        public MenuViewModel(Menu menu)
        {
            this.Url = menu.Url;
            this.Title = menu.Title;
            this.OpenInNewWindow = menu.OpenInNewWindow;
            this.ToolTip = menu.ToolTip;
            this.CssClass = menu.cssClass;
        }
    }
}