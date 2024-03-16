using Microsoft.AspNetCore.Identity;
using PhoneBook.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Domain
{
    public class DataManager
    {
        public IPhoneBookRecordRepository PhoneBookRecords { get; set; }

        public IAccountRepository Accounts { get; set; }

        //public SignInManager<IdentityUser> SignInManager { get; set; }

        //public UserManager<IdentityUser> UserManager { get; set; }

        //public RoleManager<IdentityRole> RoleManager { get; set; }

        public DataManager(IPhoneBookRecordRepository phoneBookRecords,
                           IAccountRepository accounts)
        {
            PhoneBookRecords = phoneBookRecords;
            Accounts = accounts;
            
            //IdentityUser user = UserManager.Users.Select(u => u.Email == email);
            //SignInManager.IsSignedIn(SignInManager.Context.User);
        }
    }
}
