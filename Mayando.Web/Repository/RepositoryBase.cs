using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Mayando.Web.DataAccess;

namespace Mayando.Web.Repository
{
     [CLSCompliant(false)]
    public class RepositoryBase:IDisposable
    {
        protected StuffFactory factory = new StuffFactory();
        protected DataTablesFormer tableFormer;
        protected EmbroDBManager manager;

        public RepositoryBase()
        {
            tableFormer = factory.TablesHolder;
            manager = factory.Manager;
            tableFormer.FillEmbroColumnMapping(manager.BulkCopy);
        }


        protected bool IsDBNull(SqlDataReader reader, string columnName)
        {
            return reader.GetValue(reader.GetOrdinal(columnName)) == DBNull.Value;
        }

        protected bool IsDBNull(SqlDataReader reader, int columnNum)
        {
            return reader.GetValue(columnNum) == DBNull.Value;
        }

        protected string GetStringFromReader(SqlDataReader reader, string fieldName)
        {
            string value = string.Empty;
            if (reader.GetValue(reader.GetOrdinal(fieldName)) != DBNull.Value)
                value = reader.GetString(reader.GetOrdinal(fieldName));
            return value;
        }

        protected Guid? GetGuidFromReader(SqlDataReader reader, string fieldName)
        {
            Guid? value = null;
            if (reader.GetValue(reader.GetOrdinal(fieldName)) != DBNull.Value)
                value = reader.GetGuid(reader.GetOrdinal(fieldName));
            return value;
        }

        protected int? GetInt32FromReader(SqlDataReader reader, string fieldName)
        {
          
            if (reader.GetValue(reader.GetOrdinal(fieldName)) != DBNull.Value)
                return reader.GetInt32(reader.GetOrdinal(fieldName));
            return null;
        }

        


        #region [Disposable]
        private bool disposed = false; // to detect redundant calls
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (manager != null)
                    {
                        factory.Manager.Dispose();
                    }

                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }
            disposed = true;


        }
        #endregion [Disposable]
    }
}