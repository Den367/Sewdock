using System.Web.Mvc;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.ViewModels;
using EmbroideryFile;
using EmbroideryFile.QRCode;
using System.IO;
using System;

namespace Mayando.Web.Controllers
{
    public class QrcodeController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "qrcode";
   
        #endregion

        //
        // GET: /Qrcode/
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Text(string text)
        {

            return RedirectToAction(ActionName.Index);
        }

       

        public ActionResult Index(QrcodeFormViewModel m)
        {
            return View(ViewName.Index, m);
        }



        public ActionResult QrcodeForm(QrcodeFormViewModel m)
        {
            m.QrCodeText = GetTextToBeQRed(m);
            //if (!string.IsNullOrEmpty(text))
            //{
            //    qrSvg.QrStitchInfo = new QRCodeStitchInfo() { QrCodeText = text };
            //    using (Stream strm = new MemoryStream())
            //    {
            //        qrSvg.FillStreamWithSvg(strm, QrCodeSvgSize);
            //        //m.QrcodeSvgResult = qrSvg.ReadSvgStringFromStream();
            //    }
            //}

            return View(ViewName.Index, m);


        }
        
        public ActionResult QrcodeView(QrcodeFormViewModel m)
        {

            if (ModelState.IsValid)
                return PartialView(ViewName.QrcodeView.ToString(), GetTextToBeQRed(m));
            else return View(m.QrCodeText);

        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[ValidateInput(false)]
        //public FileStreamResult OpenXML()
        //{
        //    FileStreamResult result = null;
        //    string dataType = "text/xml";

        //    //http://research.microsoft.com/en-us/projects/msagl/codesamples.aspx
        //    Microsoft.Msagl.Drawing.Graph graph = this.createGraph("testMSAGLXML");
        //    Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
        //    renderer.CalculateLayout();

        //    string xml = Microsoft.Msagl.Drawing.SvgGraphWriter.Write(graph);

        //    MemoryStream ms = new MemoryStream();
        //    StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.UTF8);
        //    sw.Write(xml);
        //    sw.Flush();
        //    ms.Seek(0, SeekOrigin.Begin);
        //    result = new FileStreamResult(ms, dataType);
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", "attachment; filename=testMSAGLXML.svg");
        //    Response.ContentType = dataType;

        //    return result;
        //}

        public FileResult GetQrSvg(string text, int size)
        {
            if (string.IsNullOrEmpty(text)) return null;
            var qrSvg = new QrcodeSvg();
            qrSvg.QrStitchInfo = new QRCodeStitchInfo{QrCodeText = text};
            using (Stream strm = new MemoryStream())
            {
                
                qrSvg.FillStreamWithSvg(strm, size);
                strm.Seek(0, SeekOrigin.Begin);
                strm.Position = 0;
                return File(strm,"image/svg+xml");
            }
        }

        public ActionResult GetQrPng(string text, int width, int height)
        {
            if (string.IsNullOrEmpty(text)) return null;
            var qrPng = new QrcodePng(text, width, height);

            return new PngImageResult(qrPng);

        }

        [HttpPost]
        public ActionResult GetQrImageUrlAjax(QrcodeFormViewModel m, int width, int height)
        {
            string text = GetTextToBeQRed(m);

           JsonResult result = Json(   new QrcodeParams {Text = text,Width = width,Height = height});
            return result;
        }

        [HttpGet]
        public FileResult DownloadQrCodeDstFile(string text, QrcodeDst qrcodeDst )
        {
            if (qrcodeDst != null) qrcodeDst.QrStitchInfo = new QRCodeStitchInfo() { QrCodeText = text };

            Stream stream = new MemoryStream();
            
                qrcodeDst.FillStreamWithDst(stream);
                return File(stream, "application/octet-stream", string.Format("qr_code_{0}.dst", Guid.NewGuid()));
            

        }


        static string GetTextToBeQRed(QrcodeFormViewModel m)
        {
            string text;
            switch (m.FormKind)
            {
                case QrcodeFormKind.Text:
                    text = m.Text;
                    break;
                case QrcodeFormKind.Url:
                    text = m.LongUrl; break;
                case QrcodeFormKind.vCard:
                    text = string.Format(VCARD_TEMPLATE_STR, m.Name, m.Surname, m.Company, m.Phone, m.Address, m.Email, m.SiteUrl, m.Notes);
                    break;
                case QrcodeFormKind.Location:
                    text = string.Format(GEO_TEMPLATE_STR, m.Longitude,m.Latitude);
                    break;
                default:
                    text = string.Empty;
                    break;
            }
            return text;
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
