using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Mayando.Web.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using JelleDruyts.Web.Mvc.Paging;
using Mayando.Web.DataAccess;
using System.Xml.Linq;
using Mayando.Web.Repository;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// Class provides intermeadiate interaction with DataBase layer
    /// </summary>
     [ CLSCompliant(false)]
    public class EmbroRepository : RepositoryBase,IEmbroRepository
    {
        private readonly EmbroCommandFormer _commands;
        public EmbroRepository ()
        {
            manager = factory.Manager;
            _commands = new EmbroCommandFormer(manager.Connection);
        }

      
        public void SaveEmbro(EmbroideryItem item)
        {
            tableFormer.EmbroTable.Rows.Add(new object[]
                {

                    item.Data,
                    item.Svg,
                    item.Title,
                    item.Article,
                    item.Created,
                    item.Published,
                    item.Hidden,
                    item.Tags,
                    item.Html,
                    item.Summary,
                    item.UserID,
                    item.TypeID,
                    item.Json,
                    item.Png
                });
           manager.ImportTable(tableFormer.EmbroTable, "emb.Embro");
        }

        public EmbroideryItem GetEmbroById(int id)
        {
            using (SqlDataReader reader = _commands.GetReadEmbroByIdCommand(id).ExecuteReader(CommandBehavior.SingleRow))
            {
                if (reader.Read())
               
                    return ReadEmbro(reader, false);
               
            }
            return null;
        }

     

        public EmbroDetailsViewModel GetEmbroByPageNoSize(int page, int size, string criteria)     
        {            
            using (SqlDataReader reader = _commands.GetReadEmbroByPageNoSize(page,size,criteria).ExecuteReader(CommandBehavior.SingleRow))
            {
                if (reader.Read())
                    return  new  EmbroDetailsViewModel(ReadEmbro(reader, false), false,false);
            }
            return null;
        }

        public EmbroideryItem GetEmbroBinaryDataById(int id)
        {
            using (SqlDataReader reader = _commands.GetReadEmbroBinaryDataByIdCommand(id).ExecuteReader(System.Data.CommandBehavior.SingleRow))
            {
                if (!reader.Read())
                {
                    return null;
                }
              

                    return ReadEmbroBinaryData(reader);
                
            }
            //return null;
        }

        public List<EmbroideryItem> GetEmbroByCountPage(int count, int page)
        {
            List<EmbroideryItem> embros = new List<EmbroideryItem>();

            using (SqlDataReader reader = _commands.GetReadEmbroByCountPageCommand(count, page, null).ExecuteReader())
            {
                if (!reader.Read()) return null;
                do
                {
                    embros.Add(ReadEmbro(reader, false));

                } while (reader.Read());

                return embros;

            }
        }

    
        #region[Menu]

        public IEnumerable<Menu> GetMenu()
        {
            IList<Menu> menus = new List<Menu>();
            Menu menu;
            using (
                SqlDataReader reader =
                    factory.Commands.GetReadMenuCommand().ExecuteReader(System.Data.CommandBehavior.CloseConnection))
            {
                if (!reader.Read())
                {
                    return null;
                }
                do
                {
                    menu = new Menu();
                    menu.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    menu.Sequence = reader.GetInt32(reader.GetOrdinal("Sequence"));
                    menu.OpenInNewWindow = reader.GetBoolean(reader.GetOrdinal("OpenInNewWindow"));
                    menu.Title = reader.GetString(reader.GetOrdinal("Title"));
                    menu.Url = reader.GetString(reader.GetOrdinal("Url"));
                    if (!IsDBNull(reader, "ToolTip")) menu.ToolTip = reader.GetString(reader.GetOrdinal("ToolTip"));
                    menus.Add(menu);
                } while (reader.Read());
            }
            return menus; //.AsEnumerable<Menu>();
        }

        #endregion [Menu]



        #region [Settings]

        public ICollection<Setting> GetSettings(SettingsScope scope)
        {

            ICollection<Setting> settings = new List<Setting>();
            Setting setting;
            using (
                SqlDataReader reader =
                    factory.Commands.GetReadSettingCommand(scope.ToString())
                           .ExecuteReader(System.Data.CommandBehavior.CloseConnection))
            {
                if (!reader.Read())
                {
                    return null;
                }
                do
                {
                    setting = new Setting();
                    if (!IsDBNull(reader, "Scope")) setting.Scope = reader.GetString(reader.GetOrdinal("Scope"));

                    if (!IsDBNull(reader, "Type")) setting.Type = reader.GetString(reader.GetOrdinal("Type"));
                    if (!IsDBNull(reader, "UserVisible"))
                        setting.UserVisible = reader.GetBoolean(reader.GetOrdinal("UserVisible"));
                    if (!IsDBNull(reader, "Sequence"))
                        setting.Sequence = reader.GetInt32(reader.GetOrdinal("Sequence"));

                    if (!IsDBNull(reader, "Title")) setting.Title = reader.GetString(reader.GetOrdinal("Title"));
                    if (!IsDBNull(reader, "Description"))
                        setting.Description = reader.GetString(reader.GetOrdinal("Description"));
                    if (!IsDBNull(reader, "Name")) setting.Name = reader.GetString(reader.GetOrdinal("Name"));
                    if (!IsDBNull(reader, "Value")) setting.Value = reader.GetString(reader.GetOrdinal("Value"));
                    settings.Add(setting);
                } while (reader.Read());
            }
            return settings; //.AsEnumerable<Menu>();


        }

        #endregion [Settings]

     

       


        public IList<EmbroideryItem> GetPhotosWithComments(EmbroDateType orderBy, int count, int page)
        {

            IList<EmbroideryItem> list = null; // = new IPagedList<EmbroideryItem>();
            //list.Add(GetEmbroById(1));
            return list;
            //return this.Context.UserVisiblePhotos.Include(EntityReferenceName.Comments.ToString()).OrderByDescending(orderBy).ToPagedList(page, count);
        }


        public void CreateMenu(Menu photosMenu)
        {
            return;
        }

        public void EnsureSettings(SettingsScope settingsScope, ProviderModel.SettingDefinition[] settingDefinition)
        {
            return;
        }

        public IEnumerable<object> GetLatestCommentsWithPhoto(int numItems, int p)
        {
            return null;
        }

        public IEnumerable<object> GetLatestEmbros(EmbroDateType embroDateType, int numItems)
        {
            return null;
        }

        public Page GetPageById(int id)
        {
            return null;
        }

        public List<Page> GetPages()
        {
            return null;
        }





        public IDictionary<string, string> GetSettingValues(SettingsScope settingsScope)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            using (
                SqlDataReader reader =
                   factory.Commands.GetReadSettingCommand(settingsScope.ToString())
                           .ExecuteReader(System.Data.CommandBehavior.CloseConnection))
            {
                if (!reader.Read())
                {
                    return null;
                }
                do
                {

                    if (!IsDBNull(reader, "Value"))
                        settings.Add(reader.GetString(reader.GetOrdinal("Name")),
                                     reader.GetString(reader.GetOrdinal("Value")));

                } while (reader.Read());
            }
            return settings;
        }

        public IDictionary<string, int> GetTagCounts(int? count)
        {
            return null;
        }

        public void SaveSettingValues(SettingsScope settingsScope, IDictionary<string, string> settings)
        {
            return;
        }

     

        public IList<string> GetExternalCommentIds()
        {
            return null;
        }

        public IList<string> GetExternalPhotoIds()
        {
            return null;
        }

        public void AddComments(ICollection<ProviderModel.PhotoProviders.CommentInfo> iCollection)
        {
            return;
        }

        public void AddPhotos(ICollection<ProviderModel.PhotoProviders.PhotoInfo> iCollection)
        {
            return;
        }



        public bool AddComment(Comment comment)
        {
            return true;
        }



        #region [SqlDataReader Helper]

        private EmbroideryItem ReadEmbro(SqlDataReader reader, bool withData)
        {
            var embro = new EmbroideryItem();

            if (!IsDBNull(reader, "Id")) embro.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            //if (withData && (reader.GetValue(reader.GetOrdinal("Data")) != DBNull.Value))
            //  embro.Data = (byte[])reader[reader.GetOrdinal("Data")];

            if (reader.GetValue(reader.GetOrdinal("Svg")) != DBNull.Value)
                embro.Svg = reader.GetString(reader.GetOrdinal("Svg"));
            //if (reader.GetValue(reader.GetOrdinal("Png")) != DBNull.Value)
            //    embro.Png = reader.GetString(reader.GetOrdinal("Png"));
            if (reader.GetValue(reader.GetOrdinal("Json")) != DBNull.Value)
                embro.Json = reader.GetString(reader.GetOrdinal("Json"));

            if (reader.GetValue(reader.GetOrdinal("Title")) != DBNull.Value)
                embro.Title = reader.GetString(reader.GetOrdinal("Title"));
            if (reader.GetValue(reader.GetOrdinal("Article")) != DBNull.Value)
                embro.Article = reader.GetString(reader.GetOrdinal("Article"));

            embro.Created = reader.GetDateTimeOffset(reader.GetOrdinal("Created"));
            if (!IsDBNull(reader, "Published"))
                embro.Published = reader.GetDateTimeOffset(reader.GetOrdinal("Published"));
            embro.Hidden = reader.GetBoolean(reader.GetOrdinal("Hidden"));
            embro.XmlTags = reader.GetString(reader.GetOrdinal("Tags"));
            if (!IsDBNull(reader, "Html")) embro.Html = reader.GetString(reader.GetOrdinal("Html"));
            embro.Summary = reader.GetString(reader.GetOrdinal("Summary"));
            if (!IsDBNull(reader, "TypeID")) embro.TypeID = reader.GetInt32(reader.GetOrdinal("TypeID"));
            if (!IsDBNull(reader, "UserID")) embro.UserID = reader.GetGuid(reader.GetOrdinal("UserID"));
            if (!IsDBNull(reader, "Downloads")) embro.DownloadsCount = reader.GetInt64(reader.GetOrdinal("Downloads"));
            return embro;
        }

     


        private EmbroideryItem ReadEmbroBinaryData(SqlDataReader reader)
        {
            var embro = new EmbroideryItem();

           
            if ( (reader.GetValue(reader.GetOrdinal("Data")) != DBNull.Value))
                embro.Data = (byte[])reader[reader.GetOrdinal("Data")];
           
            if (!IsDBNull(reader, "TypeID")) embro.TypeID = reader.GetInt32(reader.GetOrdinal("TypeID"));           
            return embro;
        }

     

    #endregion [DataReader Helper]

       
      

    



        public Comment GetCommentById(int id)
        {
            throw new NotImplementedException();
        }

        public SiteStatistics GetStatistics()
        {
            throw new NotImplementedException();
        }

        public LogLevel? GetHighestLogLevel(DateTimeOffset? minTime)
        {
            return LogLevel.Error;
        }

        public IEnumerable<Log> GetLogs(int count, int page, LogLevel? minLevel, string searchText)
        {
            return new  List<Log>(); //throw new NotImplementedException();
        }

        public void CreateLog(Log entry)
        {
            return;
            //throw new NotImplementedException();
        }

        public int DeleteLogs(int minAge, LogLevel maxLevel)
        {
            throw new NotImplementedException();
        }

        void IEmbroRepository.SaveEmbro(EmbroideryItem item)
        {
            this.SaveEmbro(item);
        }

       
        List<EmbroideryItem> IEmbroRepository.GetEmbroByCountPage(int count, int page)
        {
            throw new NotImplementedException();
        }

            

        IEnumerable<Menu> IEmbroRepository.GetMenu()
        {
            return GetMenu();
        }

        IList<EmbroideryItem> IEmbroRepository.GetPhotosWithComments(EmbroDateType type, int count, int page)
        {
            throw new NotImplementedException();
        }

        void IEmbroRepository.CreateMenu(Menu photosMenu)
        {
            throw new NotImplementedException();
        }

        void IEmbroRepository.EnsureSettings(SettingsScope settingsScope, ProviderModel.SettingDefinition[] settingDefinition)
        {
            throw new NotImplementedException();
        }

        IEnumerable<object> IEmbroRepository.GetLatestCommentsWithPhoto(int numItems, int p)
        {
            throw new NotImplementedException();
        }

        IEnumerable<object> IEmbroRepository.GetLatestEmbros(EmbroDateType embroDateType, int numItems)
        {
            throw new NotImplementedException();
        }

        Page IEmbroRepository.GetPageById(int id)
        {
            throw new NotImplementedException();
        }

        List<Page> IEmbroRepository.GetPages()
        {
            throw new NotImplementedException();
        }

        ICollection<Setting> IEmbroRepository.GetSettings(SettingsScope settingsScope)
        {
            throw new NotImplementedException();
        }

        IDictionary<string, string> IEmbroRepository.GetSettingValues(SettingsScope settingsScope)
        {
          return GetSettingValues(settingsScope)
            ;
        }

        IDictionary<string, int> IEmbroRepository.GetTagCounts(int? count)
        {
            throw new NotImplementedException();
        }

        void IEmbroRepository.SaveSettingValues(SettingsScope settingsScope, IDictionary<string, string> settings)
        {
            throw new NotImplementedException();
        }

        IPagedList<Comment> IEmbroRepository.GetCommentsForEmbro(int embroID, int pageNo, int pageSize)
        {
            throw new NotImplementedException();
        }

        IList<string> IEmbroRepository.GetExternalPhotoIds()
        {
            throw new NotImplementedException();
        }

        void IEmbroRepository.AddComments(ICollection<ProviderModel.PhotoProviders.CommentInfo> iCollection)
        {
            throw new NotImplementedException();
        }

        void IEmbroRepository.AddPhotos(ICollection<ProviderModel.PhotoProviders.PhotoInfo> iCollection)
        {
            throw new NotImplementedException();
        }

        Comment IEmbroRepository.GetCommentById(int id)
        {
            throw new NotImplementedException();
        }

        bool IEmbroRepository.EditComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        SiteStatistics IEmbroRepository.GetStatistics()
        {
            throw new NotImplementedException();
        }

        LogLevel? IEmbroRepository.GetHighestLogLevel(DateTimeOffset? minTime)
        {
            return LogLevel.Error;
        }

        IEnumerable<Log> IEmbroRepository.GetLogs(int count, int page, LogLevel? minLevel, string searchText)
        {
            throw new NotImplementedException();
        }

        void IEmbroRepository.CreateLog(Log entry)
        {
            return;
        }

        int IEmbroRepository.DeleteLogs(int minAge, LogLevel maxLevel)
        {
            throw new NotImplementedException();
        }

       
    }
}