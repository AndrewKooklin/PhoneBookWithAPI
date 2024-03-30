using Microsoft.AspNet.Identity.EntityFramework;

namespace PhoneBookWPF.Model
{
    public class RoleModel : IdentityRole
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
