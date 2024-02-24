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

        public DataManager(IPhoneBookRecordRepository phoneBookRecords)
        {
            PhoneBookRecords = phoneBookRecords;
        }
    }
}
