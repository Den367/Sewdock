using System.Collections.Generic;
using System.ComponentModel;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Properties;

namespace Mayando.Web.Models
{
    public partial class Gallery : IDataErrorInfo, ISupportSequence
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
        //   // this.Slug = this.Title.GenerateSlug();
        //}

        public string Slug { get; set; }
        #endregion

        #region Convenience Properties

        public string TagList
        {
            get
            {
                return Converter.ToTagList(this.Tags);
            }
        }

        #endregion

        public IEnumerable<string> Tags { get; set; }

        public int Sequence { get; set; }

        public object CoverPhoto { get; set; }

        public object Title { get; set; }

        public string Description { get; set; }
        public string Id { get; set; }
    }
}