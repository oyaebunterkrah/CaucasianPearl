using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Resources;

namespace CaucasianPearl.Models.Metadata
{
    public class SponsorMetadata
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

        [Display(Name = "Email", ResourceType = typeof(ModelRes))]
        [EmailAddress(ErrorMessageResourceName = "WrongEmailFormat", ErrorMessageResourceType = typeof(ValidationRes), ErrorMessage = null)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string Email { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 5)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Phone { get; set; }

        [Display(Name = "Sum", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationRes))]
        [Range(1, 10000, ErrorMessageResourceName = "SumRange", ErrorMessageResourceType = typeof(ValidationRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public int Sum { get; set; }

        [Display(Name = "Url", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Url)]
        [StringLength(2000, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(ValidationRes), MinimumLength = 10)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Url { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Text)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Comment { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(ModelRes))]
        [DisplayFormat(DataFormatString = Consts.DateFormat, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceName = "WrongDateFormat", ErrorMessageResourceType = typeof(ErrorRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public DateTime Created { get; set; }

        [Display(Name = "Confirmed", ResourceType = typeof(ModelRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public bool Confirmed { get; set; }

        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string ImageExt { get; set; }
    }
}