using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

using System.Web.Mvc;

using System.Web.Security;

using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Properties;

using Mayando.Web.ViewModels;
using System.Web;
using System.IO;


using EmbroideryFile;


namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with embro.")]
    
    public class EmbroController : SiteControllerBase
    {
        private readonly IEmbroRepository _repo;
        #region [ctor]

        public EmbroController(IEmbroRepository repo)
        {
            _repo = repo;
        }

        #endregion[ctor]
        #region Constants

        private const int ThumbnailWidth = 100;
        private const int ThumbnailHeight = 100;

        public const string ControllerName = "embro";

        #endregion

        #region Actions

        [AcceptVerbs(HttpVerbs.Post)]
 public ActionResult Detailsbyid(int id =0)
        {
            MasterViewModel = null;
            return PartialView(ViewName.Details.ToString(), new EmbroDetailsViewModel(_repo.GetEmbroById(id), false, false));

        }
              
        
        [Description("Shows an overview of the latest embro.")]   
        public ActionResult Index([Description("The page number to show.")]int page = 1,[Description("The number of items to show.")]int count =5, string criteria = "", int current = 0)
        {
             var nav = new EmbroNavigationContext{PageSize = count ,PageNumber = page, Criteria =  criteria, CurrentEmbroID = current};
            EmbroDetailsViewModel detailsViewModel;
            detailsViewModel = ( 0 == current) ? _repo.GetEmbroByPageNoSize(page, count, criteria) : new EmbroDetailsViewModel(_repo.GetEmbroById(current),false,false);
            return View(new EmbroNavigationViewModel(detailsViewModel, nav));          
        }

      
        public ActionResult Latest([Description("The number of the page to show.")]int page = 1, int count = 2, string criteria = "")
        {
            Dictionary<string, string> queryValues = new Dictionary<string, string>();
            foreach (string key in Request.QueryString.Keys)
            {
                queryValues[key] = Request.QueryString[key];
            }
            if (queryValues.Keys.Contains("criteria")) criteria = queryValues["criteria"];
            var result = ViewForEmbroPagedByDate(page, count, criteria, EmbroDateType.Published);

            
            // Since this is the site root action and will be shown as the first page after
            // installing the application, show a welcome page if no embro are found.
            var viewResult = result as ViewResult;
            if (viewResult != null && string.Equals(viewResult.ViewName, ViewName.NotFound.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return View(ViewName.Index, this.SiteData);
            }
            else
            {
                return result;
            }
        }


 //[HttpPost]


      

        [Description("Gets info about a single embro by paging info.")]
        public ActionResult DetailsByPage( [Description("The number of the page to show.")]int page = 1, int count = 2, string criteria = "")
        {
            
            return PartialView("Details",_repo.GetEmbroByPageNoSize(page, count, criteria));

        }

        [Description("Gets info about a single embro by paging info.")]

        public ActionResult Details(EmbroNavigationContext mEmbroNavigationContext)
        {
            return DetailsByPage(mEmbroNavigationContext.PageNumber, mEmbroNavigationContext.PageSize,
                           mEmbroNavigationContext.Criteria);

        } 

      
        [Description("Allows the user to upload a new embroidery.")]
        [AuthorizeAdministrator]
        public ActionResult Upload()
        {
            return ViewForUpload<EmbroideryItem>(p => p.Published = DateTimeOffset.UtcNow);
        }

        [Description("Upload a new embroidery.")]
        [AuthorizeAdministrator]
        [AcceptVerbs(HttpVerbs.Post)]      
        [Compress]
        [HttpPost, ValidateInput(false)]       
        public ActionResult Upload([Bind(Exclude = "Id")]EmbroideryItem embro, string tagList, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid || true)
            {
                if (fileUpload != null)
                {       
                    //embro.Guid = Guid.NewGuid();
                    embro.FileExtension = System.IO.Path.GetExtension(fileUpload.FileName);
                    var membershipUser = Membership.GetUser(User.Identity.Name);
                    if (membershipUser != null)
                    {
                        var providerUserKey = membershipUser.ProviderUserKey;
                        if (providerUserKey != null)
                            embro.UserID = (Guid)providerUserKey;
                    }
                    embro.Tags = tagList;
                    _repo.SaveEmbro(fileUpload.InputStream,embro,ThumbnailWidth);
                    //using (Stream embroStream = fileUpload.InputStream)
                    //{
                    //    byte[] bytes = new byte[fileUpload.ContentLength];
                    //    embroStream.Read(bytes, 0, fileUpload.ContentLength);
                    //    embroStream.Position = 0;
                    //    EmbroideryParserFactory factory = new EmbroideryParserFactory(embroStream);
                    //    IGetEmbroideryData embroDatum = factory.CreateParser();
                    //    embro.Data = bytes;  
                    //    var design = embroDatum.Design;
                    //    embro.Summary = design.ToString();
                    //    using (Stream svg = new MemoryStream())
                    //    {
                    //        SvgEncoder svgEncoder = new SvgEncoder(svg, embroDatum.Design);
                    //        svgEncoder.WriteSvg();
                    //        embro.Svg = svgEncoder.ReadSvgString();
                    //    }
                    //    using (Stream png = new MemoryStream())
                    //    {
                    //        StitchToBmp pngEncoder = new StitchToBmp(design.Blocks, ThumbnailWidth);
                    //        pngEncoder.FillStreamWithPng(png);
                    //        embro.Png = Convert.ToBase64String(png.ToByteArray());
                    //    }
                      
                    //    embro.Json = design.ToJsonCoords();                        

                    //if (!embro.Hidden) embro.Published = DateTime.UtcNow;
                       
                    //    _repo.SaveEmbro(embro);
                    //}                    
                   
                }
              
                //var tags = Converter.ToTags(tagList);
                //return PerformSave(ViewName.Upload, embro, r => r.SavePhoto(photo, tags), RedirectToAction(ActionName.Details, new { id = photo.Id }));                
            }
            //return View();
            return RedirectToAction(ActionName.Upload);
        }
         
        
        [Description("Allows the user to download an embroidery file.")]
        [Compress]
        [HttpGet]
        public FileResult Download(int id)
        {
             EmbroideryItem item =  _repo.GetEmbroBinaryDataById(id);
             return File(item.Data, System.Net.Mime.MediaTypeNames.Application.Octet, string.Format(CultureInfo.CurrentCulture,"{0}.{1}",item.Title, item.FileExtension));
        }
      

        [AuthorizeAdministrator]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(EmbroideryItem photo, string tagList, HttpPostedFileBase fileUpload)
        {
            var tags = Converter.ToTags(tagList);

            if (fileUpload != null)
            {
                int len = fileUpload.ContentLength;
                Stream file = fileUpload.InputStream;
                byte[] bytes = new byte[len];
                file.Read(bytes, 0, len);
                //photo.SetData(bytes);

            }
            return RedirectToAction(ActionName.Index);
            //return PerformSave(ViewName.Edit, photo, r => r.SavePhoto(photo, tags), RedirectToAction(ActionName.Details, new { id = photo.Id }));
        }


        [Description("Allows the user to delete a embro.")]
        [AuthorizeAdministrator]
        public ActionResult Delete([Description("The embro id.")]int id)
        {
            return ViewForDelete<EmbroideryItem>("Embro", p => p.Title, r => r.GetEmbroById(id));
        }


        [Description("Returns embroidery stitch coordinates by Json.")]
        public JsonResult JsonEmbroideryCoords(int id)
        {
            EmbroideryItem item = _repo.GetEmbroBinaryDataById(id);

            using (var factory = new EmbroideryParserFactory(item.Data))
            {
                IGetEmbroideryData embroDatum = factory.CreateParser();
                var design = embroDatum.Design;
                var minX = design.GetXCoordMin();
                var minY = design.GetYCoordMin();
                design.Blocks.SelectMany(block => block.AsEnumerable()).ToList().ForEach(coord =>
                    {
                        coord.Y -= minY;
                        coord.X -= minX;
                    });
                var result = from needle in design.Blocks
                             select new {color = needle.color.Name, needle};
                return this.Json(result, "application/json", JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region Helper Methods

        private static string[] GetTagsFromQueryString(string tags)
        {
            if (string.IsNullOrEmpty(tags))
            {
                return new string[0];
            }
            else
            {
                return tags.Split('+');
            }
        }

        private static string GetPageTitleForPhotosPublished(string titleDatePart)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.PhotosPublished, titleDatePart);
        }

        private static string GetPageTitleForPhotosTaken(string titleDatePart)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.PhotosTaken, titleDatePart);
        }

        private static string GetPageTitleForPhotosTagged(string[] tags)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.PhotosTagged, string.Join(" ", tags));
        }

        private static string GetPageTitleForEmbrosTitled(string title)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.PhotosTitled, title);
        }

        private static string GetPageTitleForEmbrosFoundFromSearch(string searchText)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.PhotosFoundFromSearch, searchText);
        }

       


        private ActionResult ViewForEmbroPagedByDate(int page, int count, string criteria,EmbroDateType type)
        {
           
            return ViewForEmbrosPaged(page, count, criteria);
        }




      

        private ActionResult ViewForEmbrosPaged(int page, int count, string navigationContextCriteria)
        {

            //var navigationContext = _repo.GetNavigationContextByCountPage(count, page, navigationContextCriteria);
            var navigationContext = new EmbroNavigationContext(){PageNumber = page,PageSize = count};
            if (navigationContext == null)
            {
                return View(ViewName.NotFound);
            }
            //navigationContext.Embros.ToList().ForEach(e => this.MasterViewModel.AddKeywords(e.TagList));
            //this.MasterViewModel.AddKeywords(embros[0].TagList);               

            // Create the ViewModel and show the View.
            EmbroDetailsViewModel embroDetailsViewModel = this.GetEmbroDetailsViewModel(navigationContext.Current, false, false);

            navigationContext.ShowFilmstrip = true;
            var model = new EmbroNavigationViewModel(embroDetailsViewModel, navigationContext);

            return View(ViewName.Details, model);
        }

        private static void GetBetweenDates(int? year, int? month, int? day, out DateTimeOffset minDate, out DateTimeOffset maxDate, out string titleDatePart)
        {
            if (!year.HasValue)
            {
                minDate = DateTimeOffsetExtensions.MinValue;
                maxDate = DateTimeOffset.MaxValue;
                titleDatePart = null;
            }
            else
            {
                if (!month.HasValue)
                {
                    minDate = new DateTimeOffset(year.Value, 1, 1, 0, 0, 0, TimeSpan.Zero);
                    maxDate = minDate.AddYears(1);
                    titleDatePart = string.Format(CultureInfo.CurrentCulture, Resources.PhotosDateTitleInYear, year.Value);
                }
                else
                {
                    if (!day.HasValue)
                    {
                        minDate = new DateTimeOffset(year.Value, month.Value, 1, 0, 0, 0, TimeSpan.Zero);
                        maxDate = minDate.AddMonths(1);
                        titleDatePart = string.Format(CultureInfo.CurrentCulture, Resources.PhotosDateTitleInMonth, minDate.ToString("y", CultureInfo.CurrentCulture));
                    }
                    else
                    {
                        minDate = new DateTimeOffset(year.Value, month.Value, day.Value, 0, 0, 0, TimeSpan.Zero);
                        maxDate = minDate.AddDays(1);
                        titleDatePart = string.Format(CultureInfo.CurrentCulture, Resources.PhotosDateTitleOnDay, minDate.ToLongDateString());
                    }
                }
            }
            // The actual dates to use need to be adjusted for the configured time zone.
            minDate = minDate.AdjustToUtc();
            maxDate = maxDate.AdjustToUtc();
        }

        #endregion
    }
}