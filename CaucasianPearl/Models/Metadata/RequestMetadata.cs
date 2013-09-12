using System;
using System.ComponentModel.DataAnnotations;

using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;
using Resources;

namespace CaucasianPearl.Models.Metadata
{
    public class RequestMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [Display(Name = "YourName", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Name { get; set; }

        [Display(Name = "YourCity", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string City { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 5)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Phone { get; set; }

        [Display(Name = "Email", ResourceType = typeof(ModelRes))]
        [EmailAddress(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "WrongEmailFormat", ErrorMessage = null)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Email { get; set; }

        [Display(Name = "RequestText", ResourceType = typeof(ModelRes))]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(2000, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Content { get; set; }

        [Display(Name = "DesiredDate", ResourceType = typeof(ModelRes))]
        [DisplayFormat(DataFormatString = Consts.DateFormat, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceName = "WrongDateFormat", ErrorMessageResourceType = typeof(ValidationRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public DateTime RequestDate { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(ModelRes))]
        [DisplayFormat(DataFormatString = Consts.DateFormat, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceName = "WrongDateFormat", ErrorMessageResourceType = typeof(ValidationRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public DateTime Created { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(ModelRes))]
        [DataType(DataType.MultilineText)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Comment { get; set; }
        
        [Display(Name = "Status", ResourceType = typeof(ModelRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public int Status { get; set; }
    }
}