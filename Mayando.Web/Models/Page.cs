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

        //partial void OnTitleChanging(string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        this.errors["Title"] = Resources.ValidationPageTitleEmpty;
        //    }
        //}

        #endregion

        #region Generate Slug

        //partial void OnTitleChanged()
        //{
        //    this.Slug = this.Title.GenerateSlug();
        //}

        #endregion

        public string Title { get; set; }

        public string Slug { get; set; }

        public bool HidePhotoComments { get; set; }

        public bool HidePhotoText { get; set; }

        public bool ShowContactForm { get; set; }

        public string Text { get; set; }

        public int Id { get; set; }
    }
}