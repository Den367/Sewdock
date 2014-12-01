
namespace Myembro.ViewModels
{
    public class DeleteViewModel
    {
        public string ItemName { get; private set; }
        public string ItemDescription { get; private set; }

        public DeleteViewModel(string itemName, string itemDescription)
        {
            this.ItemName = itemName;
            this.ItemDescription = itemDescription;
        }
    }
}