using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;
using Resources.Account;

namespace CaucasianPearl.Models.Metadata
{
    public class RegisterExternalLoginModelMetadata
    {
        [Display(Name = "LogOnModel_UserName", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        [RegularExpression(Consts.UserNameRegex, ErrorMessageResourceName = "RegisterModel_IncorrectUserName",
            ErrorMessageResourceType = typeof(AccountValidationRes))]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModelMetadata
    {
        [Display(Name = "ChangePasswordModel_CurrentPassword", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "ChangePasswordModel_NewPassword", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMin", ErrorMessageResourceType = typeof(AccountValidationRes), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "ChangePasswordModel_ConfirmNewPassword", ResourceType = typeof(AccountModelRes))]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceName = "ChangePasswordModel_PasswordAndConfirmationMismatch", ErrorMessageResourceType = typeof(AccountValidationRes))]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModelMetadata
    {
        [Display(Name = "LogOnModel_UserName", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        [RegularExpression(Consts.UserNameRegex, ErrorMessageResourceName = "RegisterModel_IncorrectUserName",
            ErrorMessageResourceType = typeof(AccountValidationRes))]
        public string UserName { get; set; }

        [Display(Name = "LogOnModel_Password", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "LogOnModel_RememberMe", ResourceType = typeof(AccountModelRes))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModelMetadata
    {
        [Display(Name = "RegisterModel_UserName", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int? Group { get; set; }

        [Display(Name = "Формат изображения")]
        [Show(ShowForDisplay = false, ShowForEdit = true)]
        [DataType(DataType.Text)]
        public string ImageExt { get; set; }

        [Display(Name = "Название отображаемое в URL")]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Название обязательно")]
        [RegularExpression("[a-z0-9_]+", ErrorMessage = "Только маленькие латинские буквы, цифры и подчёркивание")]
        public int? ShortName { get; set; }

        [Display(Name = "Порядок")]
        [Show(ShowForDisplay = false, ShowForEdit = true)]
        [DataType(DataType.Text)]
        public string Sequence { get; set; }


        [Display(Name = "RegisterModel_Email", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(Consts.EmailRegex, ErrorMessageResourceName = "RegisterModel_IncorrectEmailAddress", ErrorMessageResourceType = typeof(AccountValidationRes))]
        public string Email { get; set; }

        [Display(Name = "RegisterModel_Password", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMin", ErrorMessageResourceType = typeof(AccountValidationRes), MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "RegisterModel_ConfirmPassword", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "RegisterModel_PasswordsMustMatch", ErrorMessageResourceType = typeof(AccountValidationRes))]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLoginMetadata
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}