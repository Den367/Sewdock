using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class EmbroDetailsViewModel
    {
        public EmbroideryItem Embro { get; private set; }
       
        public int PreferredSize { get; private set; }
        public bool HidePhotoText { get; private set; }
        public bool HidePhotoComments { get; private set; }
        public IEnumerable<Comment> Comments { get; private set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public int Id { get; set; }
        public string DisplayTitle { get; set; }
        public string Json { get; set; }
        public string Png { get; set; }
        public string Svg { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Article { get; set; }
        public bool Hidden { get; set; }
        public string Html { get; set; }
        public string Summary { get; set; }
        public long DownloadsCount { get; set; }

        public IEnumerable<string> TagList { get; private set; }

        public DateTimeOffset? Created  {get; set; }


        public EmbroDetailsViewModel(EmbroideryItem embro, bool hidePhotoText, bool hidePhotoComments)
        {
            if (embro != null)
            {
                Svg = embro.Svg;
                Json = embro.Json;
                Id = embro.Id;
                DisplayTitle = embro.DisplayTitle;
                Article = embro.Article;
                Summary = embro.Summary;
                DownloadsCount = embro.DownloadsCount;
            }
            this.Comments = new List<Comment>();
            this.HidePhotoText = hidePhotoText;
            this.HidePhotoComments = hidePhotoComments;
         
           
        }
    }
}