using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;
using Resources.Account;

namespace CaucasianPearl.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base(Consts.DefaultConnectionName)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Display(Name = "LogOnModel_UserName", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        [RegularExpression(Consts.UserNameRegex, ErrorMessageResourceName = "RegisterModel_IncorrectUserName",
            ErrorMessageResourceType = typeof(AccountValidationRes))]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
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

    public class LoginModel
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

    public class RegisterModel
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

        [Display(Name = "RegisterModel_Email", ResourceType = typeof(AccountModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountValidationRes))]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(Consts.EmailRegex, ErrorMessageResourceName = "RegisterModel_IncorrectEmailAddress", ErrorMessageResourceType = typeof(AccountValidationRes))]
        public string Email { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 11)]
        [Display(Name = "Mobile No.")]
        public string MobileNumber { get; set; }

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

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
