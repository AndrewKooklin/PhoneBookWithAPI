using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Repositories.Abstract;

namespace PhoneBook.Domain.Repositories.API
{
    public class APIPhoneBookRecordsRepository : IPhoneBookRecordRepository
    {
        private HttpClient httpClient { get; set; }

        public APIPhoneBookRecordsRepository()
        {
            httpClient = new HttpClient();
        }

        public IEnumerable<PhoneBookRecord> GetPhoneBookRecords()
        {
            string url = @"https://localhost:44379/api/Home/GetRecords";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<IEnumerable<PhoneBookRecord>>(json);
        }

        public PhoneBookRecord GetPhoneBookRecordById(int id)
        {
            string url = $"https://localhost:44379/api/Details/GetRecordById/"+id.ToString();

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<PhoneBookRecord>(json);
        }

        public void SavePhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            //if (phoneBookRecord.Id == default)
            //{
            //    _context.Entry(phoneBookRecord).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            //    _context.PhoneBookRecords.Add(phoneBookRecord);
            //}
            //_context.SaveChanges();
        }

        public void DeletePhoneBookRecord(int id)
        {
            //_context.PhoneBookRecords.Remove(new PhoneBookRecord { Id = id });
            //_context.SaveChanges();
        }

        public void EditPhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            //_context.Entry(phoneBookRecord).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //// _context.PhoneBookRecords.Update(phoneBookRecord);
            //_context.SaveChanges();
        }
    }
}
