using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Mayando.Web.ViewModels
{
    public abstract class CaptchaBase
    {
       // private string _captcha;
        public void GenerateCaptcha()
        {
            var digits = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture).Replace("0", "5");
            Captcha = GetCaptchaFromDigits(digits);

        }
                public string Captcha { get; set; }
        public string UserInputCaptcha { get; set; }

            public bool IsCaptchaMatched()
        {
            var digits = GetDigitsFromCaptcha(Captcha);
            return UserInputCaptcha.Equals(digits);
        }

        private string GetCaptchaFromDigits(string digits)
        {
            var encoded = from a in digits.ToCharArray()
                          select (Convert.ToChar(captchaSymbols[a]));
            return new string(encoded.ToArray(), 0, 4);
        }

        private string GetDigitsFromCaptcha(string captcha)
        {
            var encoded = from a in captcha.ToCharArray()
                          select (  (captchaUserInput.ContainsKey(a))? Convert.ToChar(captchaUserInput[a]): 'X');
            return new string(encoded.ToArray(), 0, 4);
        }

        
        private readonly Dictionary<char, char> captchaSymbols = new Dictionary<char, char> { { '1', 'u'}, { '2', 'v' }, { '3', 'w' }, { '4', 'x' }, { '5', 'y' }, { '6', 'z' }, { '7', '{' }, { '8', 't' }, { '9', '}' } };
        private readonly Dictionary<char, char> captchaUserInput = new Dictionary<char, char> { { 'u','1'}, {  'v','2' }, {  'w','3' }, {  'x','4' }, {  'y', '5'}, { 'z', '6' }, { '{','7' }, { 't' ,'8' }, { '}', '9' } };
     
    }
}