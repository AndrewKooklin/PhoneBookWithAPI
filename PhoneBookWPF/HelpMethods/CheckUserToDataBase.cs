using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWPF.HelpMethods
{
    public class CheckUserToDataBase
    {
        GetDBContext context = new GetDBContext();

        public bool CheckUser(string userName, string password)
        {
            //var db = context.GetDB();
            bool userExist = false/*db.Users.Any(u => u.UserName == userName && u.Password == password)*/;
            return userExist;
        }
    }
}
