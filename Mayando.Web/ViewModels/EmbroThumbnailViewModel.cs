using System.Collections.Generic;
using Myembro.Infrastructure;
using Myembro.Models;

namespace Myembro.ViewModels
{
    public class EmbroThumbnailViewModel
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string PngBase64Image { get; set; }
        public string Summary { get; set; }
        public long DownloadCount { get; set; }
        public IEnumerable<string> TagList { get; set; }
        public string Criteria { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}