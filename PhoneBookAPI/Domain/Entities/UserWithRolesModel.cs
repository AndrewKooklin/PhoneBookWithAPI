using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookAPI.Domain.Entities
{
    public class UserWithRolesModel
    {
        public IdentityUser User { get; set; }

        public List<string> Roles { get; set; }
    }
}
