using PhoneBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PhoneBook.Domain.Repositories.Abstract
{
    public interface IPhoneBookRecordRepository
    {
        Task<List<PhoneBookRecord>> GetPhoneBookRecords();

        PhoneBookRecord GetPhoneBookRecordById(int id);

        Task<bool> SavePhoneBookRecord(PhoneBookRecord phoneBookRecord);

        void EditPhoneBookRecord(PhoneBookRecord phoneBookRecord);

        void DeletePhoneBookRecord(int id);
    }
}
