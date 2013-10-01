using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Resources;

namespace CaucasianPearl.Models.Metadata
{
    public class EventMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [Display(Name = "Title", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Title { get; set; }

        [Display(Name = "EventDate", ResourceType = typeof(ModelRes))]
        [DisplayFormat(DataFormatString = Consts.DateFormat, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceName = "WrongDateFormat", ErrorMessageResourceType = typeof(ValidationRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public DateTime EventDate { get; set; }

        [Display(Name = "Description", ResourceType = typeof(ModelRes))]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(2048, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Content", ResourceType = typeof(ModelRes))]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(2048, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Cover", ResourceType = typeof(ModelRes))]
        public int Cover { get; set; }

        [Display(Name = "ShortName", ResourceType = typeof(ModelRes))]
        public int? ShortName { get; set; }

        [Display(Name = "Sequence", ResourceType = typeof(ModelRes))]
        public string Sequence { get; set; }
    }
}