using System;
using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Resources;

namespace CaucasianPearl.Models.Metadata
{
    public class SiteSettingsMetadata
    {
        public int ID { get; set; }

        [Display(Name = "Title", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Value", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        public string Value { get; set; }

        [Display(Name = "DefaultValue", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        public string DefaultValue { get; set; }

        [Display(Name = "Description", ResourceType = typeof(ModelRes))]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        public string Description { get; set; }
    }
}