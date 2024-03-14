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

        public DataManager(IPhoneBookRecordRepository phoneBookRecords,
                            IAccountRepository accounts)
        {
            PhoneBookRecords = phoneBookRecords;
            Accounts = accounts;
        }
    }
}
