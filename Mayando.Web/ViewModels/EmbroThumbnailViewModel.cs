using System.Collections.Generic;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class EmbroThumbnailViewModel
    {
        public int Id { get; set; }
        public string PngBase64Image { get; set; }
        public string Summary { get; set; }
        public long DownloadCount { get; set; }
        public IEnumerable<string> TagList { get; set; }
    }
}