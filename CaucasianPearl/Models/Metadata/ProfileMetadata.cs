using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.Filters;
using Resources.Profile;
using Resources.Shared;

namespace CaucasianPearl.Models.Metadata
{
    public class ProfileMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [Display(Name = "UserName", ResourceType = typeof (ProfileModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof (SharedValidationRes))]
        [StringLength(30, ErrorMessageResourceName = "StringLengthMax",
            ErrorMessageResourceType = typeof (SharedValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string UserName { get; set; }

        [Display(Name = "Email", ResourceType = typeof (ProfileModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof (SharedValidationRes))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof (SharedValidationRes),
            ErrorMessageResourceName = "FormatIncorrect")]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}",
            ErrorMessageResourceType = typeof (SharedValidationRes), ErrorMessageResourceName = "FormatIncorrect")]
        public string Email { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(ProfileModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax",
            ErrorMessageResourceType = typeof(SharedValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(ProfileModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax",
            ErrorMessageResourceType = typeof(SharedValidationRes), MinimumLength = 3)]
        public string LastName { get; set; }

        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public int? Group { get; set; }

        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string ImageExt { get; set; }

        public int? ShortName { get; set; }

        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public int? Sequence { get; set; }
    }
}