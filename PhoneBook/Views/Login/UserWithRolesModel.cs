using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Views.Login
{
    public class UserWithRolesModel
    {
        public IdentityUser User { get; set; }

        public List<string> Roles { get; set; }
    }
}
