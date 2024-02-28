using Microsoft.AspNetCore.Identity;
using PhoneBookAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookAPI.Domain.Repositories.Abstract
{
    public interface IPhoneBookRecordRepositoryAPI
    {
        IEnumerable<PhoneBookRecord> GetPhoneBookRecordsFromAPI();

        PhoneBookRecord GetPhoneBookRecordById(int id);

        void SavePhoneBookRecord(PhoneBookRecord phoneBookRecord);

        void EditPhoneBookRecord(PhoneBookRecord phoneBookRecord);

        void DeletePhoneBookRecord(int id);
    }
}
