using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Views.Roles
{
    public class MyIdentityRole : IdentityRole
    {

        [Required(ErrorMessage = "Заполните поле \"Роль\"")]
        [MinLength(4, ErrorMessage = "Длина не менее 4 символов.")]
        [Display(Name = "Роль")]
        public override string Name { get; set; }
    }
}
