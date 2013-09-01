using System;
using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;
using Resources.Request;

namespace CaucasianPearl.Models.Metadata
{
    public class RequestMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [Display(Name = "YourName", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Name { get; set; }

        [Display(Name = "YourCity", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string City { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 5)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Phone { get; set; }

        [Display(Name = "Email", ResourceType = typeof(RequestModelRes))]
        [EmailAddress(ErrorMessageResourceType = typeof(RequestValidationRes), ErrorMessageResourceName = "WrongEmailFormat", ErrorMessage = null)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Email { get; set; }

        [Display(Name = "RequestDateTime", ResourceType = typeof(RequestModelRes))]
        [DisplayFormat(DataFormatString = Consts.DateFormat, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceName = "WrongDateFormat", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public DateTime RequestDateTime { get; set; }

        [Display(Name = "Content", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [StringLength(2000, ErrorMessageResourceName = "StringLengthMinMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Content { get; set; }

        [Display(Name = "RequestRegistrationDate", ResourceType = typeof(RequestModelRes))]
        [DisplayFormat(DataFormatString = Consts.DateFormat, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceName = "WrongDateFormat", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public DateTime RequestRegistrationDate { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Comment { get; set; }

        [Display(Name = "Status", ResourceType = typeof(RequestModelRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public int Status { get; set; }
    }
}