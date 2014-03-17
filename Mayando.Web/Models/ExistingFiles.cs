using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Globalization;

namespace FileUpload.Models
{
    public sealed class ExistingFilesModel :IDisposable
    {
        private DataTable UploadedFiles;

        public ExistingFilesModel()
        {
            UploadedFiles = new DataTable();
            UploadedFiles.Locale = CultureInfo.InvariantCulture;
            DataColumn IDColumn = UploadedFiles.Columns.Add("ID", Type.GetType("System.Int32"));
            IDColumn.AutoIncrement = true;
            IDColumn.AutoIncrementSeed = 1;
            IDColumn.AutoIncrementStep = 1;
            DataColumn[] keys = new DataColumn[1];
            keys[0] = IDColumn;
            UploadedFiles.PrimaryKey = keys;

            UploadedFiles.Columns.Add("File Name", Type.GetType("System.String"));
            UploadedFiles.Columns.Add("File Size", Type.GetType("System.Int32"));
            UploadedFiles.Columns.Add("Context Type", Type.GetType("System.String"));
            UploadedFiles.Columns.Add("Time Uploadeded", Type.GetType("System.DateTime"));
            UploadedFiles.Columns.Add("File Data", Type.GetType("System.Byte[]"));
        }

        public DataTable GetUploadedFiles() { return UploadedFiles; }

        public void AddAFile(string fileName, int size, string contentType, Byte[] fileData)
        {
            DataRow row = UploadedFiles.NewRow();
            UploadedFiles.Rows.Add(row);

            row["File Name"] = fileName;
            row["File Size"] = size;
            row["Context Type"] = contentType;
            row["Time Uploadeded"] = System.DateTime.Now;
            row["File Data"] = fileData;
        }

        public void DeleteAFile(int id)
        {
            DataRow RowToDelete = UploadedFiles.Rows.Find(id);
            if (RowToDelete != null)
            {
                UploadedFiles.Rows.Remove(RowToDelete);
            }
        }

       private bool disposed = false;

    //Implement IDisposable.
    public void Dispose()
    {
        UploadedFiles.Clear();
        UploadedFiles.Dispose();
        Dispose(true);
        GC.SuppressFinalize(this);
    }

     void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Free other state (managed objects).
            }
            // Free your own state (unmanaged objects).
            UploadedFiles.Clear();
            UploadedFiles.Dispose();
            // Set large fields to null.
            disposed = true;
        }
    }

    // Use C# destructor syntax for finalization code.
    ~ExistingFilesModel()
    {
        // Simply call Dispose(false).
        Dispose (false);
    }
    }
}
