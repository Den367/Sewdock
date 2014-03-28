using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using Mayando.Web.Models;

namespace Mayando.Web.DataAccess
{
    public class EmbroDBManager :IDisposable
    {
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        SqlBulkCopy bulkCopy;
        /// <summary>
        ///Connection to interact with DataBase  <see cref="SqlConnection"/>
        /// </summary>
        public SqlConnection Connection { get {
            return sqlConnection ??
                   (sqlConnection =
                    new SqlConnection {ConnectionString = ConfigurationManager.AppSettings["connectionString"]});
        }
        }
        public SqlCommand Command { get
        {
                       return Connection.CreateCommand();
        } }
        public SqlBulkCopy BulkCopy { get
        {
            if (bulkCopy == null) bulkCopy = new SqlBulkCopy(Connection, SqlBulkCopyOptions.FireTriggers, null);
            return bulkCopy;
        } }
        #region [ctor]
        public EmbroDBManager()
        {

            
            
        }
        #endregion [ctor]
        public delegate void FillColumnsMap(SqlBulkCopy bulkCopy);
        /// <summary>
        /// Сохраняем таблицу массовым копированием в базу
        /// </summary>
        /// <param name="isch">подключение к БД</param>
        /// <param name="table">таблица</param>
        /// <param name="tableName">имя таблицы</param>
        /// <param name="fillColumnMappings">маппинг колонок</param>
        public bool ImportTable(SqlConnection isch, DataTable table, string tableName, FillColumnsMap fillColumnMappings)
        {
            try
            {
                if (isch.State != ConnectionState.Open) isch.Open();
                if (isch.State != ConnectionState.Open) throw new Exception("Connection has not been opened!");
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(isch))
                {
                    fillColumnMappings(bulkCopy);
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.WriteToServer(table);
                }
                table.Clear();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                isch.Close();
            }
            return true;
        }

        public bool ImportTable( DataTable table, string tableName)
        {
            try
            {
                if (Connection.State != ConnectionState.Open) sqlConnection.Open();
                if (Connection.State != ConnectionState.Open) throw new Exception("Connection has not been opened!");
              
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.WriteToServer(table);
               
                table.Clear();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return true;
        }

        /// <summary>
        /// Вызывает хранимую процедуру с параметрами
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="Parameters">набор параметров</param>
        public void ExecuteSP(string spName, params SqlParameter[] Parameters)
        {
            using (
                SqlCommand cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (Parameters != null)
                    foreach (SqlParameter sqlPrm in Parameters)
                    {
                        cmd.Parameters.Add(sqlPrm);

                    }
                //cmd.CommandTimeout = Config.CommandTimeout;

                cmd.ExecuteNonQuery();

            }
        }


        IDataReader GetResultSetFromStoredProc(string spName, params SqlParameter[] Parameters)
        {
            

            SqlCommand cmd = sqlConnection.CreateCommand();
            
                cmd.CommandType = CommandType.StoredProcedure;
                if (Parameters != null)
                    foreach (SqlParameter sqlPrm in Parameters)
                    {
                        cmd.Parameters.Add(sqlPrm);

                    }
                //cmd.CommandTimeout = Config.CommandTimeout;


                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            
            
        }


      



        #region Dispose()
        bool _isDisposed = false;
        /// <summary>
        /// Освобождение объекта.
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {               
                if (sqlConnection != null) sqlConnection.Close();
                sqlConnection = null;
               if (bulkCopy != null) bulkCopy.Close();
                GC.SuppressFinalize(this);
                _isDisposed = true;
            }
        }
        #endregion
    }
}