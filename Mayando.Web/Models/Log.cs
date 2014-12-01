using System;
using Myembro.Infrastructure;

namespace Myembro.Models
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
                this.Level = value;
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

        public string Detail { get; set; }

        public Infrastructure.LogLevel Level { get; set; }

        public string Message { get; set; }

        public DateTime TimeUtc { get; set; }
    }
}
