using System;
using System.Collections.Generic;
using System.Web.Security;
using Mayando.Web.Models;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// Defines a navigation context for photos.
    /// </summary>
    public class EmbroNavigationContext
    {
        #region Properties

        /// <summary>
        /// Gets all the photos in the navigation context.
        /// </summary>
        public IEnumerable<EmbroThumbnailViewModel> Embros { get; set; }

        /// <summary>
        /// Gets the current photo in the navigation context.
        /// </summary>
        public EmbroideryItem Current { get; private set; }

        /// <summary>
        /// Gets the type of navigation context.
        /// </summary>
        public NavigationContextType? Type { get; private set; }

        /// <summary>
        /// Gets the criteria for the navigation context.
        /// </summary>
        public string Criteria { get;  set; }

        /// <summary>
        /// Gets a description of the navigation context.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets a URL to the overview page of the navigation context.
        /// </summary>
        public string OverviewUrl { get; private set; }

        /// <summary>
        /// Gets the index of the current photo in the navigation context.
        /// </summary>
        public int CurrentIndexInNavigationContext { get; private set; }

        /// <summary>
        /// Gets the previous photo in the navigation context.
        /// </summary>
        public EmbroideryItem Previous { get; private set; }

        ///// <summary>
        ///// Gets all the previous photos in the navigation context.
        ///// </summary>
        //public IEnumerable<EmbroideryItem> PreviousEmbros { get; private set; }

        /// <summary>
        /// Gets the next photo in the navigation context.
        /// </summary>
        public EmbroideryItem Next { get; private set; }

        ///// <summary>
        ///// Gets all the next photos in the navigation context.
        ///// </summary>
        //public IEnumerable<EmbroideryItem> NextEmbros { get; private set; }

        ///// <summary>
        ///// Gets the delay in seconds to go to the next photo in the slideshow, or <see langword="null"/> when not in a slideshow.
        ///// </summary>
        //public int? SlideshowDelay { get; private set; }

        ///// <summary>
        ///// Gets a value that determines if in a slideshow.
        ///// </summary>
        //public bool IsSlideshow { get; private set; }

        /// <summary>
        /// The default delay for a slideshow.
        /// </summary>
        public int DefaultSlideshowDelay { get; private set; }

        /// <summary>
        /// Gets a value that determines if the filmstrip should be shown in the navigation context.
        /// </summary>
        public bool ShowFilmstrip { get;  set; }

        public int PageSize {get;set;}
        public int PageNumber {get;set;}
        public int TotalItemCount { get; set; }
        /// <summary>
        /// Current id of embroidery for details
        /// </summary>
        public int CurrentEmbroID { get; set; }

        public Guid UserID { get; set; }

        #endregion

        #region Constructors

     



        /// <summary>
        /// Initializes a new instance of the <see cref="EmbroNavigationContext"/> class.
        /// </summary>
        /// <param name="embros">All the photos in the navigation context.</param>
        /// <param name="current">The current photo in the navigation context.</param>
        /// <param name="type">The type of navigation context.</param>
        /// <param name="criteria">The criteria for the navigation context.</param>
      
        public EmbroNavigationContext(IList<EmbroThumbnailViewModel> embros, EmbroideryItem current, NavigationContextType? type, string criteria)
        {
            if (embros == null)
            {
                embros = new EmbroThumbnailViewModel[0];
            }

            this.Embros = embros;
            this.Current = current;
            this.Type = type;
            this.Criteria = criteria;
           

          
          
        }

        public EmbroNavigationContext()
        {
            PageSize = 7;
            PageNumber = 1;
            var user = Membership.GetUser();
            if (null != user )
            UserID = (Guid) user.ProviderUserKey;
        }

        #endregion
    }
}