using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using JelleDruyts.Web.Mvc.Paging;
using Mayando.ProviderModel;
using Mayando.ProviderModel.PhotoProviders;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;

namespace Mayando.Web.Models
{
    /// <summary>
    /// Provides access to the Mayando object context which represents the database.
    /// </summary>
    public sealed class MayandoRepository : Repository<MayandoContext>
    {
        #region Fields

        /// <summary>
        ///  The random number generator to use when querying for random entities.
        /// </summary>
        private Random random;

        #endregion

        #region Constructor

        private readonly static string TablePrefix = ConfigurationManager.AppSettings["TablePrefix"];

        /// <summary>
        /// Initializes a new instance of the <see cref="MayandoRepository"/> class.
        /// </summary>
        public MayandoRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MayandoRepository"/> class.
        /// </summary>
        /// <param name="scopeOption">Defines the transaction scope option.</param>
        public MayandoRepository(TransactionScopeOption scopeOption)
            : this((TransactionScopeOption?)scopeOption)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MayandoRepository"/> class.
        /// </summary>
        /// <param name="scopeOption">If a transaction is used, defines the transaction scope option.</param>
        private MayandoRepository(TransactionScopeOption? scopeOption)
            : base(ObjectContextFactory.Create<MayandoContext>("MayandoContext", c => new MayandoContext(c), TablePrefix), scopeOption)
        {
            this.random = new Random();
        }

        #endregion

        #region Photos

        public IEnumerable<Photo> GetPhotos(PhotoDateType orderBy)
        {
            return this.Context.UserVisiblePhotos.OrderByDescending(orderBy).ToList();
        }

        public IPagedList<Photo> GetPhotosWithComments(PhotoDateType orderBy, int count, int page)
        {
            return this.Context.UserVisiblePhotos.Include(EntityReferenceName.Comments.ToString()).OrderByDescending(orderBy).ToPagedList(page, count);
        }

        public IPagedList<Photo> GetHiddenPhotosWithComments(PhotoDateType orderBy, int count, int page)
        {
            return this.Context.Photos.Include(EntityReferenceName.Comments.ToString()).Where(p => p.Hidden).OrderByDescending(orderBy).ToPagedList(page, count);
        }

        public IEnumerable<Photo> GetLatestPhotos(PhotoDateType orderBy, int count)
        {
            return this.Context.UserVisiblePhotos.OrderByDescending(orderBy).Take(count).ToList();
        }

        public IPagedList<Photo> GetPhotosWithCommentsBetween(PhotoDateType type, DateTimeOffset minDate, DateTimeOffset maxDate, int count, int page)
        {
            return this.Context.UserVisiblePhotos.Include(EntityReferenceName.Comments.ToString()).WhereDateBetween(type, minDate, maxDate).OrderByAscending(type).ToPagedList(page, count);
        }

        public Photo GetMostRecentPhotoWithTagsAndComments(PhotoDateType type)
        {
            return this.Context.UserVisiblePhotos.Include(EntityReferenceName.Tags.ToString()).Include(EntityReferenceName.Comments.ToString()).OrderByDescending(p => p.Id).OrderByDescending(type).FirstOrDefault();
        }

        public Photo GetRandomPhotoWithTagsAndComments()
        {
            var totalPhotos = this.Context.UserVisiblePhotos.Count();
            var skip = this.random.Next(0, totalPhotos);
            return this.Context.UserVisiblePhotos.Include(EntityReferenceName.Tags.ToString()).Include(EntityReferenceName.Comments.ToString()).OrderBy(p => p.Id).Skip(skip).FirstOrDefault();
        }

        public Photo GetPhotoById(int photoId)
        {
            return this.Context.UserVisiblePhotos.FirstOrDefault(p => p.Id == photoId);
        }

        public Photo GetPhotoWithTagsAndCommentsById(int photoId)
        {
            return this.Context.UserVisiblePhotos.Include(EntityReferenceName.Tags.ToString()).Include(EntityReferenceName.Comments.ToString()).FirstOrDefault(p => p.Id == photoId);
        }

        public Photo GetPhotoWithTagsById(int photoId)
        {
            return this.Context.UserVisiblePhotos.Include(EntityReferenceName.Tags.ToString()).FirstOrDefault(p => p.Id == photoId);
        }

        public IEnumerable<Photo> GetPhotosOnOrBefore(PhotoDateType type, DateTimeOffset date, int count)
        {
            var photos = this.Context.UserVisiblePhotos.WhereDateOnOrBefore(type, date).OrderByDescending(type).Take(count).ToList();
            photos.Reverse();
            return photos;
        }

        public IEnumerable<Photo> GetPhotosOnOrAfter(PhotoDateType type, DateTimeOffset date, int count)
        {
            return this.Context.UserVisiblePhotos.WhereDateOnOrAfter(type, date).OrderByAscending(type).Take(count).ToList();
        }

        public void AddPhotos(IEnumerable<PhotoInfo> photos)
        {
            var allTags = (from p in photos
                           select p.Tags).SelectMany(c => c.ToList());
            var tagLookup = EnsureTagsCreated(allTags);
            foreach (var photoInfo in photos)
            {
                var photo = new Photo
                {
                    ExternalId = photoInfo.ExternalId,
                    ExternalUrl = photoInfo.WebUrl,
                    DatePublished = photoInfo.DatePublished,
                    DateTaken = photoInfo.DateTaken,
                    Text = photoInfo.Text,
                    Title = photoInfo.Title,
                    UrlLarge = photoInfo.UrlLarge,
                    UrlNormal = photoInfo.UrlNormal,
                    UrlSmall = photoInfo.UrlSmall,
                    UrlThumbnail = photoInfo.UrlThumbnail,
                    UrlThumbnailSquare = photoInfo.UrlThumbnailSquare
                };
                SetPhotoTags(photo, photoInfo.Tags, tagLookup);
                this.Context.AddToPhotos(photo);
            }
        }

        public void AddTagsToPhotos(IEnumerable<int> photoIds, IEnumerable<string> tags)
        {
            var tagLookup = EnsureTagsCreated(tags);
            foreach (var photoId in photoIds)
            {
                var photo = GetPhotoWithTagsById(photoId);
                foreach (var tag in tagLookup.Values)
                {
                    if (!photo.Tags.Contains(tag))
                    {
                        photo.Tags.Add(tag);
                    }
                }
            }
        }

        public void RemoveTagsFromPhotos(IEnumerable<int> photoIds, IEnumerable<string> tags)
        {
            foreach (var photoId in photoIds)
            {
                var photo = GetPhotoWithTagsById(photoId);
                foreach (var tag in tags)
                {
                    // Note that we're doing an explicit string.Equals here
                    // instead of "where t.Name == tag" as we would normally.
                    // The reason is that here we've already pulled the tags
                    // from the database so they're regular strings. The string
                    // comparison would therefore be case sensitive, where if it were
                    // a query on the server, the database would make it case
                    // insensitive by itself. So we have to build in explicit support
                    // for making the lookup case insensitive. See work item #22860.
                    var tagToRemove = (from t in photo.Tags
                                       where string.Equals(t.Name, tag, StringComparison.OrdinalIgnoreCase)
                                       select t).FirstOrDefault();
                    if (tagToRemove != null)
                    {
                        photo.Tags.Remove(tagToRemove);
                    }
                }
            }
            RemoveUnusedTags();
        }

        public void CreatePhoto(Photo photo, IEnumerable<string> tags)
        {
            this.Context.AddToPhotos(photo);
            var tagLookup = EnsureTagsCreated(tags);
            SetPhotoTags(photo, tags, tagLookup);
        }

        public void SavePhoto(Photo photo, IEnumerable<string> tags)
        {
            var originalPhoto = GetPhotoById(photo.Id);
            this.Context.ApplyPropertyChanges(EntitySetName.Photos.ToString(), photo);
            var tagLookup = EnsureTagsCreated(tags);
            SetPhotoTags(originalPhoto, tags, tagLookup);
            RemoveUnusedTags();
        }

        public void DeletePhotos(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                DeletePhoto(id, false);
            }
            RemoveUnusedTags();
        }

        public void DeletePhoto(int id)
        {
            DeletePhoto(id, true);
        }

        private void DeletePhoto(int id, bool removeUnusedTags)
        {
            var photo = GetPhotoById(id);
            this.Context.DeleteObject(photo);
            if (removeUnusedTags)
            {
                RemoveUnusedTags();
            }
        }

        private static void SetPhotoTags(Photo photo, IEnumerable<string> tags, IDictionary<string, Tag> tagLookup)
        {
            // If it's not a new photo, load all its tags first before removing them.
            if (photo.EntityState != EntityState.Added && photo.EntityState != EntityState.Detached)
            {
                photo.Tags.Load();
            }

            // Remove all existing tags and then add the new ones.
            photo.Tags.Clear();
            foreach (var tag in tags)
            {
                var tagName = tag.Trim();
                photo.Tags.Add(tagLookup[tagName.ToUpperInvariant()]);
            }
        }

        public IPagedList<Photo> GetPhotosWithCommentsTitled(PhotoDateType orderBy, string title, int count, int page)
        {
            return (from p in this.Context.UserVisiblePhotos.Include(EntityReferenceName.Comments.ToString())
                    where p.Title.Contains(title)
                    select p).OrderByDescending(orderBy).ToPagedList(page, count);
        }

        public IPagedList<Photo> GetPhotosWithCommentsTagged(PhotoDateType orderBy, ICollection<string> tags, int count, int page)
        {
            if (tags == null || tags.Count == 0)
            {
                return new Photo[0].ToPagedList(page, count);
            }
            else
            {
                var tagsQuery = this.Context.Tags.Where(GenerateTagsPredicate(tags));
                var photos = (from p in this.Context.UserVisiblePhotos.Include(EntityReferenceName.Comments.ToString())
                              where tagsQuery.Count() > 0 && p.Tags.Intersect(tagsQuery).Count() == tagsQuery.Count()
                              select p).OrderByDescending(orderBy).ToList();
                return photos.ToPagedList(page, count);
            }
        }

        public IList<string> GetExternalPhotoIds()
        {
            return (from p in this.Context.UserVisiblePhotos
                    where !string.IsNullOrEmpty(p.ExternalId)
                    select p.ExternalId).ToList();
        }

        public IPagedList<Photo> FindPhotosWithComments(PhotoDateType orderBy, string searchText, int count, int page)
        {
            return (from p in this.Context.UserVisiblePhotos.Include(EntityReferenceName.Comments.ToString())
                    where p.Title.Contains(searchText)
                        || p.Text.Contains(searchText)
                        || p.Comments.Any(c => c.Text.Contains(searchText) || c.AuthorEmail.Contains(searchText) || c.AuthorName.Contains(searchText) || c.AuthorUrl.Contains(searchText))
                        || p.Tags.Any(t => t.Name.Contains(searchText))
                    select p).OrderByDescending(orderBy).ToPagedList(page, count);
        }

        public void HidePhotos(IEnumerable<int> photoIds)
        {
            SetPhotosHidden(photoIds, true);
        }

        public void UnhidePhotos(IEnumerable<int> photoIds)
        {
            SetPhotosHidden(photoIds, false);
        }

        private void SetPhotosHidden(IEnumerable<int> photoIds, bool hidden)
        {
            foreach (var photoId in photoIds)
            {
                var photo = GetPhotoById(photoId);
                photo.Hidden = hidden;
            }
        }

        #endregion

        #region Comments

        public Comment GetCommentById(int commentId)
        {
            return this.Context.Comments.FirstOrDefault(c => c.Id == commentId);
        }

        public Comment GetCommentWithPhotoById(int commentId)
        {
            return this.Context.Comments.Include(EntityReferenceName.Photo.ToString()).FirstOrDefault(c => c.Id == commentId);
        }

        public IPagedList<Comment> GetLatestCommentsWithPhoto(int count, int page)
        {
            return (from c in this.Context.Comments.Include(EntityReferenceName.Photo.ToString())
                    where !c.Photo.Hidden
                    orderby c.DatePublishedUtc descending
                    select c).ToList().ToPagedList(page, count);
        }

        public void AddComment(int photoId, Comment comment)
        {
            var photo = new Photo { Id = photoId };
            this.Context.AttachTo(EntitySetName.Photos, photo);
            comment.Photo = photo;
            this.Context.AddToComments(comment);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public void SaveComment(Comment comment)
        {
            this.Context.AttachAsModified(EntitySetName.Comments, comment);
        }

        public void DeleteComment(int id)
        {
            var comment = GetCommentById(id);
            this.Context.DeleteObject(comment);
        }

        public void AddComments(IEnumerable<CommentInfo> comments)
        {
            // Save the changes first so any photos that were added before are now persisted.
            this.Context.SaveChanges();
            foreach (var commentInfo in comments)
            {
                var photo = this.Context.UserVisiblePhotos.FirstOrDefault(p => p.ExternalId == commentInfo.ExternalPhotoId);
                if (photo != null)
                {
                    var comment = new Comment
                    {
                        Text = commentInfo.Text,
                        DatePublished = commentInfo.DatePublished,
                        ExternalId = commentInfo.ExternalId,
                        AuthorIsOwner = commentInfo.AuthorIsOwner,
                        AuthorName = commentInfo.AuthorName,
                        AuthorEmail = commentInfo.AuthorEmail,
                        AuthorUrl = commentInfo.AuthorUrl,
                        Photo = photo
                    };
                    this.Context.AddToComments(comment);
                }
            }
        }

        public IList<string> GetExternalCommentIds()
        {
            return (from c in this.Context.Comments
                    where !string.IsNullOrEmpty(c.ExternalId)
                    select c.ExternalId).ToList();
        }

        #endregion

        #region Pages

        public IEnumerable<Page> GetPages()
        {
            return this.Context.Pages.ToList();
        }

        public Page GetPageById(int id)
        {
            return this.Context.Pages.FirstOrDefault(p => p.Id == id);
        }

        public Page GetPageBySlug(string slug)
        {
            return this.Context.Pages.FirstOrDefault(p => p.Slug == slug);
        }

        public Page GetPageWithPhotoById(int id)
        {
            return this.Context.Pages.Include(EntityReferenceName.Photo.ToString()).FirstOrDefault(p => p.Id == id);
        }

        public Page GetPageWithPhotoAndTagsAndCommentsById(int id)
        {
            return this.Context.Pages.Include(EntityReferenceName.Photo.ToString() + "." + EntityReferenceName.Tags.ToString()).Include(EntityReferenceName.Photo.ToString() + "." + EntityReferenceName.Comments.ToString()).FirstOrDefault(p => p.Id == id);
        }

        public Page GetPageWithPhotoAndTagsAndCommentsBySlug(string slug)
        {
            return this.Context.Pages.Include(EntityReferenceName.Photo.ToString() + "." + EntityReferenceName.Tags.ToString()).Include(EntityReferenceName.Photo.ToString() + "." + EntityReferenceName.Comments.ToString()).FirstOrDefault(p => p.Slug == slug);
        }

        public void CreatePage(Page page, int? photoId)
        {
            this.Context.AddToPages(page);
            SetPagePhoto(page, photoId);
        }

        public void SavePage(Page page, int? photoId)
        {
            var originalPage = GetPageWithPhotoById(page.Id);
            this.Context.ApplyPropertyChanges(EntitySetName.Pages.ToString(), page);
            SetPagePhoto(originalPage, photoId);
        }

        private void SetPagePhoto(Page page, int? photoId)
        {
            if (photoId == null)
            {
                page.Photo = null;
            }
            else
            {
                var photo = GetPhotoById(photoId.Value);
                page.Photo = photo;
            }
        }

        public void DeletePage(int id)
        {
            var page = new Page { Id = id };
            this.Context.AttachTo(EntitySetName.Pages, page);
            this.Context.DeleteObject(page);
        }

        #endregion

        #region Galleries

        public IEnumerable<Gallery> GetGalleries()
        {
            return (from g in this.Context.Galleries
                    orderby g.Sequence
                    select g).ToList();
        }

        public IEnumerable<Gallery> GetTopLevelGalleriesWithTagsAndCoverPhotoAndChildGalleriesWithTheirTags()
        {
            return (from g in this.Context.Galleries.Include(EntityReferenceName.CoverPhoto.ToString()).Include(EntityReferenceName.Tags.ToString()).Include(EntityReferenceName.ChildGalleries.ToString() + "." + EntityReferenceName.Tags.ToString())
                    where g.ParentGallery == null
                    orderby g.Sequence
                    select g).ToList();
        }

        public IEnumerable<Gallery> GetGalleriesWithTagsAndCoverPhoto()
        {
            return (from g in this.Context.Galleries.Include(EntityReferenceName.CoverPhoto.ToString()).Include(EntityReferenceName.Tags.ToString())
                    orderby g.Sequence
                    select g).ToList();
        }

        public IEnumerable<Gallery> GetGalleriesWithTagsAndCoverPhotoAndChildGalleriesWithTheirTags()
        {
            return (from g in this.Context.Galleries.Include(EntityReferenceName.CoverPhoto.ToString()).Include(EntityReferenceName.Tags.ToString()).Include(EntityReferenceName.ChildGalleries.ToString() + "." + EntityReferenceName.Tags.ToString())
                    orderby g.Sequence
                    select g).ToList();
        }

        public Gallery GetGalleryById(int id)
        {
            return this.Context.Galleries.FirstOrDefault(g => g.Id == id);
        }

        public Gallery GetGalleryWithParentAndChildGalleriesById(int id)
        {
            return this.Context.Galleries.Include(EntityReferenceName.ParentGallery.ToString()).Include(EntityReferenceName.ChildGalleries.ToString()).FirstOrDefault(g => g.Id == id);
        }

        public Gallery GetGalleryBySlug(string slug)
        {
            return this.Context.Galleries.FirstOrDefault(g => g.Slug == slug);
        }

        public Gallery GetGalleryWithTagsAndCoverPhotoById(int id)
        {
            return this.Context.Galleries.Include(EntityReferenceName.CoverPhoto.ToString()).Include(EntityReferenceName.Tags.ToString()).FirstOrDefault(g => g.Id == id);
        }

        public Gallery GetGalleryWithTagsAndCoverPhotoAndParentGalleryById(int id)
        {
            return this.Context.Galleries.Include(EntityReferenceName.CoverPhoto.ToString()).Include(EntityReferenceName.Tags.ToString()).Include(EntityReferenceName.ParentGallery.ToString()).FirstOrDefault(g => g.Id == id);
        }

        public Gallery GetGalleryWithCoverPhotoByIdAndLoadAllGalleriesWithTags(int id)
        {
            // Force loading all galleries with their tags for this scenario.
            this.Context.Galleries.Include(EntityReferenceName.Tags.ToString()).ToList();
            return this.Context.Galleries.Include(EntityReferenceName.CoverPhoto.ToString()).FirstOrDefault(g => g.Id == id);
        }

        public Gallery GetGalleryWithTagsAndCoverPhotoBySlug(string slug)
        {
            return this.Context.Galleries.Include(EntityReferenceName.CoverPhoto.ToString()).Include(EntityReferenceName.Tags.ToString()).FirstOrDefault(g => g.Slug == slug);
        }

        public Gallery GetGalleryWithCoverPhotoBySlugAndLoadAllGalleriesWithTags(string slug)
        {
            // Force loading all galleries with their tags for this scenario.
            this.Context.Galleries.Include(EntityReferenceName.Tags.ToString()).ToList();
            return this.Context.Galleries.Include(EntityReferenceName.CoverPhoto.ToString()).FirstOrDefault(g => g.Slug == slug);
        }

        public void CreateGallery(Gallery gallery, int? coverPhotoId, IEnumerable<string> tags)
        {
            CreateGallery(gallery, coverPhotoId, tags, null);
        }

        public void CreateGallery(Gallery gallery, int? coverPhotoId, IEnumerable<string> tags, int? parentGalleryId)
        {
            this.Context.AddToGalleries(gallery);
            SetGalleryCoverPhoto(gallery, coverPhotoId);
            SetParentGallery(gallery, parentGalleryId);
            var tagLookup = EnsureTagsCreated(tags);
            SetGalleryTags(gallery, tags, tagLookup);
        }

        public void SaveGallery(Gallery gallery, int? coverPhotoId, IEnumerable<string> tags)
        {
            SaveGallery(gallery, coverPhotoId, tags, null);
        }

        public void SaveGallery(Gallery gallery, int? coverPhotoId, IEnumerable<string> tags, int? parentGalleryId)
        {
            var originalGallery = GetGalleryWithTagsAndCoverPhotoAndParentGalleryById(gallery.Id);
            this.Context.ApplyPropertyChanges(EntitySetName.Galleries.ToString(), gallery);
            SetGalleryCoverPhoto(originalGallery, coverPhotoId);
            SetParentGallery(originalGallery, parentGalleryId);
            var tagLookup = EnsureTagsCreated(tags);
            SetGalleryTags(originalGallery, tags, tagLookup);
            RemoveUnusedTags();
        }

        private void SetGalleryCoverPhoto(Gallery gallery, int? coverPhotoId)
        {
            if (coverPhotoId.HasValue)
            {
                gallery.CoverPhoto = GetPhotoById(coverPhotoId.Value);
            }
            else
            {
                gallery.CoverPhoto = null;
            }
        }

        private void SetParentGallery(Gallery gallery, int? parentGalleryId)
        {
            if (parentGalleryId.HasValue)
            {
                gallery.ParentGallery = GetGalleryById(parentGalleryId.Value);
            }
            else
            {
                gallery.ParentGallery = null;
            }
        }

        public void DeleteGallery(int id)
        {
            var gallery = GetGalleryWithParentAndChildGalleriesById(id);

            // Move all child galleries to this gallery's parent gallery.
            foreach (var childGallery in gallery.ChildGalleries.ToArray())
            {
                childGallery.ParentGallery = gallery.ParentGallery;
            }

            this.Context.DeleteObject(gallery);
        }

        private static void SetGalleryTags(Gallery gallery, IEnumerable<string> tags, IDictionary<string, Tag> tagLookup)
        {
            // If it's not a new gallery, load all its tags first before removing them.
            if (gallery.EntityState != EntityState.Added && gallery.EntityState != EntityState.Detached)
            {
                gallery.Tags.Load();
            }

            // Remove all existing tags and then add the new ones.
            gallery.Tags.Clear();
            foreach (var tag in tags)
            {
                var tagName = tag.Trim();
                gallery.Tags.Add(tagLookup[tagName.ToUpperInvariant()]);
            }
        }

        public int GetMaxGallerySequence()
        {
            return this.Context.Galleries.Max<Gallery, int?>(g => g.Sequence) ?? 0;
        }

        #endregion

        #region Menus

        public IEnumerable<Menu> GetMenus()
        {
            return (from m in this.Context.Menus
                    orderby m.Sequence
                    select m).ToList();
        }

        public Menu GetMenuById(int id)
        {
            return this.Context.Menus.FirstOrDefault(m => m.Id == id);
        }

        public void CreateMenu(Menu menu)
        {
            this.Context.AddToMenus(menu);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public void SaveMenu(Menu menu)
        {
            this.Context.AttachAsModified(EntitySetName.Menus, menu);
        }

        public void DeleteMenu(int id)
        {
            var menu = new Menu { Id = id };
            this.Context.AttachTo(EntitySetName.Menus, menu);
            this.Context.DeleteObject(menu);
        }

        public int GetMaxMenuSequence()
        {
            return this.Context.Menus.Max<Menu, int?>(m => m.Sequence) ?? 0;
        }

        #endregion

        #region Settings

        public ICollection<Setting> GetSettings()
        {
            return (from s in this.Context.Settings
                    orderby s.Sequence
                    select s).ToList();
        }

        public ICollection<Setting> GetSettings(SettingsScope scope)
        {
            var dbScope = scope.ToString();
            return (from s in this.Context.Settings
                    where s.Scope == dbScope
                    orderby s.Sequence
                    select s).ToList();
        }

        public Setting GetSetting(SettingsScope scope, string name)
        {
            var dbScope = scope.ToString();
            return (from s in this.Context.Settings
                    where s.Scope == dbScope && s.Name == name
                    select s).FirstOrDefault();
        }

        public IDictionary<string, string> GetSettingValues(SettingsScope scope)
        {
            var settings = GetSettings(scope);
            return settings.ToDictionary(e => e.Name, e => e.Value);
        }

        public void SaveSettingValues(SettingsScope scope, IDictionary<string, string> settings)
        {
            var dbScope = scope.ToString();
            foreach (var setting in settings)
            {
                var dbSetting = this.Context.Settings.FirstOrDefault(s => s.Name == setting.Key && s.Scope == dbScope);
                if (dbSetting != null && dbSetting.EntityState != EntityState.Deleted && dbSetting.EntityState != EntityState.Detached)
                {
                    dbSetting.Value = setting.Value;
                }
            }
        }

        public void AddSetting(Setting setting)
        {
            this.Context.AddToSettings(setting);
        }

        public void DeleteSetting(SettingsScope scope, string name)
        {
            var setting = GetSetting(scope, name);
            if (setting != null)
            {
                this.Context.DeleteObject(setting);
            }
        }

        public void EnsureSettings(SettingsScope scope, SettingDefinition[] settings)
        {
            var dbScope = scope.ToString();
            var existingSettings = GetSettings(scope);
            foreach (var existingSetting in existingSettings)
            {
                var definedSetting = settings.FirstOrDefault(s => s.Name == existingSetting.Name);
                if (definedSetting == null)
                {
                    // It's in the database but not in the list of defined settings, remove it from the database.
                    this.Context.DeleteObject(existingSetting);
                }
            }

            foreach (var definedSetting in settings)
            {
                var existingSetting = existingSettings.FirstOrDefault(s => s.Name == definedSetting.Name);
                if (existingSetting == null)
                {
                    // It's in not the database but in the list of defined settings, add it to the database.
                    existingSetting = new Setting();
                    existingSetting.Name = definedSetting.Name;
                    existingSetting.Type = definedSetting.Type.ToString();
                    existingSetting.Scope = dbScope;
                    existingSetting.Value = definedSetting.InitialValue;
                    this.Context.AddToSettings(existingSetting);
                }

                // At this point, the setting is in the database (or will be added to it soon).
                // Update all its editable properties.
                existingSetting.Description = definedSetting.Description;
                existingSetting.Sequence = definedSetting.Sequence;
                existingSetting.Title = definedSetting.Title;
                existingSetting.UserVisible = definedSetting.UserVisible;
            }
        }

        #endregion

        #region Tags

        public IDictionary<Tag, int> GetTagCounts(int? numberOfTags)
        {
            var tagsQuery = from t in this.Context.Tags.Include(EntityReferenceName.Photos.ToString())
                            orderby t.Photos.Count descending
                            select new { Tag = t, Count = t.Photos.Count };
            if (numberOfTags.HasValue && numberOfTags.Value > 0)
            {
                tagsQuery = tagsQuery.Take(numberOfTags.Value);
            }
            return tagsQuery.ToDictionary(t => t.Tag, t => t.Count);
        }

        private IDictionary<string, Tag> EnsureTagsCreated(IEnumerable<string> tags)
        {
            var tagDictionary = new Dictionary<string, Tag>();
            var distinctTags = (from t in tags
                                select t.Trim()).Distinct(StringComparer.OrdinalIgnoreCase);
            foreach (var tag in distinctTags)
            {
                var existingTag = (from t in this.Context.Tags
                                   where t.Name == tag
                                   select t).FirstOrDefault();
                if (existingTag != null)
                {
                    // There is a matching tag in the database, use that one.
                    tagDictionary[tag.ToUpperInvariant()] = existingTag;
                }
                else
                {
                    // There is no matching tag in the database, create a new one.
                    var newTag = new Tag { Name = tag };
                    this.Context.AddToTags(newTag);
                    tagDictionary[tag.ToUpperInvariant()] = newTag;
                }
            }
            return tagDictionary;
        }

        private static Expression<Func<Tag, bool>> GenerateTagsPredicate(IEnumerable<string> tags)
        {
            var t = Expression.Parameter(typeof(Tag), "t");
            var body = tags
                .Select(name => Expression.Equal(Expression.Property(t, "Name"), Expression.Constant(name))) // t.Name == name
                .Aggregate((accum, clause) => Expression.Or(accum, clause)); // t.Name == name1 OR t.Name == name2 ...
            return Expression.Lambda<Func<Tag, bool>>(body, t);
        }

        private void RemoveUnusedTags()
        {
            // Save changes first to make sure all tags are persisted.
            this.Context.SaveChanges();
            var unusedTags = from t in this.Context.Tags.Include(EntityReferenceName.Photos.ToString()).Include(EntityReferenceName.Galleries.ToString())
                             where t.Photos.Count == 0 && t.Galleries.Count == 0
                             select t;
            foreach (var unusedTag in unusedTags)
            {
                this.Context.DeleteObject(unusedTag);
            }
        }

        #endregion

        #region Logs

        public void CreateLog(Log entry)
        {
            this.Context.AddToLogs(entry);
        }

        public int DeleteLogs(int minAge)
        {
            return DeleteLogs(minAge, LogLevel.Error);
        }

        public int DeleteLogs(int minAge, LogLevel maxLevel)
        {
            var minDate = DateTime.UtcNow.AddDays(-minAge);
            var maxLevelValue = (int)maxLevel;
            var count = 0;
            foreach (var log in this.Context.Logs.Where(l => l.TimeUtc <= minDate && l.Level <= maxLevelValue))
            {
                this.Context.DeleteObject(log);
                count++;
            }
            return count;
        }

        public IPagedList<Log> GetLogs(int count, int page)
        {
            return GetLogs(count, page, null, null);
        }

        public IPagedList<Log> GetLogs(int count, int page, LogLevel? minLevel, string searchText)
        {
            IQueryable<Log> logs = this.Context.Logs;
            if (minLevel.HasValue)
            {
                var minLevelValue = (int)minLevel.Value;
                logs = logs.Where(l => l.Level >= minLevelValue);
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                logs = logs.Where(l => l.Message.Contains(searchText) || l.Detail.Contains(searchText));
            }
            return logs.OrderByDescending(l => l.TimeUtc).ToList().ToPagedList(page, count);
        }

        public LogLevel? GetHighestLogLevel(DateTimeOffset? minTime)
        {
            IQueryable<Log> logs = this.Context.Logs;
            if (minTime.HasValue)
            {
                var minDatabaseTime = minTime.Value.UtcDateTime;
                logs = logs.Where(l => l.TimeUtc >= minDatabaseTime);
            }
            if (logs.Count() == 0)
            {
                return null;
            }
            else
            {
                return (LogLevel)(from l in logs select l.Level).Max();
            }
        }

        #endregion

        #region Statistics

        public SiteStatistics GetStatistics()
        {
            return new SiteStatistics(this.Context.UserVisiblePhotos.Count(), this.Context.Photos.Where(p => p.Hidden).Count(), this.Context.Galleries.Count(), this.Context.Pages.Count(), this.Context.Comments.Count(), this.Context.Tags.Count());
        }

        #endregion
    }
}