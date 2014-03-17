using System.Collections.Generic;
using System.ComponentModel;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Properties;

namespace Mayando.Web.Models
{
    public partial class Page : IDataErrorInfo
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

        partial void OnTitleChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.errors["Title"] = Resources.ValidationPageTitleEmpty;
            }
        }

        #endregion

        #region Generate Slug

        partial void OnTitleChanged()
        {
            this.Slug = this.Title.GenerateSlug();
        }

        #endregion
    }
}