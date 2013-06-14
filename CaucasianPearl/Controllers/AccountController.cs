using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using Resources.Account;
using WebMatrix.WebData;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Models;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            // TODO: учесть другие варианты
            ViewBag.ReturnUrl = Request.UrlReferrer != null &&
                                Request.UrlReferrer.AbsolutePath.IndexOf("notauthorized",
                                                                         StringComparison.InvariantCultureIgnoreCase) <
                                0
                                    ? Request.UrlReferrer.AbsolutePath
                                    : Consts.RootPath;

            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                return RedirectToLocal(returnUrl);

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, AccountErrorRes.TheUserNameOrPasswordProvidedIsIncorrect);
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(string returnUrl)
        {
            WebSecurity.Logout();

            return RedirectToLocal(returnUrl);
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
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction(Consts.Actions.Index, Consts.Controllers.Home.Name);
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError(string.Empty, ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            var ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (
                    var scope = new TransactionScope(TransactionScopeOption.Required,
                                                     new TransactionOptions
                                                         {
                                                             IsolationLevel = IsolationLevel.Serializable
                                                         }))
                {
                    var hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new {Message = message});
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess
                          ? "Your password has been set."
                          : message == ManageMessageId.RemoveLoginSuccess
                                ? "The external login was removed."
                                : string.Empty;
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            var hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword,
                                                                             model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                        return RedirectToAction(Consts.Controllers.Account.Actions.Manage,
                                                new {Message = ManageMessageId.ChangePasswordSuccess});

                    ModelState.AddModelError(string.Empty,
                                             AccountErrorRes.TheCurrentPasswordIsIncorrectOrTheNewPasswordIsInvalid);
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                var state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction(Consts.Controllers.Account.Actions.Manage,
                                                new {Message = ManageMessageId.SetPasswordSuccess});
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty,
                                                 String.Format(
                                                     AccountErrorRes
                                                         .UnableToCreateLocalAccountAnAccountWithTheNameMayAlreadyExist,
                                                     User.Identity.Name));
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
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new {ReturnUrl = returnUrl}));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new {returnUrl}));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }

            // User is new, ask for their desired membership name
            var loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View("ExternalLoginConfirmation",
                        new RegisterExternalLoginModel {UserName = result.UserName, ExternalLoginData = loginData});
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider;
            string providerUserId;

            if (User.Identity.IsAuthenticated ||
                !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (var caucasianPearlContext = new CaucasianPearlContext())
                {
                    var user = caucasianPearlContext.Profiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        caucasianPearlContext.Profiles.Add(new Profile { UserName = model.UserName });
                        caucasianPearlContext.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }

                    ModelState.AddModelError(key: "UserName",
                                             errorMessage:
                                                 AccountErrorRes.UserNameAlreadyExistsPleaseEnterADifferentUserName);
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            var accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            var externalLogins = new List<ExternalLogin>();
            foreach (var account in accounts)
            {
                var clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                    {
                        Provider = account.Provider,
                        ProviderDisplayName = clientData.DisplayName,
                        ProviderUserId = account.ProviderUserId,
                    });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 ||
                                       OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction(Consts.Actions.Index, Consts.Controllers.Home.Name);
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return AccountErrorRes.MembershipCreateStatus_DuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return AccountErrorRes.MembershipCreateStatus_DuplicateEmail;

                case MembershipCreateStatus.InvalidPassword:
                    return AccountErrorRes.MembershipCreateStatus_InvalidPassword;

                case MembershipCreateStatus.InvalidEmail:
                    return AccountErrorRes.MembershipCreateStatus_InvalidEmail;

                case MembershipCreateStatus.InvalidAnswer:
                    return AccountErrorRes.MembershipCreateStatus_InvalidAnswer;

                case MembershipCreateStatus.InvalidQuestion:
                    return AccountErrorRes.MembershipCreateStatus_InvalidQuestion;

                case MembershipCreateStatus.InvalidUserName:
                    return AccountErrorRes.MembershipCreateStatus_InvalidUserName;

                case MembershipCreateStatus.ProviderError:
                    return AccountErrorRes.MembershipCreateStatus_ProviderError;

                case MembershipCreateStatus.UserRejected:
                    return AccountErrorRes.MembershipCreateStatus_UserRejected;

                default:
                    return AccountErrorRes.MembershipCreateStatus_Default;
            }
        }

        [AllowAnonymous]
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session[Consts.SessionKeys.Culture] = new CultureInfo(lang);

            return Redirect(returnUrl);
        }

        #endregion
    }
}