using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;
using Resources;

namespace CaucasianPearl.Models.Metadata
{
    public class RegisterExternalLoginModelMetadata
    {
        [Display(Name = "UserName", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [RegularExpression(Consts.UserNameRegex, ErrorMessageResourceName = "IncorrectUserName",
            ErrorMessageResourceType = typeof(ValidationRes))]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModelMetadata
    {
        [Display(Name = "ChangePasswordModel_CurrentPassword", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "NewPassword", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMin", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "ChangePasswordModel_ConfirmNewPassword", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessageResourceName = "PasswordAndConfirmationMismatch", ErrorMessageResourceType = typeof(ValidationRes))]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModelMetadata
    {
        [Display(Name = "UserName", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [RegularExpression(Consts.UserNameRegex, ErrorMessageResourceName = "IncorrectUserName",
            ErrorMessageResourceType = typeof(ValidationRes))]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "NeedRemember", ResourceType = typeof(ModelRes))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModelMetadata
    {
        [Display(Name = "UserName", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
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


        [Display(Name = "Email", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(Consts.EmailRegex, ErrorMessageResourceName = "IncorrectEmailAddress", ErrorMessageResourceType = typeof(ValidationRes))]
        public string Email { get; set; }

        [Display(Name = "Password", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMin", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = "PasswordsMustMatch", ErrorMessageResourceType = typeof(ValidationRes))]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLoginMetadata
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}