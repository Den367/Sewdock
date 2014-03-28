using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JelleDruyts.Web.Mvc.Paging;
using Mayando.Web.Infrastructure;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Models
{
    public interface IEmbroRepository:IDisposable
    {
        //EmbroideryItem GetMostRecentEmbroWithTagsAndComments(EmbroDateType type);
        #region [Saving]

        void SaveEmbro(System.IO.Stream stream, EmbroideryItem embro, int size);
        void SaveEmbro(EmbroideryItem item);
        #endregion [Saving]

        #region [Data Retrieving]

        #region [Embros]
        /// <summary>
        /// Returns information for single embroidery design by id
        /// </summary>
        /// <param name="id"> embroidery design identity <see cref="int"/></param>
        /// <returns>embroidery design information <see cref="EmbroideryItem"/></returns>
        EmbroideryItem GetEmbroById(int id);
        List<EmbroideryItem> GetEmbroByCountPage(int count, int page);
        EmbroideryItem GetEmbroBinaryDataById(int id);
       
        #endregion [Embros]
        #region [Menu]
        IEnumerable<Menu> GetMenu();
        
        #endregion[Menu]
        #region [Comments]
        IList<EmbroideryItem> GetPhotosWithComments(EmbroDateType type, int count, int page);
        #endregion [Comments]

        #endregion [Data Retrieving]

        void CreateMenu(Menu photosMenu);

        void EnsureSettings(SettingsScope settingsScope, ProviderModel.SettingDefinition[] settingDefinition);

        IEnumerable<object> GetLatestCommentsWithPhoto(int numItems, int p);

        IEnumerable<object> GetLatestEmbros(EmbroDateType embroDateType, int numItems);

        Page GetPageById(int id);

        List<Page> GetPages();

        ICollection<Setting> GetSettings(SettingsScope settingsScope);

        //object GetSettings(SettingsScope settingsScope);

        IDictionary<string, string> GetSettingValues(SettingsScope settingsScope);

        IDictionary<string, int> GetTagCounts(int? count);

        void SaveSettingValues(SettingsScope settingsScope, IDictionary<string, string> settings);

        JelleDruyts.Web.Mvc.Paging.IPagedList<Comment> GetCommentsForEmbro(int embroID, int pageNo, int pageSize);

        IList<string> GetExternalPhotoIds();

        void AddComments(ICollection<ProviderModel.PhotoProviders.CommentInfo> iCollection);

        void AddPhotos(ICollection<ProviderModel.PhotoProviders.PhotoInfo> iCollection);



        Comment GetCommentById(int id);
        bool EditComment(Comment comment);
      

        SiteStatistics GetStatistics();

        Infrastructure.LogLevel? GetHighestLogLevel(DateTimeOffset? minTime);

        IEnumerable<Log> GetLogs(int count, int page, Infrastructure.LogLevel? minLevel, string searchText);

        void CreateLog(Log entry);

        int DeleteLogs(int minAge, Infrastructure.LogLevel maxLevel);
        EmbroNavigationViewModel GetEmbroByPageNoSize(EmbroNavigationContext navContext);
        
    
    }
}
