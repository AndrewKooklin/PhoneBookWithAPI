using PhoneBookWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWPF.HelpMethods
{
    public class AddNewUser
    {
        GetDBContext getDBContext = new GetDBContext();
        
        public void Add(User user)
        {
            //var db = getDBContext.GetDB();
            //db.Users.Add(user);
            //db.SaveChanges();
        }
    }
}
