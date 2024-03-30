using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookWEB.Domain.Entities
{
    public class PhoneBookRecord : EntityBase
    {
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Заполните поле \"Фамилия\"")]
        [MinLength(3, ErrorMessage = "Длина не менее 3 символов")]
        [MaxLength(50, ErrorMessage = "Длина не более 50 символов")]
        public override string LastName { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Заполните поле \"Имя\"")]
        [MinLength(3, ErrorMessage = "Длина не менее 3 символов")]
        public override string FirstName { get; set; }

        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Заполните поле \"Отчество\"")]
        [MinLength(3, ErrorMessage = "Длина не менее 3 символов")]
        public override string FathersName { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Заполните поле \"Телефон\"")]
        [MinLength(11, ErrorMessage = "Длина не менее 11 символов")]
        public override string PhoneNumber { get; set; }

        [Display(Name = "Адрес")]
        public override string Address { get; set; }

        [Display(Name = "Описание")]
        public override string Description { get; set; }
    }
}
