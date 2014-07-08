using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using Mayando.Web.Infrastructure;
using Mayando.Web.Properties;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with user accounts.")]
    public class AccountController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "account";

        #endregion

        #region Constructors

        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.

        public AccountController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IMembershipService service)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = service ?? new AccountMembershipService();
        }

        #endregion

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public IMembershipService MembershipService
        {
            get;
            protected set;
        }

        #region Actions
        [Description("Allows the user to send an email to the website owner.")]
        public ActionResult Index()
        {
            return View(ViewName.Index);
        }

        [Description("Allows a user to log on.")]
        public ActionResult LogOn([Description("The URL to which to return.")]string returnUrl)
        {
            this.ViewData[ViewDataKeyReturnUrl] = returnUrl;
            return View(ViewName.Index);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl)
        {

            if (!ValidateLogOn(userName, password))
            {
                Logger.Log(LogLevel.Warning, string.Format(CultureInfo.CurrentCulture, "The '{0}' user attempted to log on but failed.", userName));
                return View(ViewName.Index);
            }

            FormsAuth.SignIn(userName, rememberMe);
            Logger.Log(LogLevel.Information, string.Format(CultureInfo.CurrentCulture, "The '{0}' user successfully logged on.", userName));
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToHomepage();
            }
        }

        [Description("Allows the current user to log off.")]
        public ActionResult LogOff()
        {

            FormsAuth.SignOut();

            return RedirectToHomepage();
        }


        [HttpGet]
        public ActionResult Register()
        {
            var newUserView = new RegisterViewModel();
            newUserView.GenerateCaptcha();
            return View(newUserView);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel userView)
        {
            if (!userView.IsCaptchaMatched())
            {
                ModelState.AddModelError("Captcha", Resources.AccountController_Register_Текст_с_картинки_введен_неверно);
            }
            
            if (MembershipService.CheckEmailExist( userView.Email))
            {
                ModelState.AddModelError("Email", Resources.AccountController_Register_Пользователь_с_таким_email_уже_зарегистрирован);
            }

            if (ModelState.IsValid)
            {
                MembershipService.CreateUser(userView.UserName, userView.Password, userView.Email);
            }
            else return View(userView);
            return RedirectToHomepage();
        }

        [Authorize]
        [Description("Allows the current user to change the password.")]
        public ActionResult ChangePassword()
        {
            var model = new AccountViewModel(MembershipService.MinPasswordLength);
            return View(model);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exceptions result in password not being changed.")]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var model = new AccountViewModel(MembershipService.MinPasswordLength);

            if (!ValidateChangePassword(currentPassword, newPassword, confirmPassword))
            {
                return View(model);
            }

            try
            {
                if (MembershipService.ChangePassword(User.Identity.Name, currentPassword, newPassword))
                {
                    SetPageFlash("Your password was successfully changed.");
                    return RedirectToHomepage();
                }
                else
                {
                    ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                    return View(model);
                }
            }
            catch
            {
                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                return View(model);
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        #endregion

        #region Validation Methods

        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }
            if (newPassword == null || newPassword.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            if (!MembershipService.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }

            return ModelState.IsValid;
        }

        #endregion
    }

    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }


    #region [Membership]
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool CheckEmailExist(string eMail);
    }

    public class AccountMembershipService : IMembershipService
    {
        protected MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);

        }

        public bool CheckEmailExist(string eMail)
        {
            var userName = _provider.GetUserNameByEmail(eMail);
            if (string.IsNullOrWhiteSpace(userName)) return false;
            return true;
        }


        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            //return currentUser.ChangePassword(currentUser.ResetPassword()   , newPassword);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }
    }
    #endregion

}
