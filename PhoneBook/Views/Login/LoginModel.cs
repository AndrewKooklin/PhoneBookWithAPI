using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Views.Login
{
    [AllowAnonymous]
    public class LoginModel
    {
        //[BindProperty]
        //public InputModel Input { get; set; }

        //public string ReturnUrl { get; set; }

        //[TempData]
        //public string ErrorMessage { get; set; }

        //public class InputModel
        //{
        //[Required(ErrorMessage = "Заполните поле \"UserName\"")]
        //[MinLength(3, ErrorMessage = "Длина не менее 3 символов")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "Заполните поле \"Email\"")]
        [EmailAddress(ErrorMessage = "Поле Email формата name@site.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните поле \"Пароль\"")]
        [MinLength(6, ErrorMessage = "Длина не менее 6 символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
        //}
    }
}
