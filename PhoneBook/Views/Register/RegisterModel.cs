using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Views.Register
{
    [AllowAnonymous]
    public class RegisterModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            //[Required(ErrorMessage = "Заполните поле \"UserName\"")]
            //[MinLength(3, ErrorMessage = "Длина не менее 3 символов.")]
            //[Display(Name = "UserName")]
            //public string UserName { get; set; }

            [Required(ErrorMessage = "Заполните поле \"Email\"")]
            [EmailAddress(ErrorMessage = "Поле Email формата name@site.com")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Заполните поле пароль")]
            [MinLength(6, ErrorMessage = "Длина не менее 6 символов.")]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Подтвердите пароль")]
            [Compare("Password", ErrorMessage = "Поля \"Пароль\" и \"Подтвердите пароль\" не совпадают.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Выберите поле \"Роль\"")]
            [Display(Name = "Роль")]
            public string Role { get; set; }

            public IEnumerable<SelectListItem> RolesList { get; set; }
        }
    }
}
