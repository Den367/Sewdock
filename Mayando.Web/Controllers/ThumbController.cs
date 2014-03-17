using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Repository;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Controllers
{
    public class ThumbController :   SiteControllerBase
    {
        private readonly IThumbRepository _repo;

        #region Constants

        private const int ThumbnailWidth = 100;
        private const int ThumbnailHeight = 100;

        public const string ControllerName = "thumb";

        #endregion

        public ActionResult Index()
        {
            return GetThumbsPaged(pagingInfo: new EmbroNavigationContext ());
        }

        public ThumbController(IThumbRepository repo)
        {
            _repo = repo;
        }
        
        public ActionResult GetThumbsPaged(EmbroNavigationContext pagingInfo)
        {
            var navigationContext = _repo.GetNavigationContextByCountPage(pagingInfo);
            if (navigationContext != null)
            {                          
if (MasterViewModel != null) navigationContext.Embros.ToList().ForEach(e => this.MasterViewModel.AddKeywords(e.TagList));            

            // Create the ViewModel and show the View.
            EmbroDetailsViewModel embroDetailsViewModel = this.GetEmbroDetailsViewModel(navigationContext.Current, false, false);

            navigationContext.ShowFilmstrip = true;
            var model = new EmbroNavigationViewModel(embroDetailsViewModel, navigationContext);

            return View(ViewName.NavigationContextFilmstrip, model); }
            return View(ViewName.NavigationContextFilmstrip);
        }
    }

   
}
