using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using  System.IO;
using EmbroideryFile.QR;

namespace EmbroideryFile.QRCode
{
    public class QrcodePng
    {
     
        private readonly StitchToBmp _bitmapCreator;
        readonly IQRCodeStitchGeneration _stitchGen;
        private ImageFormat _format;


        #region [Public Properties]
        public QRCodeStitchInfo QrStitchInfo {
            set
            {
                _stitchGen.Info = value;   

            }
        }

       
        #endregion [Public Properties]
        public QrcodePng(string qRCodeText, int width, int height)
        {
            _stitchGen = new QrCodeStitcher{Info = new QRCodeStitchInfo {QrCodeText = qRCodeText}};
            

            _bitmapCreator = new StitchToBmp(_stitchGen.GetQRCodeStitchBlocks(), width, height);
            
           
         
        }
/*
        public void SaveJpeg(Image image, string path, int quality)
        {
            //ensure the quality is within the correct range
            if ((quality < 0) || (quality > 100))
            {
                //create the error message
                string error = string.Format("Jpeg image quality must be between 0 and 100, with 100 being the highest quality.  A value of {0} was specified.", quality);
                //throw a helpful exception
                throw new ArgumentOutOfRangeException(error);
            }

            //create an encoder parameter for the image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            //get the jpeg codec
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            //create a collection of all parameters that we will pass to the encoder
            EncoderParameters encoderParams = new EncoderParameters(1);
            //set the quality parameter for the codec
            encoderParams.Param[0] = qualityParam;
            //save the image using the codec and the parameters
            image.Save(path, jpegCodec, encoderParams);
        }
*/

        private Dictionary<string, ImageCodecInfo> encoders = null;
        /// <summary>
        /// A quick lookup for getting image encoders
        /// </summary>
        public Dictionary<string, ImageCodecInfo> Encoders()
        {
            //get accessor that creates the dictionary on demand

            //if the quick lookup isn't initialised, initialise it
            if (encoders == null)
            {
                encoders = new Dictionary<string, ImageCodecInfo>();
            }

            //if there are no codecs, try loading them
            if (encoders.Count == 0)
            {
                //get all the codecs
                foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                {
                    //add each codec to the quick lookup
                    encoders.Add(codec.MimeType.ToLower(), codec);
                }
            }

            //return the lookup
            return encoders;

        }
        #region [Public Methods]       

        public void FillStreamWithPng(Stream stream)
        {
            _bitmapCreator.FillStreamWithPng(stream);

        }




        #endregion [Public Methods]
       
    }
    }

