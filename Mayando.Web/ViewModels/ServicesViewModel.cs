
namespace Myembro.ViewModels
{
    public class ServicesViewModel
    {
        public bool ServiceApiEnabled { get; private set; }
        public string ApiKey { get; private set; }

        public ServicesViewModel(bool serviceApiEnabled, string apiKey)
        {
            this.ServiceApiEnabled = serviceApiEnabled;
            this.ApiKey = apiKey;
        }
    }
}