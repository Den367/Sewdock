﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EmbroideryFile;
using EmbroideryFile.QRCode;
using Myembro.Interfaces;

namespace Myembro.Infrastructure
{


    public class PngImageResult : ActionResult
    {
        private const string ImageFormat = "image/png";
        private readonly IWritePng2Stream _png;

        public PngImageResult(IWritePng2Stream png)
        {

            _png = png;
        }

        public override void ExecuteResult(ControllerContext context)
        {

            var response = context.HttpContext.Response;

            response.Clear();
            response.Charset = String.Empty;
            response.ContentType = ImageFormat;

          
                // PNG can only write to a seek-able stream
                //  Thus we have to go through a memory stream, which permits seeking.
                using (var mStream = new MemoryStream())
                {
                    try
                    {
                        if(_png != null) _png.FillStreamWithPng(mStream);
                        mStream.Seek(0, SeekOrigin.Begin);
                        mStream.CopyTo(response.OutputStream);
                        
                    }
                    catch (Exception ex)
                    {

                        Logger.LogException(ex);
                    }
                 
                }
         

            
           

        }

    }
}
