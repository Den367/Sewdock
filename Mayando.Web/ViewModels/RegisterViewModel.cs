
 using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Mayando.Web.Properties;

namespace Mayando.Web.ViewModels
{
    public class RegisterViewModel :CaptchaBase
    {


        [Required(ErrorMessageResourceType = typeof(Mayando.Web.Properties.Resources), ErrorMessageResourceName = "RegisterViewModel_Email_ErrorMessage")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mayando.Web.Properties.Resources), ErrorMessageResourceName = "RegisterViewModel_Password_ErrorMessage")]
        public string Password { get; set; }

        [System.Web.Mvc.Compare("Password", ErrorMessageResourceType = typeof(Mayando.Web.Properties.Resources), ErrorMessageResourceName = "RegisterViewModel_ConfirmPassword_ErrorMessage")]
        public string ConfirmPassword { get; set; }

         [Required(ErrorMessageResourceType = typeof(Mayando.Web.Properties.Resources), ErrorMessageResourceName = "RegisterViewModel_UserName_ErrorMessage")]
        public string UserName { get; set; }
        #region [Validation]
        #endregion [Validation]

    }
}