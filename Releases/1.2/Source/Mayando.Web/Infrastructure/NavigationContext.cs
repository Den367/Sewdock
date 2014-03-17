using System.Collections.Generic;
using Mayando.Web.Models;

namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// Defines a navigation context for photos.
    /// </summary>
    public class NavigationContext
    {
        #region Properties

        /// <summary>
        /// Gets all the photos in the navigation context.
        /// </summary>
        public IEnumerable<Photo> Photos { get; private set; }

        /// <summary>
        /// Gets the current photo in the navigation context.
        /// </summary>
        public Photo Current { get; private set; }

        /// <summary>
        /// Gets the type of navigation context.
        /// </summary>
        public NavigationContextType? Type { get; private set; }

        /// <summary>
        /// Gets the criteria for the navigation context.
        /// </summary>
        public string Criteria { get; private set; }

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
        public Photo Previous { get; private set; }

        /// <summary>
        /// Gets all the previous photos in the navigation context.
        /// </summary>
        public IEnumerable<Photo> PreviousPhotos { get; private set; }

        /// <summary>
        /// Gets the next photo in the navigation context.
        /// </summary>
        public Photo Next { get; private set; }

        /// <summary>
        /// Gets all the next photos in the navigation context.
        /// </summary>
        public IEnumerable<Photo> NextPhotos { get; private set; }

        /// <summary>
        /// Gets the delay in seconds to go to the next photo in the slideshow, or <see langword="null"/> when not in a slideshow.
        /// </summary>
        public int? SlideshowDelay { get; private set; }

        /// <summary>
        /// Gets a value that determines if in a slideshow.
        /// </summary>
        public bool IsSlideshow { get; private set; }

        /// <summary>
        /// The default delay for a slideshow.
        /// </summary>
        public int DefaultSlideshowDelay { get; private set; }

        /// <summary>
        /// Gets a value that determines if the filmstrip should be shown in the navigation context.
        /// </summary>
        public bool ShowFilmstrip { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationContext"/> class.
        /// </summary>
        /// <param name="photos">All the photos in the navigation context.</param>
        /// <param name="current">The current photo in the navigation context.</param>
        /// <param name="type">The type of navigation context.</param>
        /// <param name="criteria">The criteria for the navigation context.</param>
        /// <param name="initialSlideshowDirection">The initial direction in which a new slideshow will be started.</param>
        /// <param name="showFilmstrip">Determines if the filmstrip should be shown in the navigation context.</param>
        public NavigationContext(IList<Photo> photos, Photo current, NavigationContextType? type, string criteria, NavigationDirection initialSlideshowDirection, bool showFilmstrip)
            : this(photos, current, type, criteria, null, null, null, initialSlideshowDirection, showFilmstrip)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationContext"/> class.
        /// </summary>
        /// <param name="photos">All the photos in the navigation context.</param>
        /// <param name="current">The current photo in the navigation context.</param>
        /// <param name="type">The type of navigation context.</param>
        /// <param name="criteria">The criteria for the navigation context.</param>
        /// <param name="description">A description of the navigation context.</param>
        /// <param name="overviewUrl">A URL to the overview page of the navigation context.</param>
        /// <param name="slideshowDelay">The delay in seconds to go to the next photo in the slideshow, or <see langword="null"/> when not in a slideshow.</param>
        /// <param name="showFilmstrip">Determines if the filmstrip should be shown in the navigation context.</param>
        public NavigationContext(IList<Photo> photos, Photo current, NavigationContextType? type, string criteria, string description, string overviewUrl, int? slideshowDelay, bool showFilmstrip)
            : this(photos, current, type, criteria, description, overviewUrl, slideshowDelay, NavigationDirection.Forward, showFilmstrip)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationContext"/> class.
        /// </summary>
        /// <param name="photos">All the photos in the navigation context.</param>
        /// <param name="current">The current photo in the navigation context.</param>
        /// <param name="type">The type of navigation context.</param>
        /// <param name="criteria">The criteria for the navigation context.</param>
        /// <param name="description">A description of the navigation context.</param>
        /// <param name="overviewUrl">A URL to the overview page of the navigation context.</param>
        /// <param name="slideshowDelay">The delay in seconds to go to the next photo in the slideshow, or <see langword="null"/> when not in a slideshow.</param>
        /// <param name="initialSlideshowDirection">The initial direction in which a new slideshow will be started.</param>
        /// <param name="showFilmstrip">Determines if the filmstrip should be shown in the navigation context.</param>
        public NavigationContext(IList<Photo> photos, Photo current, NavigationContextType? type, string criteria, string description, string overviewUrl, int? slideshowDelay, NavigationDirection initialSlideshowDirection, bool showFilmstrip)
        {
            if (photos == null)
            {
                photos = new Photo[0];
            }

            this.Photos = photos;
            this.Current = current;
            this.Type = type;
            this.Criteria = criteria;
            this.Description = description;
            this.OverviewUrl = overviewUrl;
            this.SlideshowDelay = slideshowDelay;
            this.IsSlideshow = slideshowDelay.HasValue;
            this.DefaultSlideshowDelay = SiteData.GlobalDefaultSlideshowDelay * (initialSlideshowDirection == NavigationDirection.Forward ? 1 : -1);
            this.ShowFilmstrip = showFilmstrip;

            // Determine the previous and next photos in the context based on the current index.
            this.CurrentIndexInNavigationContext = photos.IndexOf(this.Current);
            var previousPhotos = new List<Photo>();
            var nextPhotos = new List<Photo>();
            for (var i = 0; i < photos.Count; i++)
            {
                if (i < this.CurrentIndexInNavigationContext)
                {
                    previousPhotos.Add(photos[i]);
                    // The last photo before the current is the "previous" one.
                    this.Previous = photos[i];
                }
                if (i > this.CurrentIndexInNavigationContext)
                {
                    nextPhotos.Add(photos[i]);
                    if (this.Next == null)
                    {
                        // The first photo after the current is the "next" one.
                        this.Next = photos[i];
                    }
                }
            }
            this.PreviousPhotos = previousPhotos;
            this.NextPhotos = nextPhotos;
        }

        #endregion
    }
}