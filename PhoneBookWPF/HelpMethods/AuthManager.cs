using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookWPF.HelpMethods
{
    public class AuthManager
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthManager(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public bool CheckLogIn()
        {
            return false;
        }
    }
}
