using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models
{
    [MetadataType(typeof (RegisterExternalLoginModelMetadata))]
    public class RegisterExternalLoginModel
    {
        public string UserName { get; set; }
        public string ExternalLoginData { get; set; }
    }

    [MetadataType(typeof (LocalPasswordModelMetadata))]
    public class LocalPasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }


    [MetadataType(typeof (LoginModelMetadata))]
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    [MetadataType(typeof (RegisterModelMetadata))]
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int? Group { get; set; }
        public string ImageExt { get; set; }
        public int? ShortName { get; set; }
        public string Sequence { get; set; }
    }

    [MetadataType(typeof (ExternalLoginMetadata))]
    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}