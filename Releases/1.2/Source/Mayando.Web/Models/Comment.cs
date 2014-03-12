using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using Mayando.Web.Properties;

namespace Mayando.Web.Models
{
    public partial class Comment : IDataErrorInfo
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

        partial void OnAuthorNameChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.errors["AuthorName"] = Resources.ValidationAuthorNameEmpty;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Net.Mail.MailAddress")]
        partial void OnAuthorEmailChanging(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                // An email address was given, see if it's valid.
                try
                {
                    new MailAddress(value);
                }
                catch (FormatException)
                {
                    this.errors["AuthorEmail"] = Resources.ValidationAuthorEmailInvalid;
                }
            }
        }

        partial void OnTextChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.errors["Text"] = Resources.ValidationTextEmpty;
            }
        }

        #endregion

        #region Convenience Properties

        public DateTimeOffset DatePublished
        {
            get
            {
                return new DateTimeOffset(this.DatePublishedUtc, TimeSpan.Zero);
            }
            set
            {
                this.DatePublishedUtc = value.UtcDateTime;
            }
        }

        #endregion
    }
}