
namespace Myembro.ViewModels
{
    public class HtmlEditorViewModel
    {
        public string FormFieldName { get; private set; }
        public string Text { get; private set; }
        public int Height { get; private set; }

        public HtmlEditorViewModel(string formFieldName, string text)
            : this(formFieldName, text, 5)
        {
        }

        public HtmlEditorViewModel(string formFieldName, string text, int height)
        {
            this.FormFieldName = formFieldName;
            this.Text = text;
            this.Height = height;
        }
    }
}