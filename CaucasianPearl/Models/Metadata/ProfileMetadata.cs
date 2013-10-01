using System.ComponentModel.DataAnnotations;

using CaucasianPearl.Resources;

namespace CaucasianPearl.Models.Metadata
{
    public class ProfileMetadata
    {
        public int ID { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Position", ResourceType = typeof(ModelRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        public string Position { get; set; }

        [Display(Name = "Description", ResourceType = typeof(ModelRes))]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Login", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        public string UserName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(ModelRes))]
        [EmailAddress(ErrorMessageResourceName = "WrongEmailFormat", ErrorMessageResourceType = typeof(ValidationRes), ErrorMessage = null)]
        public string Email { get; set; }

        [Display(Name = "ShortName", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(30, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        public string ShortName { get; set; }

        [Display(Name = "Sequence", ResourceType = typeof(ModelRes))]
        public int? Sequence { get; set; }

        [Display(Name = "ImageExt", ResourceType = typeof(ModelRes))]
        public string ImageExt { get; set; }
    }
}