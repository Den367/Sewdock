using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using Mayando.Web.Enumerations;
using Mayando.Web.Infrastructure;
 using Mayando.Web.Properties;
namespace Mayando.Web.ViewModels
{
    public class QrcodeFormViewModel
    {
        private string _qrCodeText = string.Empty;


        public string QrCodeText
        {
            get
            {
               
                switch (FormKind)
                {
                    case QrcodeFormKind.Text:

                        return Text;
                    case QrcodeFormKind.Url:
                        return LongUrl;
                    case QrcodeFormKind.vCard:
                        return string.Format(VCARD_TEMPLATE_STR, Name, Surname, Company, Phone, Address, Email, SiteUrl, Notes);
                    case QrcodeFormKind.Location:
                        return string.Format(CultureInfo.CurrentCulture, GEO_TEMPLATE_STR, Longitude, Latitude);
                      
                    default:
                        throw new ArgumentOutOfRangeException();
                }
              
            }
            set { _qrCodeText = value; }
        }

        public string Text   { get; set; }

        public string LongUrl { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SiteUrl { get; set; }
        public string Company { get; set; }
        public string Notes { get; set; }
        [StringLength(24)]
        [Display(ResourceType = typeof(Mayando.Web.Properties.Resources), Description = "LongitudeText")]
        [RegularExpression(@"^-?([1]?[1-7][1-9]|[1]?[1-8][0]|[1-9]?[0-9])\.{1}\d{1,6}", ErrorMessageResourceType = typeof(Mayando.Web.Properties.Resources), ErrorMessageResourceName = "IncorrectNumberValue")]
        public string Longitude { get; set; }
      [StringLength(24)]
      [Display(ResourceType = typeof(Mayando.Web.Properties.Resources), Description = "LatitudeText")]
        [RegularExpression(@"^-?([1-8]?[1-9]|[1-9]0)\.{1}\d{1,6}", ErrorMessageResourceType = typeof(Mayando.Web.Properties.Resources), ErrorMessageResourceName = "IncorrectNumberValue")]
        public string Latitude { get; set; }
        public QrcodeFormKind FormKind {get;set;}
        public string QrcodeSvgResult { get; set; }


        public QrcodeFormViewModel()
        {
            FormKind = QrcodeFormKind.Text;
            Text = string.Empty;
            LongUrl = @"http://";
            Name = "John";
            Surname = "Doe";
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            SiteUrl = string.Empty;
            QrcodeSvgResult = string.Empty;
        }

        const string VCARD_TEMPLATE_STR = @"BEGIN:VCARD
VERSION:3.0
N:{0}
FN:{1}
ORG:{2}
TEL:{3}
ADR:{4}
EMAIL:{5}
URL:{6}
NOTE:{7}
END:VCARD";
        const string GEO_TEMPLATE_STR = @"GEO:{0},{1}";
    }
}