using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Repositories.Abstract;

namespace PhoneBook.Domain.Repositories.API
{
    public class APIPhoneBookRecordsRepository: IPhoneBookRecordRepository
    {
        private readonly AppDBContext _context;

        public APIPhoneBookRecordsRepository(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<PhoneBookRecord> GetPhoneBookRecords()
        {
            return _context.PhoneBookRecords;
        }

        public PhoneBookRecord GetPhoneBookRecordById(int id)
        {
            return _context.PhoneBookRecords.FirstOrDefault(r => r.Id == id);
        }

        public void SavePhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            if (phoneBookRecord.Id == default)
            {
                _context.Entry(phoneBookRecord).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _context.PhoneBookRecords.Add(phoneBookRecord);
            }
            _context.SaveChanges();
        }

        public void DeletePhoneBookRecord(int id)
        {
            _context.PhoneBookRecords.Remove(new PhoneBookRecord { Id = id });
            _context.SaveChanges();
        }

        public void EditPhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            _context.Entry(phoneBookRecord).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            // _context.PhoneBookRecords.Update(phoneBookRecord);
            _context.SaveChanges();
        }
    }
}
