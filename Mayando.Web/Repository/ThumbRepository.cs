using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Mayando.Web.DataAccess;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Repository
{
     [CLSCompliant(false)]
    public class ThumbRepository : RepositoryBase,IThumbRepository
    {
        private readonly ThumbCommandFormer _commands;
        public ThumbRepository ()
        {
            manager = factory.Manager;
            _commands = new ThumbCommandFormer(manager.Connection);
        }

        public EmbroNavigationContext GetNavigationContextByCountPage(EmbroNavigationContext context)
        {
           
            var thumbs = new List<EmbroThumbnailViewModel>();
            var cmd = _commands.GetReadThumbsByCountPageCommand(context.PageSize, context.PageNumber, context.Criteria);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read()) return null;
                do
                {
                    var thumb = ReadThumb(reader);
                    thumbs.Add(new EmbroThumbnailViewModel
                        {
                            PngBase64Image = thumb.Png,TagList = thumb.TagList, DownloadCount = thumb.DownloadsCount, Summary = thumb.Summary,Id = thumb.Id 
                        });                  

                } while (reader.Read());
            }

           
            cmd.Connection.Close();
            context.TotalItemCount = (int)cmd.Parameters["TotalItem"].Value;
            context.Embros = thumbs;
            if (thumbs.Count() > 1) context.CurrentEmbroID = thumbs[0].Id;
            return context;
        }

            private EmbroideryItem ReadThumb(SqlDataReader reader)
            {
                var thumb = new EmbroideryItem();

                if (!IsDBNull(reader, "Id")) thumb.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                //if (withData && (reader.GetValue(reader.GetOrdinal("Data")) != DBNull.Value))
                //  thumb.Data = (byte[])reader[reader.GetOrdinal("Data")];

                //if (reader.GetValue(reader.GetOrdinal("Svg")) != DBNull.Value)
                //  thumb.Svg = reader.GetString(reader.GetOrdinal("Svg"));
                if (reader.GetValue(reader.GetOrdinal("Png")) != DBNull.Value)
                    thumb.Png = reader.GetString(reader.GetOrdinal("Png"));
                //if (reader.GetValue(reader.GetOrdinal("Json")) != DBNull.Value)
                //  thumb.Json = reader.GetString(reader.GetOrdinal("Json"));

                if (reader.GetValue(reader.GetOrdinal("Title")) != DBNull.Value)
                    thumb.Title = reader.GetString(reader.GetOrdinal("Title"));
                if (reader.GetValue(reader.GetOrdinal("Article")) != DBNull.Value)
                    thumb.Article = reader.GetString(reader.GetOrdinal("Article"));

                thumb.Created = reader.GetDateTimeOffset(reader.GetOrdinal("Created"));
                if (!IsDBNull(reader, "Published"))
                    thumb.Published = reader.GetDateTimeOffset(reader.GetOrdinal("Published"));
                thumb.Hidden = reader.GetBoolean(reader.GetOrdinal("Hidden"));
                thumb.XmlTags = reader.GetString(reader.GetOrdinal("Tags"));
                if (!IsDBNull(reader, "Html")) thumb.Html = reader.GetString(reader.GetOrdinal("Html"));
                thumb.Summary = reader.GetString(reader.GetOrdinal("Summary"));
                if (!IsDBNull(reader, "TypeID")) thumb.TypeID = reader.GetInt32(reader.GetOrdinal("TypeID"));
                if (!IsDBNull(reader, "UserID")) thumb.UserID = reader.GetGuid(reader.GetOrdinal("UserID"));
                if (!IsDBNull(reader, "Downloads")) thumb.DownloadsCount = reader.GetInt64(reader.GetOrdinal("Downloads"));
                return thumb;
            }

    }
}