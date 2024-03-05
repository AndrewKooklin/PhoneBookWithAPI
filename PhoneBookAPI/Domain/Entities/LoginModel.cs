using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookAPI.Domain.Entities
{
    [AllowAnonymous]
    public class LoginModel
    {
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Заполните поле \"Email\"")]
        //[EmailAddress(ErrorMessage ="Поле Email формата name@site.com")]
        //public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
