using Microsoft.EntityFrameworkCore;
using PhoneBookAPI.Domain.Entities;
using PhoneBookAPI.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookAPI.Domain.Repositories.EF
{
    public class EFPhoneBookRecordsRepositoryAPI : IPhoneBookRecordRepositoryAPI
    {
        private readonly AppDBContextAPI _context;
        private int countBeforeAdded;
        private int countAfterAdded;

        public EFPhoneBookRecordsRepositoryAPI(AppDBContextAPI context)
        {
            _context = context;
        }

        public List<PhoneBookRecord> GetPhoneBookRecordsFromAPI()
        {
            return _context.PhoneBookRecords.ToList();
        }

        public PhoneBookRecord GetPhoneBookRecordById(int id)
        {
            return _context.PhoneBookRecords.FirstOrDefault(r => r.Id == id);
        }

        public async Task<bool> SavePhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            countBeforeAdded = await _context.PhoneBookRecords.CountAsync();

            if (phoneBookRecord.Id == default)
            {
                //_context.Entry(phoneBookRecord).State = Microsoft.EntityFrameworkCore.EntityState.Added;
               await _context.PhoneBookRecords.AddAsync(phoneBookRecord);
            }
            _context.SaveChanges();
            _context.Dispose();
            countAfterAdded = await _context.PhoneBookRecords.CountAsync();
            if (countAfterAdded > countBeforeAdded)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public void DeletePhoneBookRecord(int id)
        {
            _context.PhoneBookRecords.Remove(new PhoneBookRecord{Id = id });
            _context.SaveChanges();
        }

        public void EditPhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            _context.Entry(phoneBookRecord).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.PhoneBookRecords.Update(phoneBookRecord);
            _context.SaveChanges();
        }


    }
}
