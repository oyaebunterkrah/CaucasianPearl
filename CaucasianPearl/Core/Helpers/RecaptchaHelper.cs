using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using CaucasianPearl.Properties;

namespace CaucasianPearl.Core.Helpers
{
    public static class ReCaptchaValidation
    {
        public static bool Validate(string challengeValue, string responseValue)
        {
            var captchaValidtor = new Recaptcha.RecaptchaValidator
            {
                PrivateKey = Convert.ToString(Settings.Default.ReCaptchaPrivateKey), // Get Private key of the CAPTCHA from Web.config file.
                RemoteIP = HttpContext.Current.Request.UserHostAddress,
                Challenge = challengeValue,
                Response = responseValue
            };

            var recaptchaResponse = captchaValidtor.Validate(); // Send data about captcha validation to reCAPTCHA site.
            return recaptchaResponse.IsValid; // Get boolean value about Captcha success / failure.
        }
    }
}