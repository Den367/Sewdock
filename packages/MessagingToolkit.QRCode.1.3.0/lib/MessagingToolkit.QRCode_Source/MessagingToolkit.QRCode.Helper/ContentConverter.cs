namespace MessagingToolkit.QRCode.Helper
{
    using System;

    public class ContentConverter
    {
        internal static char n = '\n';

        public static string Convert(string targetString)
        {
            if (targetString != null)
            {
                if (targetString.IndexOf("MEBKM:") > -1)
                {
                    targetString = ConvertDocomoBookmark(targetString);
                }
                if (targetString.IndexOf("MECARD:") > -1)
                {
                    targetString = ConvertDocomoAddressBook(targetString);
                }
                if (targetString.IndexOf("MATMSG:") > -1)
                {
                    targetString = ConvertDocomoMailto(targetString);
                }
                if (targetString.IndexOf(@"http\://") > -1)
                {
                    targetString = ReplaceString(targetString, @"http\://", "\nhttp://");
                }
            }
            return targetString;
        }

        private static string ConvertDocomoAddressBook(string targetString)
        {
            targetString = RemoveString(targetString, "MECARD:");
            targetString = RemoveString(targetString, ";");
            targetString = ReplaceString(targetString, "N:", "NAME1:");
            targetString = ReplaceString(targetString, "SOUND:", n + "NAME2:");
            targetString = ReplaceString(targetString, "TEL:", n + "TEL1:");
            targetString = ReplaceString(targetString, "EMAIL:", n + "MAIL1:");
            targetString = targetString + n;
            return targetString;
        }

        private static string ConvertDocomoBookmark(string targetString)
        {
            targetString = RemoveString(targetString, "MEBKM:");
            targetString = RemoveString(targetString, "TITLE:");
            targetString = RemoveString(targetString, ";");
            targetString = RemoveString(targetString, "URL:");
            return targetString;
        }

        private static string ConvertDocomoMailto(string s)
        {
            string str = s;
            char ch = '\n';
            return (ReplaceString(ReplaceString(ReplaceString(RemoveString(RemoveString(str, "MATMSG:"), ";"), "TO:", "MAILTO:"), "SUB:", ch + "SUBJECT:"), "BODY:", ch + "BODY:") + ch);
        }

        private static string RemoveString(string s, string s1)
        {
            return ReplaceString(s, s1, "");
        }

        private static string ReplaceString(string s, string s1, string s2)
        {
            string str = s;
            for (int i = str.IndexOf(s1, 0); i > -1; i = str.IndexOf(s1, (int) (i + s2.Length)))
            {
                str = str.Substring(0, i) + s2 + str.Substring(i + s1.Length);
            }
            return str;
        }
    }
}

