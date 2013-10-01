using System;
using System.ComponentModel.DataAnnotations;

using CaucasianPearl.Core.Filters;
using CaucasianPearl.Resources;

namespace CaucasianPearl.Models.Metadata
{
    public class FeedbackMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [Display(Name = "YourName", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForEdit = false)]
        public string Name { get; set; }

        [Display(Name = "YourCity", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForEdit = false)]
        public string City { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(ModelRes))]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(2000, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 50)]
        [Show(ShowForEdit = false)]
        public string Comment { get; set; }

        [Display(Name = "DateTime", ResourceType = typeof(ModelRes))]
        [DataType(DataType.DateTime)]
        [Show(ShowForEdit = false)]
        public DateTime Created { get; set; }

        [Display(Name = "IsApproved", ResourceType = typeof(ModelRes))]
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public bool IsApproved { get; set; }
    }
}