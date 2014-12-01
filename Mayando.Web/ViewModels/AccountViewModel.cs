
namespace Myembro.ViewModels
{
    public class AccountViewModel
    {
        public int MinPasswordLength { get; private set; }

        public AccountViewModel(int minPasswordLength)
        {
            this.MinPasswordLength = minPasswordLength;
        }
    }
}