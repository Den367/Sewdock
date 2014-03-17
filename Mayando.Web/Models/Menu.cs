using System.Collections.Generic;
using System.ComponentModel;
using Mayando.Web.Properties;

namespace Mayando.Web.Models
{
    public partial class Menu : IDataErrorInfo, ISupportSequence
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

        public int Id { get; set; }

        public string Title { get; set; }

        public bool OpenInNewWindow { get; set; }

        public int Sequence { get; set; }

        public string ToolTip { get; set; }

        public string Url { get; set; }
    }
}