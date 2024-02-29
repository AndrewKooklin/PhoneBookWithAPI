using PhoneBookWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWPF.HelpMethods
{
    public class DeleteClient
    {
        GetDBContext getDBContext = new GetDBContext();

        public void DeleteOneClient(Clients client)
        {
            //var db = getDBContext.GetDB();
            //db.Clients.Remove(client);
            //db.SaveChanges();
        }
    }
}
