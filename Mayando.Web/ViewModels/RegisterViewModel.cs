using System.Resources;
 using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Myembro.Properties;
using System.Web.Security;
namespace Myembro.ViewModels
{
    public class RegisterViewModel 
    {


        [Required(ErrorMessageResourceType = typeof(Myembro.Properties.Resources), ErrorMessageResourceName = "RegisterViewModel_Email_ErrorMessage")]       
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Myembro.Properties.Resources), ErrorMessageResourceName = "RegisterViewModel_Password_ErrorMessage")]        
        [MinLength(6, ErrorMessage = "Минимальная длина пароля 6 символов")]
        [MaxLength(20, ErrorMessage = "Максимальная длина пароля 20 символов")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Invalid")]
        [RegularExpression( @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$", ErrorMessage = @"Пароль может содержать только латинские символы, дефисы, подчеркивания, точки<br/>
В пароле должны быть символы как в верхнем, так и в нижнем регистре<br/>В пароле должна присутствовать хотя бы одна цифра")]
        public string Password { get; set; }

        [System.Web.Mvc.Compare("Password", ErrorMessageResourceType = typeof(Myembro.Properties.Resources), ErrorMessageResourceName = "RegisterViewModel_ConfirmPassword_ErrorMessage")]
        public string ConfirmPassword { get; set; }

         [Required(ErrorMessageResourceType = typeof(Myembro.Properties.Resources), ErrorMessageResourceName = "RegisterViewModel_UserName_ErrorMessage")]
        public string UserName { get; set; }
        #region [Validation]
        #endregion [Validation]

    }
}