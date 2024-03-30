using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookWEB.Domain.Entities
{
    [AllowAnonymous]
    public class LoginModel
    {
        [Required(ErrorMessage = "Заполните поле \"Email\"")]
        [EmailAddress(ErrorMessage = "Поле Email формата name@site.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните поле \"Пароль\"")]
        [MinLength(6, ErrorMessage = "Длина пароля не менее 6 символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
