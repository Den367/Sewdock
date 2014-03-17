using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Helper;

namespace EmbroideryFile.QRCode
{
    internal class QRCodeCreator
    {

        public bool[][] GetQRCodeMatrix(string DataToEncode)
        {

            if (string.IsNullOrEmpty(DataToEncode))
                return null;

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.CharacterSet = "ASCII";
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 1;
            qrCodeEncoder.QRCodeVersion = -1;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            return qrCodeEncoder.CalQrcode(Encoding.ASCII.GetBytes(DataToEncode));

        }

        //public IDataReader GetIReader()
        //{
        //    IDataReader result = null;
        //    try
        //    {
        //        if (typeof(IDbConnection).IsAssignableFrom(providerAssembly.GetType(ConnTypeName)))
        //        {
        //            dbConn = (IDbConnection)Activator.CreateInstance(providerAssembly.GetType(ConnTypeName));
        //            if (dbConn != null)
        //            {
        //                dbConn.ConnectionString = connectionString;
        //                dbConn.Open();
        //                if (dbConn.State != ConnectionState.Open) throw new Exception("Не удалось открыть подключение!");
        //                dbComm = (IDbCommand)Activator.CreateInstance(providerAssembly.GetType(CommTypeName));
        //                // using (dbComm)
        //                {

        //                    dbComm.Connection = dbConn;
        //                    dbComm.CommandTimeout = comandTimeout;
        //                    dbComm.CommandText = queryText;
        //                    IDataReader reader = dbComm.ExecuteReader();
        //                    result = reader;
        //                }
        //            }
        //            else throw new Exception(" GetIReader()Не удалось инициализировать экземпляр подключения");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteTrace(ex.Message);
        //    }
        //    return result;

        //}
    }
}
