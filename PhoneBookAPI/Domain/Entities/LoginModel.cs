using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookAPI.Domain.Entities
{
    [AllowAnonymous]
    public class LoginModel
    {
        public string EMail { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
