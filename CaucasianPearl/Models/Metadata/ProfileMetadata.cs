using System.ComponentModel.DataAnnotations;

using CaucasianPearl.Core.Filters;
using Resources;

namespace CaucasianPearl.Models.Metadata
{
    public class ProfileMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(30, ErrorMessageResourceName = "StringLengthMax",
            ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string UserName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [EmailAddress(ErrorMessageResourceName = "WrongEmailFormat", ErrorMessageResourceType = typeof(ValidationRes), ErrorMessage = null)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string Email { get; set; }

        [Display(Name = "Name", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string LastName { get; set; }

        [Display(Name = "Position", ResourceType = typeof(ModelRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string Position { get; set; }

        [Display(Name = "Description", ResourceType = typeof(ModelRes))]
        [StringLength(500, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string Description { get; set; }

        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string ImageExt { get; set; }

        public int? ShortName { get; set; }

        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public int? Sequence { get; set; }
    }
}