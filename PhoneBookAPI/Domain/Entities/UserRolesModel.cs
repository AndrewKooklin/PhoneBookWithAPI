using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookAPI.Domain.Entities
{
    public class UserRolesModel
    {
        public static string EMail { get; set; } = "";

        public static List<string> Roles { get; set; }
    }
}
