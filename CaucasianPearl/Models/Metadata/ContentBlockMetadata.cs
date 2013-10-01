using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using CaucasianPearl.Core.Filters;
using CaucasianPearl.Resources;

namespace CaucasianPearl.Models.Metadata
{
    public class ContentBlockMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [Display(Name = "BlockId", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string BlockId { get; set; }

        [Display(Name = "PlaceHolderId", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string PlaceHolderId { get; set; }

        [Display(Name = "Content", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Description", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string Description { get; set; }

        [Display(Name = "IsPublished", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public bool IsPublished { get; set; }

        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int? Sequence { get; set; }
    }
}