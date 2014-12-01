using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using Myembro.Properties;

namespace Myembro.Models
{
    public class ContactForm : IDataErrorInfo
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

        #region Properties

        private string authorName;
        public string AuthorName
        {
            get { return this.authorName; }
            set
            {
                this.authorName = value;
                if (string.IsNullOrEmpty(value))
                {
                    this.errors["AuthorName"] =  Myembro.Properties.Resources.ValidationAuthorNameEmpty;
                }
            }
        }

        private string authorEmail;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Net.Mail.MailAddress")]
        public string AuthorEmail
        {
            get { return this.authorEmail; }
            set
            {
                this.authorEmail = value;
                if (string.IsNullOrEmpty(value))
                {
                    this.errors["AuthorEmail"] =  Myembro.Properties.Resources.ValidationAuthorEmailEmpty;
                }
                else
                {
                    // An email address was given, see if it's valid.
                    try
                    {
                        new MailAddress(value);
                    }
                    catch (FormatException)
                    {
                        this.errors["AuthorEmail"] =  Myembro.Properties.Resources.ValidationAuthorEmailInvalid;
                    }
                }
            }
        }

        private string text;
        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                if (string.IsNullOrEmpty(value))
                {
                    this.errors["Text"] =  Myembro.Properties.Resources.ValidationTextEmpty;
                }
            }
        }

        public bool RememberMe { get; set; }

        #endregion
    }
}