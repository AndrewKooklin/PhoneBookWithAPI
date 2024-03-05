using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWPF.Model
{
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
