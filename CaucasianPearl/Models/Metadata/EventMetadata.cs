using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;

namespace CaucasianPearl.Models.Metadata
{
    public class EventMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [DataType(DataType.Text)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(50, ErrorMessage = "Не более 50 символов")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Дата события")]
        [DisplayFormat(DataFormatString = Consts.DateTimeFormat, ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [DataType(DataType.Html)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Описание")]
        [AllowHtml]
        public string Description { get; set; }

        [DataType(DataType.Html)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Содержание")]
        [DisplayFormat(HtmlEncode = true)]
        [AllowHtml]
        public string Content { get; set; }

        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int Cover { get; set; }

        [DataType(DataType.Text)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Название отображаемое в URL")]
        [Required(ErrorMessage = "Название обязательно")]
        [RegularExpression("[a-z0-9_]+", ErrorMessage = "Только маленькие латинские буквы, цифры и подчёркивание")]
        public int? ShortName { get; set; }

        [DataType(DataType.Text)]
        [Show(ShowForDisplay = false, ShowForEdit = true)]
        [Display(Name = "Порядок")]
        public string Sequence { get; set; }
    }
}