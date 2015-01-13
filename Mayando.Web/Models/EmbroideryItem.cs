using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Myembro.Extensions;
using Myembro.Infrastructure;
using Myembro.Properties;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using EmbroideryFile;
using System.Xml.Linq;
using System.Linq;

namespace Myembro.Models
{
    public class EmbroideryItem : IDataErrorInfo
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


        #region [Fields and AutoProps]
       
        EmbroideryType _Type;
        byte[] _data;
        IList<string> tagList;        
        DateTimeOffset _Created;
        DateTimeOffset? _Published;


        #endregion [Fields]

        #region [Properties]
        public int Id { get; set; }
        public string DisplayTitle { get; set; }
        public string Json { get; set; }
        public string Png { get; set; }
        public string Svg { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Article { get; set; }
        public bool Hidden { get; set; }
        [AllowHtml]
        public string Html { get; set; }
        public string Summary { get; set; }
        public long DownloadsCount { get; set; }

        public IEnumerable<string> TagList { get { return tagList; } }
        /// <summary>
        /// Returns tags wrapped in to xml
        /// </summary>
        public string Tags
        {            
            get 
            {
                return Converter.ToTagXmled(tagList);                          
            }
            set
            {
                tagList = Converter.ToTagList(value);
               
            }
        }

        public string XmlTags { set { tagList = Converter.FromTagXmled(value); } }

        public string UserID { get; set; }

        public int TypeID
        {
           get { return (int) _Type; }
            set { _Type = (EmbroideryType)Enum.ToObject(typeof(EmbroideryType), value); }
        }

        //public EmbroType EmbroType {  }

        public byte[] Data
        {
            set
            {
                int len = value.Length;
                _data = new byte[len];
                Buffer.BlockCopy(value, 0, _data, 0, len);

            }
            get { return _data; }
        }
        public DateTimeOffset Created { get { return _Created; } set { _Created = value; } }
        public DateTimeOffset? Published { get { return _Published; } set { _Published = value; } }
        public string FileExtension {
            set
            {
                switch (value.ToLower())
                {
                    case ".pes": _Type = EmbroideryType.PES; break;
                    case ".pec": _Type = EmbroideryType.PEC; break;
                    case ".dst": _Type = EmbroideryType.DST; break;
                    default: _Type = 0; break;

                }
            }
            get { return  _Type.ToString(); }
        }
        #endregion [Properties]

        #region [ctor]
        public EmbroideryItem()
        {            
            _Created = DateTime.UtcNow;
            DownloadsCount = 0;
            Id = 0;
        }
        #endregion [ctor]

    }
}