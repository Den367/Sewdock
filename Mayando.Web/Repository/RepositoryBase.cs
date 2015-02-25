using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Myembro.DataAccess;

namespace Myembro.Repository
{
     [CLSCompliant(false)]
    public class RepositoryBase:IDisposable
    {
        protected StuffFactory factory = new StuffFactory();
        protected DataTablesFormer tableFormer;
        protected EmbroDBManager manager;
        protected SqlConnection _connection;
         protected object locker = new object();
        public RepositoryBase()
        {
            tableFormer = factory.TablesHolder;
            manager = factory.Manager;
            tableFormer.FillEmbroColumnMapping(manager.BulkCopy);
            _connection = manager.Connection;
           
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

        protected bool? GetBooleanFromReader(SqlDataReader reader, string fieldName)
        {

            if (reader.GetValue(reader.GetOrdinal(fieldName)) != DBNull.Value)
                return reader.GetBoolean(reader.GetOrdinal(fieldName));
            return null;
        }

        protected DateTime? GetDateTimeFromReader(SqlDataReader reader, string fieldName)
        {

            if (reader.GetValue(reader.GetOrdinal(fieldName)) != DBNull.Value)
                return reader.GetDateTime(reader.GetOrdinal(fieldName));
            return null;
        }

        protected DateTimeOffset? GetDateTimeOffsetFromReader(SqlDataReader reader, string fieldName)
        {

            if (reader.GetValue(reader.GetOrdinal(fieldName)) != DBNull.Value)
                return reader.GetDateTimeOffset(reader.GetOrdinal(fieldName));
            return null;
        }

        protected int GetIntParam(SqlCommand cmd, string paramName)
        {
            var obj = cmd.Parameters[paramName].Value;
            if (obj != null) return (int)obj;
            return 0;
        }

        protected object GetValueFromReader<T>(SqlDataReader reader, string fieldName)
        {


            var value = reader.GetValue(reader.GetOrdinal(fieldName));
            if (value != DBNull.Value) return (T)value;
            return null;

        }

        protected object GetValueFromCommand<T>(SqlCommand cmd, string fieldName)
        {


            var value = cmd.Parameters[fieldName].Value;
            if (value != DBNull.Value) return (T)value;
            return null;

        }
        


        #region [Disposable]
        private bool disposed = false; // to detect redundant calls
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

         ~RepositoryBase()
         {
             Dispose(false);
         }

       
        protected virtual void Dispose(bool disposing)
        {
            lock (locker)
            {
                if (!disposed)
                {
                    if (disposing)
                    {
                        if (manager != null)
                        {

                            manager.Dispose();
                            manager = null;
                        }
                        if (_connection != null) _connection.Dispose();

                    }

                    // There are no unmanaged resources to release, but
                    // if we add them, they need to be released here.
                }
                disposed = true;
            }

        }
        #endregion [Disposable]
    }
}