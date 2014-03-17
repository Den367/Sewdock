using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mayando.Web.Models
{
    public interface IDesignModel
    {

        #region [Embroidery Properties]
         string ExternalId { get; set; }
         string Svg { get; set; }
         string Title { get; set; }
         string Article { get; set; }
         bool Hidden { get; set; }
         string Html { get; set; }
         string Summary { get; set; }
         long DownloadsCount { get; set; }
         string Tags { get; set; }
         Guid UserID { get; set; }
         Guid Guid { get; set; }

         byte[] Data
         {
             get;
             set;
         }
         DateTime Created { get; set; }
         DateTime? Published { get; set; }
         string FileExtension { get; set; }
        #endregion [Embroidery Properties]



      

        #region IDataErrorInfo Members

        string Error
        {
            get;
        }

        string this[string columnName]
        {
            get;
        }

        #endregion



        #region Convenience Properties

        string DisplayTitle
        {
            get;
        }

        string DisplayTitleWithDate
        {
            get;
        }

        string TagList
        {
            get;
        }

        IEnumerable<string> TagNames
        {
            get;
        }

        string UrlSmallestAvailable
        {
            get;
        }

        string UrlLargestAvailable
        {
            get;
        }

        DateTimeOffset GetDate(EmbroDateType type);


        DateTimeOffset GetAdjustedDate(EmbroDateType type);

      



         string Type { get; set; }



    }
}
        #endregion []