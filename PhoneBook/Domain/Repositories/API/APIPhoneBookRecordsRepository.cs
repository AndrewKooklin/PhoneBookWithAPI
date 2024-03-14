using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Repositories.Abstract;

namespace PhoneBook.Domain.Repositories.API
{
    public class APIPhoneBookRecordsRepository : IPhoneBookRecordRepository
    {
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        string urlRequest = "";
        HttpResponseMessage response;
        string result;
        bool apiResponseConvert;

        public APIPhoneBookRecordsRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<PhoneBookRecord>> GetPhoneBookRecords()
        {
            urlRequest = $"{url}" + "Home/GetRecords";
            List<PhoneBookRecord> records = new List<PhoneBookRecord>();
            HttpResponseMessage response = null;

            using (_httpClient)
            {
                using (response = await _httpClient.GetAsync(urlRequest))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    records = JsonConvert.DeserializeObject<List<PhoneBookRecord>>(apiResponse);
                }
            }
            return records;
            
            //IEnumerable<PhoneBookRecord> records = null;
            //HttpResponseMessage response = _httpClient.GetAsync(urlRequest).GetAwaiter().GetResult();

            //if (response.IsSuccessStatusCode)
            //{
            //    records = response.Content.ReadFromJsonAsync<IEnumerable<PhoneBookRecord>>().GetAwaiter().GetResult();
            //}
            //return records;
        }

        public PhoneBookRecord GetPhoneBookRecordById(int id)
        {
            urlRequest = $"{url}"+"Details/GetRecordById/"+$"{id}";

            string json = _httpClient.GetStringAsync(urlRequest).Result;

            return JsonConvert.DeserializeObject<PhoneBookRecord>(json);
        }

        public async Task<bool> SavePhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            urlRequest = $"{url}" + "CreateRecord/CreateRecord/" + $"{phoneBookRecord}";
            using (response = await _httpClient.PostAsJsonAsync(urlRequest, phoneBookRecord))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                apiResponseConvert = JsonConvert.DeserializeObject<bool>(apiResponse);
            }
            return apiResponseConvert;
        }

        public async void DeletePhoneBookRecord(int id)
        {
            urlRequest = $"{url}" + "DeleteRecord/DeleteRecord/"+$"{id}";

            response = await _httpClient.DeleteAsync(urlRequest);
        }

        public async void EditPhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            urlRequest = $"{url}" + "EditRecord/EditRecord/";

            response = await _httpClient.PostAsJsonAsync(urlRequest, phoneBookRecord);
        }
    }
}
