using PhoneBookAPI.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookAPI.Domain
{
    public class DataManager
    {
        public IPhoneBookRecordRepositoryAPI PhoneBookRecords { get; set; }

        public IAccountRepositoryAPI Accounts { get; set; }

        public DataManager(IPhoneBookRecordRepositoryAPI phoneBookRecords,
                            IAccountRepositoryAPI accounts)
        {
            PhoneBookRecords = phoneBookRecords;
            Accounts = accounts;
        }
    }
}
