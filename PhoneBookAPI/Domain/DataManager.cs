using PhoneBookAPI.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookAPI.Domain
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
