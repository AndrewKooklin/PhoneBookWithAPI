using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace PhoneBookWPF.Model
{
    public class UserWithRolesModel
    {
        public IdentityUser User { get; set; }

        public List<string> Roles { get; set; }
    }
}
