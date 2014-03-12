
namespace Mayando.Web.ViewModels
{
    public class MenuViewModel
    {
        public string Url { get; private set; }
        public string Title { get; private set; }
        public string ToolTip { get; private set; }
        public bool OpenInNewWindow { get; private set; }
        public bool Selected { get; set; }

        public MenuViewModel(string url, string title, bool openInNewWindow, string toolTip)
        {
            this.Url = url;
            this.Title = title;
            this.OpenInNewWindow = openInNewWindow;
            this.ToolTip = toolTip;
        }
    }
}