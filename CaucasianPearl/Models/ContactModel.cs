using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models
{
    [MetadataType(typeof(ContactMetadata))]
    public class Contact
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Неправильный формат")]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", ErrorMessage = "Неправильный формат")]
        [DisplayName("Адрес почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DisplayName("Комментарий")]
        public string Comment { get; set; }
    }
}
