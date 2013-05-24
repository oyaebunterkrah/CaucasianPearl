using System;
using System.ComponentModel.DataAnnotations;
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
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [StringLength(30, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 5)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Email { get; set; }

        [Display(Name = "Content", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [StringLength(2000, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 50)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Content { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 5)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Phone { get; set; }

        [Display(Name = "YourCity", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string City { get; set; }

        [Display(Name = "RequestDateTime", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.DateTime)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public DateTime RequestDateTime { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [StringLength(500, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 50)]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public string Comment { get; set; }

        [Display(Name = "Status", ResourceType = typeof(RequestModelRes))]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        public int Status { get; set; }
    }
}