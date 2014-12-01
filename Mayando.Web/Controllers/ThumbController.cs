using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Myembro.Extensions;
using Myembro.Infrastructure;
using Myembro.Repository;
using Myembro.ViewModels;

namespace Myembro.Controllers
{
    public class ThumbController : SiteControllerBase
    {
        private readonly IThumbRepository _repo;

        #region Constants

        private const int ThumbnailWidth = 100;
        private const int ThumbnailHeight = 100;

        public const string ControllerName = "thumb";

        #endregion

        public ActionResult Index()
        {
            return GetThumbsPaged(pagingInfo: new EmbroNavigationContext(GetUserProviderKey()));
        }

        public ThumbController(IThumbRepository repo)
        {
            _repo = repo;
        }

        //[OutputCache(Duration = 15, VaryByParam = "*")]
        public ActionResult GetThumbsPaged(EmbroNavigationContext pagingInfo)
        {
            var navigationContext = _repo.GetNavigationContextByCountPage(pagingInfo);
            if (navigationContext != null)
            {
                if (MasterViewModel != null) navigationContext.Embros.ToList().ForEach(e => this.MasterViewModel.AddKeywords(e.TagList));

                // Create the ViewModel and show the View.
                if (navigationContext.Current != null)
                {

                }
                EmbroDetailsViewModel embroDetailsViewModel = this.GetEmbroDetailsViewModel(navigationContext.Current, false, false);

                navigationContext.ShowFilmstrip = true;
                var model = new EmbroNavigationViewModel(embroDetailsViewModel, navigationContext);

                return View(ViewName.NavigationContextFilmstrip, model);
            }
            return View(ViewName.NavigationContextFilmstrip);
        }

        public ActionResult GetThumbByID(int embroID)
        {
            var pngWriter = _repo.GetEmbroByID(embroID);
            return new PngImageResult(pngWriter);

        }
    }


}
