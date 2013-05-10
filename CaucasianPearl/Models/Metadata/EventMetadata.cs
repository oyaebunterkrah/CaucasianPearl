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
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Название отображаемое в URL")]
        [Required(ErrorMessage = "Название обязательно")]
        [RegularExpression("[a-z0-9_]+", ErrorMessage = "Только маленькие латинские буквы, цифры и подчёркивание")]
        public string ShortName { get; set; }

        [DataType(DataType.Date)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Дата начала события")]
        [DisplayFormat(DataFormatString = Consts.DateFormat, ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Дата окончания события")]
        [DisplayFormat(DataFormatString = Consts.DateFormat, ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Time)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Время начала события")]
        [DisplayFormat(DataFormatString = Consts.TimeFormat, ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Time)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Время окончания события")]
        [DisplayFormat(DataFormatString = Consts.TimeFormat, ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [DataType(DataType.Text)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Место")]
        public string Place { get; set; }

        [DataType(DataType.Text)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Тип")]
        public string Type { get; set; }

        [DataType(DataType.Html)]
        [Show(ShowForDisplay = true, ShowForEdit = true)]
        [Display(Name = "Описание")]
        [AllowHtml]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [Show(ShowForDisplay = false, ShowForEdit = false)]
        [Display(Name = "Формат изображения")]
        public string ImageExt { get; set; }

        [DataType(DataType.Text)]
        [Show(ShowForDisplay = false, ShowForEdit = true)]
        [Display(Name = "Порядок")]
        public string Sequence { get; set; }
    }
}