using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWPF.Model
{
    public class LoginModel
    {
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public string ErrorMessage { get; set; }

        public class InputModel
        {
            public string UserName { get; set; }

            //[Required(ErrorMessage = "Заполните поле \"Email\"")]
            //[EmailAddress(ErrorMessage ="Поле Email формата name@site.com")]
            //public string Email { get; set; }

            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }
    }
}
