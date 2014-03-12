using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Properties;

namespace Mayando.Web.Models
{
    public partial class Photo : IDataErrorInfo
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

        partial void OnUrlNormalChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.errors["UrlNormal"] = Resources.ValidationPhotoUrlNormalEmpty;
            }
        }

        partial void OnDatePublishedUtcChanging(DateTime value)
        {
            if (value < DateTimeOffsetExtensions.MinValue)
            {
                this.errors["DatePublished"] = Resources.ValidationPhotoDatePublishedInvalid;
                this.errors["DatePublishedUtc"] = Resources.ValidationPhotoDatePublishedInvalid;
            }
        }

        partial void OnDateTakenUtcChanging(DateTime? value)
        {
            if (value.HasValue && value.Value < DateTimeOffsetExtensions.MinValue)
            {
                this.errors["DateTaken"] = Resources.ValidationPhotoDateTakenInvalid;
                this.errors["DateTakenUtc"] = Resources.ValidationPhotoDateTakenInvalid;
            }
        }

        #endregion

        #region Convenience Properties

        public string DisplayTitle
        {
            get
            {
                return (string.IsNullOrEmpty(this.Title) ? this.DatePublished.ToAdjustedDisplayString() : this.Title);
            }
        }

        public string DisplayTitleWithDate
        {
            get
            {
                return (string.IsNullOrEmpty(this.Title) ? this.DatePublished.ToAdjustedDisplayString() : string.Format(CultureInfo.CurrentCulture, "{0} ({1})", this.Title, this.DatePublished.ToAdjustedDisplayString()));
            }
        }

        public string TagList
        {
            get
            {
                return Converter.ToTagList(this.Tags);
            }
        }

        public IEnumerable<string> TagNames
        {
            get
            {
                return Converter.ToTagNames(this.Tags);
            }
        }

        public string UrlSmallestAvailable
        {
            get
            {
                if (!string.IsNullOrEmpty(this.UrlThumbnailSquare))
                {
                    return this.UrlThumbnailSquare;
                }
                if (!string.IsNullOrEmpty(this.UrlThumbnail))
                {
                    return this.UrlThumbnail;
                }
                if (!string.IsNullOrEmpty(this.UrlSmall))
                {
                    return this.UrlSmall;
                }
                if (!string.IsNullOrEmpty(this.UrlNormal))
                {
                    return this.UrlNormal;
                }
                if (!string.IsNullOrEmpty(this.UrlLarge))
                {
                    return this.UrlLarge;
                }
                return null;
            }
        }

        public string UrlLargestAvailable
        {
            get
            {
                if (!string.IsNullOrEmpty(this.UrlLarge))
                {
                    return this.UrlLarge;
                }
                if (!string.IsNullOrEmpty(this.UrlNormal))
                {
                    return this.UrlNormal;
                }
                if (!string.IsNullOrEmpty(this.UrlSmall))
                {
                    return this.UrlSmall;
                }
                if (!string.IsNullOrEmpty(this.UrlThumbnail))
                {
                    return this.UrlThumbnail;
                }
                if (!string.IsNullOrEmpty(this.UrlThumbnailSquare))
                {
                    return this.UrlThumbnailSquare;
                }
                return null;
            }
        }

        public DateTimeOffset GetDate(PhotoDateType type)
        {
            if (type == PhotoDateType.Taken)
            {
                return (this.DateTaken ?? this.DatePublished);
            }
            else
            {
                return this.DatePublished;
            }
        }

        public DateTimeOffset GetAdjustedDate(PhotoDateType type)
        {
            return GetDate(type).AdjustFromUtc();
        }

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

        public DateTimeOffset? DateTaken
        {
            get
            {
                return (this.DateTakenUtc.HasValue ? new DateTimeOffset(this.DateTakenUtc.Value, TimeSpan.Zero) : (DateTimeOffset?)null);
            }
            set
            {
                this.DateTakenUtc = (value.HasValue ? value.Value.UtcDateTime : (DateTime?)null);
            }
        }

        #endregion
    }
}