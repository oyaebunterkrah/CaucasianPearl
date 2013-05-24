using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;

namespace CaucasianPearl.Models.Metadata
{
    public class OneNewsMetadata
    {
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int ID { get; set; }

        [Display(Name = "Название")]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Не более 50 символов")]
        public string Name { get; set; }

        [Show(ShowForDisplay = false, ShowForEdit = false)]
        public int CreatedBy { get; set; }

        [Display(Name = "Дата создания новости")]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты")]
        [DisplayFormat(DataFormatString = Consts.DateFormat, ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Время создания новости")]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = Consts.TimeFormat, ApplyFormatInEditMode = true)]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "Дата последнего обновления")]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = Consts.DateFormat, ApplyFormatInEditMode = true)]
        public DateTime LastUpdatedDate { get; set; }

        [Display(Name = "Время последнего обновления")]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = Consts.TimeFormat, ApplyFormatInEditMode = true)]
        public DateTime LastUpdatedTime { get; set; }

        [Display(Name = "Обновил")]
        [Show(ShowForDisplay = true, ShowForEdit = false)]
        [DataType(DataType.Text)]
        public string LastUpdatedUser { get; set; }

        [DataType(DataType.Html)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Содержание")]
        [DisplayFormat(HtmlEncode = true)]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Формат изображения")]
        [Show(ShowForDisplay = false, ShowForEdit = true)]
        [DataType(DataType.Text)]
        public string ImageExt { get; set; }

        [Display(Name = "Название отображаемое в URL")]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Название обязательно")]
        [RegularExpression("[a-z0-9_]+", ErrorMessage = "Только маленькие латинские буквы, цифры и подчёркивание")]
        public int? ShortName { get; set; }

        [Display(Name = "Порядок")]
        [Show(ShowForDisplay = false, ShowForEdit = true)]
        [DataType(DataType.Text)]
        public string Sequence { get; set; }
    }
}