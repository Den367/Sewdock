using System;
using Mayando.Web.Infrastructure;

namespace Mayando.Web.Models
{
    public partial class Log
    {
        #region Convenience Properties

        public LogLevel LogLevel
        {
            get
            {
                return (LogLevel)this.Level;
            }
            set
            {
                this.Level = (int)value;
            }
        }

        public DateTimeOffset Time
        {
            get
            {
                return new DateTimeOffset(this.TimeUtc, TimeSpan.Zero);
            }
            set
            {
                this.TimeUtc = value.UtcDateTime;
            }
        }

        #endregion
    }
}
