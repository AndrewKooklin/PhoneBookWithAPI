﻿using Microsoft.EntityFrameworkCore;
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

        public EFPhoneBookRecordsRepositoryAPI(AppDBContextAPI context)
        {
            _context = context;
        }

        public IEnumerable<PhoneBookRecord> GetPhoneBookRecordsFromAPI()
        {
            return _context.PhoneBookRecords.AsEnumerable();
        }

        public PhoneBookRecord GetPhoneBookRecordById(int id)
        {
            return _context.PhoneBookRecords.FirstOrDefault(r => r.Id == id);
        }

        public void SavePhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            if (phoneBookRecord.Id == default)
            {
                //_context.Entry(phoneBookRecord).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _context.PhoneBookRecords.Add(phoneBookRecord);
            }
            _context.SaveChanges();
            _context.Dispose();
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