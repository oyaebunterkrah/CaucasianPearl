using System;
using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.Filters;
using Resources.Request;

namespace CaucasianPearl.Models.Metadata
{
    public class SiteSettingsMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [Display(Name = "Название")]
        //[Display(Name = "YourName", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string Name { get; set; }


        [Display(Name = "Значение")]
        // [Display(Name = "YourName", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(RequestValidationRes))]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string Value { get; set; }

        [Display(Name = "Описание")]
        // [Display(Name = "YourName", ResourceType = typeof(RequestModelRes))]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(RequestValidationRes), MinimumLength = 3)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        public string Description { get; set; }
    }
}