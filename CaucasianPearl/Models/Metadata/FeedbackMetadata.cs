using System;
using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.Filters;
using Resources.Feedback;

namespace CaucasianPearl.Models.Metadata
{
    public class FeedbackMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [Display(Name = "YourName", ResourceType = typeof(FeedbackModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(FeedbackValidationRes))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(FeedbackValidationRes), MinimumLength = 3)]
        [Show(ShowForEdit = false)]
        public string Name { get; set; }

        [Display(Name = "YourCity", ResourceType = typeof(FeedbackModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(FeedbackValidationRes))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(FeedbackValidationRes), MinimumLength = 3)]
        [Show(ShowForEdit = false)]
        public string City { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(FeedbackModelRes))]
        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(FeedbackValidationRes))]
        [StringLength(2000, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(FeedbackValidationRes), MinimumLength = 50)]
        [Show(ShowForEdit = false)]
        public string Comment { get; set; }

        [Display(Name = "Suggestion", ResourceType = typeof(FeedbackModelRes))]
        [DataType(DataType.Text)]
        [StringLength(2000, ErrorMessageResourceName = "StringLengthMax", ErrorMessageResourceType = typeof(FeedbackValidationRes), MinimumLength = 50)]
        [Show(ShowForEdit = false)]
        public string Suggestion { get; set; }

        [Display(Name = "FeedbackDateTime", ResourceType = typeof(FeedbackModelRes))]
        [DataType(DataType.DateTime)]
        [Show(ShowForEdit = false)]
        public DateTime FeedbackDateTime { get; set; }

        [Display(Name = "IsApproved", ResourceType = typeof(FeedbackModelRes))]
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public bool IsApproved { get; set; }
    }
}