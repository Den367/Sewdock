using System;
using System.Configuration;

namespace FileUpload.Models
{
    public class UtilitySettings
    {
        private static object _threadLock = new Object();
        private static UtilitySettings _Instance = null;

        private string _ApplicationName;
        private string _Author;
        private string _DevelopmentTime;
        private string _DeploymentVirtualDirectory;

        public string ApplicationName { get { return _ApplicationName; } }
        public string Author { get { return _Author; } }
        public string DevelopmentTime { get { return _DevelopmentTime; } }
        public string DeploymentVirtualDirectory { 
            get { return _DeploymentVirtualDirectory; } }

        private UtilitySettings()
        {
            _ApplicationName = ConfigurationManager.AppSettings["ApplicationName"];
            _Author = ConfigurationManager.AppSettings["Author"];
            _DevelopmentTime = ConfigurationManager.AppSettings["DevelopmentTime"];
            _DeploymentVirtualDirectory
                = ConfigurationManager.AppSettings["DeploymentVirtualDirectory"];
        }

        public static UtilitySettings GetInstance()
        {
            lock (_threadLock)
                if (_Instance == null)
                    _Instance = new UtilitySettings();

            return _Instance;
        }

    }
}
