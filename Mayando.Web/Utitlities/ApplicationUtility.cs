using System;
using System.Text;
using FileUpload.Models;

namespace FileUpload.Utilities
{
    public static class AppUtility
    {
        public static string FormatUrl(string pathWithoutVirtualDirectoryName)
        {
            UtilitySettings appInfomation
                = UtilitySettings.GetInstance();
            string DeploymentVirtualDirectory
                = appInfomation.DeploymentVirtualDirectory;

            if (string.IsNullOrEmpty(DeploymentVirtualDirectory))
            {
                return pathWithoutVirtualDirectoryName;
            }

            StringBuilder SB = new StringBuilder();
            SB.Append("/");
            SB.Append(appInfomation.DeploymentVirtualDirectory);
            SB.Append("/");
            SB.Append(pathWithoutVirtualDirectoryName);

            return SB.ToString();
        }

        public static string JQueryLink()
        {
            StringBuilder SB = new StringBuilder();
            SB.Append("<script src=\"");
            SB.Append(AppUtility.FormatUrl("/Scripts/jquery-1.4.1.min.js"));
            SB.Append("\" type=\"text/javascript\"></script>");

            return SB.ToString();
        }

        public static string AppStylelink()
        {
           StringBuilder SB = new StringBuilder();
            SB.Append("<link href=\"");
            SB.Append(AppUtility.FormatUrl("/Styles/AppStyles.css"));
            SB.Append("\" rel=\"stylesheet\" type=\"text/css\" />");

            return SB.ToString();
        }
    }
}