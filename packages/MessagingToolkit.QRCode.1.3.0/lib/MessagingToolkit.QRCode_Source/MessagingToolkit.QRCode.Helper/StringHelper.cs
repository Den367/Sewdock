namespace MessagingToolkit.QRCode.Helper
{
    using System;

    public sealed class StringHelper
    {
        private static readonly bool AssumeShiftJis = ("SHIFT-JIS".Equals(PlatformDefaultEncoding, StringComparison.InvariantCultureIgnoreCase) || "EUC-JP".Equals(PlatformDefaultEncoding, StringComparison.InvariantCultureIgnoreCase));
        private const string EucJP = "EUC-JP";
        public const string GB2312 = "GB2312";
        private const string ISO88591 = "ISO-8859-1";
        private static readonly string PlatformDefaultEncoding = "ISO-8859-1";
        public const string ShiftJis = "SHIFT-JIS";
        private const string Utf8 = "UTF-8";

        private StringHelper()
        {
        }

        public static string GuessEncoding(byte[] bytes)
        {
            if ((((bytes.Length > 3) && (bytes[0] == 0xef)) && (bytes[1] == 0xbb)) && (bytes[2] == 0xbf))
            {
                return "UTF-8";
            }
            int length = bytes.Length;
            bool flag = true;
            bool flag2 = true;
            bool flag3 = true;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            for (int i = 0; (i < length) && ((flag || flag2) || flag3); i++)
            {
                int num6 = bytes[i] & 0xff;
                if ((num6 >= 0x80) && (num6 <= 0xbf))
                {
                    if (num2 > 0)
                    {
                        num2--;
                    }
                }
                else
                {
                    if (num2 > 0)
                    {
                        flag3 = false;
                    }
                    if ((num6 >= 0xc0) && (num6 <= 0xfd))
                    {
                        flag5 = true;
                        for (int j = num6; (j & 0x40) != 0; j = j << 1)
                        {
                            num2++;
                        }
                    }
                }
                if (((num6 == 0xc2) || (num6 == 0xc3)) && (i < (length - 1)))
                {
                    int num8 = bytes[i + 1] & 0xff;
                    if ((num8 <= 0xbf) && (((num6 == 0xc2) && (num8 >= 160)) || ((num6 == 0xc3) && (num8 >= 0x80))))
                    {
                        flag4 = true;
                    }
                }
                if ((num6 >= 0x7f) && (num6 <= 0x9f))
                {
                    flag = false;
                }
                if (((num6 >= 0xa1) && (num6 <= 0xdf)) && !flag6)
                {
                    num4++;
                }
                if (!(flag6 || ((((num6 < 240) || (num6 > 0xff)) && (num6 != 0x80)) && (num6 != 160))))
                {
                    flag2 = false;
                }
                if (((num6 >= 0x81) && (num6 <= 0x9f)) || ((num6 >= 0xe0) && (num6 <= 0xef)))
                {
                    if (flag6)
                    {
                        flag6 = false;
                    }
                    else
                    {
                        flag6 = true;
                        if (i >= (bytes.Length - 1))
                        {
                            flag2 = false;
                        }
                        else
                        {
                            int num9 = bytes[i + 1] & 0xff;
                            if ((num9 < 0x40) || (num9 > 0xfc))
                            {
                                flag2 = false;
                            }
                            else
                            {
                                num3++;
                            }
                        }
                    }
                }
                else
                {
                    flag6 = false;
                }
            }
            if (num2 > 0)
            {
                flag3 = false;
            }
            if (flag2 && AssumeShiftJis)
            {
                return "SHIFT-JIS";
            }
            if (flag3 && flag5)
            {
                return "UTF-8";
            }
            if (flag2 && ((num3 >= 3) || ((20 * num4) > length)))
            {
                return "SHIFT-JIS";
            }
            if (!(flag4 || !flag))
            {
                return "ISO-8859-1";
            }
            return PlatformDefaultEncoding;
        }
    }
}

