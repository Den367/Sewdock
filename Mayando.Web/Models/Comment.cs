using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using Mayando.Web.Properties;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Models
{
    public partial class Comment : CaptchaBase,IDataErrorInfo
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

      

        #endregion

        #region Convenience Properties

        public DateTimeOffset DatePublished
        {
            get;
            set;
        }

        #endregion
        public string Text { get; set; }

        public string AuthorUrl { get; set; }

        public string AuthorName { get; set; }

        public int Id { get; set; }

        public bool AuthorIsOwner { get; set; }

        public string AuthorEmail { get; set; }

        public int EmbroId { get; set; }

        public string ExternalID { get; set; }
           
        public string UserName { get; set; }

        #region Constructors
        public Comment()
        {
            DatePublished = DateTime.UtcNow;
            GenerateCaptcha();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="embroId">The id of the photo this comment is for in the external service.</param>
        /// <param name="text">The actual text of the comment.</param>
        /// <param name="datePublished">The date the comment was published.</param>
        public Comment(int embroId, string text, DateTimeOffset datePublished)
        {
           
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="embroId"></param>
      /// <param name="text"></param>
      /// <param name="datePublished"></param>
      /// <param name="userName"></param>
      /// <param name="authorName"></param>
      /// <param name="authorEmail"></param>
      /// <param name="authorUrl"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "7#")]
        public Comment(int embroId, string text, DateTimeOffset datePublished, string userName, string authorName, string authorEmail, string authorUrl)
        {
           
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("The comment text must be specified.");
            }
            this.Text = text;
            this.DatePublished = datePublished;
            this.EmbroId = embroId;
            UserName = userName;
            this.AuthorName = authorName;
            this.AuthorEmail = authorEmail;
            this.AuthorUrl = authorUrl;
    
        }

        #endregion



    }
}