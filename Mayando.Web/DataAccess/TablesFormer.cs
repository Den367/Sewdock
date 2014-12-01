using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Myembro.DataAccess
{
    public class DataTablesFormer
    {
        #region[ctor]
        public DataTablesFormer()
        {
            CreateEmbroTableColumns();
        
        }

        #endregion [ctor]
        readonly DataTable emptyEmbroTable = new DataTable();

        public DataTable EmbroTable { get { return emptyEmbroTable; } }
        /// <summary>
        /// Fills column mapping without identity column Id
        /// </summary>
        /// <param name="bulkCopy"> instance of <see cref="SqlBulkCopy"/></param>
        public void FillEmbroColumnMapping(SqlBulkCopy bulkCopy)
        {
           
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Data", "Data"));
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Svg", "Svg"));    
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Title", "Title"));
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Article", "Article"));
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Created", "Created"));
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Published", "Published"));
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Hidden", "Hidden"));        
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Tags", "Tags"));
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Html", "Html"));
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Summary", "Summary"));
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("UserID", "UserID"));
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("TypeID", "TypeID"));
            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Json", "Json"));
           bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Png", "Png"));
        }

        void CreateEmbroTableColumns()
        {

            emptyEmbroTable.Columns.AddRange
                (new DataColumn[]{
         
            new DataColumn("Data", typeof(byte[])),
            new DataColumn("Svg", typeof(string)),
            new DataColumn("Title",typeof (string)),
            new DataColumn("Article", typeof(string)),
            new DataColumn("Created", typeof(DateTimeOffset)),
            new DataColumn("Published", typeof(DateTimeOffset)),
            new DataColumn("Hidden", typeof(bool)),
            new DataColumn("Tags", typeof(string)),
            new DataColumn("Html", typeof(string)),
            new DataColumn("Summary", typeof(string)),
            new DataColumn("UserID", typeof(string)),
            new DataColumn("TypeID", typeof(int)),
            new DataColumn("Json", typeof(string)),
            new DataColumn("Png", typeof(string))

                 });

        }
    }
}