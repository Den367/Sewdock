using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using CaptchaMvc.Attributes;
using Myembro.Extensions;
using Myembro.Infrastructure;
using Myembro.Properties;
using Myembro.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;


using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Myembro.Models;

namespace Myembro.Controllers
{
    [Description("Handles actions that have to do with user accounts.")]
    [SessionState(SessionStateBehavior.Default)]
    [Authorize]
    public class AccountController : SiteControllerBase
       
    {
         


        //protected override void Initialize(RequestContext requestContext)
        //{

        //    // Initialize culture.
        //    SiteControllerBase.SetCultureForHttpRequest(requestContext.HttpContext.Request);


        //    // Execute the actual request.
        //    base.Initialize(requestContext);


        //}

        public
             AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            //to support email address as user name
            UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager) { AllowOnlyAlphanumericUserNames = false };
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }



        //
        // GET: /Account/Index
        [AllowAnonymous]
        public ActionResult Index()
        {
            //var requestContext = Request.RequestContext;
            //SiteControllerBase.SetCultureForHttpRequest(requestContext.HttpContext.Request);
            return View();
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //var requestContext = Request.RequestContext;
            //SiteControllerBase.SetCultureForHttpRequest(requestContext.HttpContext.Request);
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Myembro.Models.RegisterViewModel model)
        {
            await Task.Yield();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }


        public ActionResult ChangePassword()
        {

            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View("Manage", new ManageUserViewModel());
            //var model = new ManageUserViewModel();
            //return RedirectToAction("Manage",model);
        }


        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion













        #region Constants

    //    public const string ControllerName = "account";

        #endregion

        #region Constructors

    //    // This constructor is used by the MVC framework to instantiate the controller using
    //    // the default forms authentication and membership providers.

    //    //public AccountController()
    //    //    : this(null, null)
    //    //{
    //    //}

    //    // This constructor is not used by the MVC framework but is instead provided for ease
    //    // of unit testing this type. See the comments at the end of this file for more
    //    // information.
    //    public AccountController(IFormsAuthentication formsAuth, IMembershipService service)
    //    {
    //        FormsAuth = formsAuth ?? new FormsAuthenticationService();
    //        MembershipService = service ?? new AccountMembershipService();
    //    }

        #endregion

    //    public IFormsAuthentication FormsAuth
    //    {
    //        get;
    //        private set;
    //    }

    //    public IMembershipService MembershipService
    //    {
    //        get;
    //        protected set;
    //    }

        #region Actions
    //    [Description("Allows the user to send an email to the website owner.")]
    //    public ActionResult Index()
    //    {
    //        return View(ViewName.Index);
    //    }

    //    [Description("Allows a user to log on.")]
    //    public ActionResult LogOn([Description("The URL to which to return.")]string returnUrl)
    //    {
    //        this.ViewData[ViewDataKeyReturnUrl] = returnUrl;
    //        return View(ViewName.Index);
    //    }

    //    [AcceptVerbs(HttpVerbs.Post)]
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Justification = "Needs to take same parameter type as Controller.Redirect()")]  
    //    public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl)
    //    {

    //        if (!ValidateLogOn(userName, password))
    //        {
    //            Logger.Log(LogLevel.Warning, string.Format(CultureInfo.CurrentCulture, "The '{0}' user attempted to log on but failed.", userName));
    //            return View(ViewName.Index);
    //        }

    //        FormsAuth.SignIn(userName, rememberMe);
    //        Logger.Log(LogLevel.Information, string.Format(CultureInfo.CurrentCulture, "The '{0}' user successfully logged on.", userName));
    //        if (!String.IsNullOrEmpty(returnUrl))
    //        {
    //            return Redirect(returnUrl);
    //        }
    //        else
    //        {
    //            return RedirectToHomepage();
    //        }
    //    }

    //    [Description("Allows the current user to log off.")]
    //    public ActionResult LogOff()
    //    {

    //        FormsAuth.SignOut();

    //        return RedirectToHomepage();
    //    }


    //    [HttpGet]
    //    public ActionResult Register()
    //    {
    //        var newUserView = new RegisterViewModel();
    //        newUserView.GenerateCaptcha();
    //        return View(newUserView);
    //    }

    //     [HttpPost, CaptchaVerify("Captcha is not valid")]
    //    public ActionResult Register(Myembro.Models.RegisterViewModel userView)
    //    {
    //         if (!ModelState.IsValid)
    //         {
    //             TempData["ErrorMessage"] = Myembro.Properties.Resources.CaptchaIsNotValidMessageText;
    //             return View(userView);
    //         }
    //         if (!userView.IsCaptchaMatched())
    //        {
    //            ModelState.AddModelError("Captcha", Myembro.Properties.Resources.AccountController_Register_Текст_с_картинки_введен_неверно);
    //        }
            
    //        if (MembershipService.CheckEmailExist( userView.Email))
    //        {
    //            ModelState.AddModelError("Email", Myembro.Properties.Resources.AccountController_Register_Пользователь_с_таким_email_уже_зарегистрирован);
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            MembershipService.CreateUser(userView.UserName, userView.Password, userView.Email);
    //        }
    //        else return View(userView);
          
    //        //TempData["Message"] = Myembro.Properties.Resources.AccountRegisterSuccessfull;
    //        return RedirectToHomepage();
    //    }

    //    [Authorize]
    //    [Description("Allows the current user to change the password.")]
    //    public ActionResult ChangePassword()
    //    {
    //        var model = new AccountViewModel(MembershipService.MinPasswordLength);
    //        return View(model);
    //    }

    //    [Authorize]
    //    [AcceptVerbs(HttpVerbs.Post)]
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exceptions result in password not being changed.")]
    //    public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
    //    {
    //        var model = new AccountViewModel(MembershipService.MinPasswordLength);

    //        if (!ValidateChangePassword(currentPassword, newPassword, confirmPassword))
    //        {
    //            return View(model);
    //        }

    //        try
    //        {
    //            if (MembershipService.ChangePassword(User.Identity.Name, currentPassword, newPassword))
    //            {
    //                SetPageFlash("Your password was successfully changed.");
    //                return RedirectToHomepage();
    //            }
    //            else
    //            {
    //                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
    //                return View(model);
    //            }
    //        }
    //        catch
    //        {
    //            ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
    //            return View(model);
    //        }
    //    }

    //    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        if (filterContext.HttpContext.User.Identity is WindowsIdentity)
    //        {
    //            throw new InvalidOperationException("Windows authentication is not supported.");
    //        }
    //    }

    //    #endregion

    //    #region Validation Methods

    //    private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
    //    {
    //        if (String.IsNullOrEmpty(currentPassword))
    //        {
    //            ModelState.AddModelError("currentPassword", "You must specify a current password.");
    //        }
    //        if (newPassword == null || newPassword.Length < MembershipService.MinPasswordLength)
    //        {
    //            ModelState.AddModelError("newPassword",
    //                String.Format(CultureInfo.CurrentCulture,
    //                     "You must specify a new password of {0} or more characters.",
    //                     MembershipService.MinPasswordLength));
    //        }

    //        if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
    //        {
    //            ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
    //        }

    //        return ModelState.IsValid;
    //    }

    //    private bool ValidateLogOn(string userName, string password)
    //    {
    //        if (String.IsNullOrEmpty(userName))
    //        {
    //            ModelState.AddModelError("username", "You must specify a username.");
    //        }
    //        if (String.IsNullOrEmpty(password))
    //        {
    //            ModelState.AddModelError("password", "You must specify a password.");
    //        }
    //        if (!MembershipService.ValidateUser(userName, password))
    //        {
    //            ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
    //        }

    //        return ModelState.IsValid;
    //    }

    //    #endregion
    //}

    //// The FormsAuthentication type is sealed and contains static members, so it is difficult to
    //// unit test code that calls its members. The interface and helper class below demonstrate
    //// how to create an abstract wrapper around such a type in order to make the AccountController
    //// code unit testable.

    //public interface IFormsAuthentication
    //{
    //    void SignIn(string userName, bool createPersistentCookie);
    //    void SignOut();
    //}

    //public class FormsAuthenticationService : IFormsAuthentication
    //{
    //    public void SignIn(string userName, bool createPersistentCookie)
    //    {
    //        FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
    //    }
    //    public void SignOut()
    //    {
    //        FormsAuthentication.SignOut();
    //    }
    //}


    //#region [Membership]
    //public interface IMembershipService
    //{
    //    int MinPasswordLength { get; }

    //    bool ValidateUser(string userName, string password);
    //    MembershipCreateStatus CreateUser(string userName, string password, string email);
    //    bool ChangePassword(string userName, string oldPassword, string newPassword);
    //    bool CheckEmailExist(string eMail);
    //}

    //public class AccountMembershipService : IMembershipService
    //{
    //    protected MembershipProvider _provider;

    //    public AccountMembershipService()
    //        : this(null)
    //    {
    //    }

    //    public AccountMembershipService(MembershipProvider provider)
    //    {
    //        _provider = provider ?? Membership.Provider;
    //    }

    //    public int MinPasswordLength
    //    {
    //        get
    //        {
    //            return _provider.MinRequiredPasswordLength;
    //        }
    //    }

    //    public bool ValidateUser(string userName, string password)
    //    {
    //        return _provider.ValidateUser(userName, password);

    //    }

    //    public bool CheckEmailExist(string eMail)
    //    {
    //        var userName = _provider.GetUserNameByEmail(eMail);
    //        if (string.IsNullOrWhiteSpace(userName)) return false;
    //        return true;
    //    }


    //    public MembershipCreateStatus CreateUser(string userName, string password, string email)
    //    {
    //        MembershipCreateStatus status;
    //        _provider.CreateUser(userName, password, email, null, null, true, null, out status);
    //        return status;
    //    }

    //    public bool ChangePassword(string userName, string oldPassword, string newPassword)
    //    {
    //        MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
    //        //return currentUser.ChangePassword(currentUser.ResetPassword()   , newPassword);
    //        return currentUser.ChangePassword(oldPassword, newPassword);
    //    }
    }
    #endregion

}
