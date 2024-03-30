using Newtonsoft.Json;
using PhoneBookWEB.Domain.Entities;
using PhoneBookWEB.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PhoneBookWEB.Domain.Repositories.API
{
    public class APIPhoneBookRecordsRepository : IPhoneBookRecordRepository
    {
        private HttpClient _httpClient;
        private string url = @"https://localhost:44379/api/";
        string urlRequest = "";
        HttpResponseMessage response;
        string result;
        bool apiResponseConvert;
        private PhoneBookRecord bookRecord;

        public APIPhoneBookRecordsRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<PhoneBookRecord>> GetPhoneBookRecords()
        {
            urlRequest = $"{url}" + "Home/GetRecords";
            List<PhoneBookRecord> records = new List<PhoneBookRecord>();
            HttpResponseMessage response = null;

            using (_httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (response = await _httpClient.GetAsync(urlRequest))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    records = JsonConvert.DeserializeObject<List<PhoneBookRecord>>(apiResponse);
                }
            }
            return records;
        }

        public PhoneBookRecord GetPhoneBookRecordById(int id)
        {
            urlRequest = $"{url}" + "Details/GetRecordById/" + $"{id}";

            using(_httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string json = _httpClient.GetStringAsync(urlRequest).Result;
                bookRecord = JsonConvert.DeserializeObject<PhoneBookRecord>(json);
            }

            return bookRecord;
        }

        public async Task<bool> SavePhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            urlRequest = $"{url}" + "CreateRecordAPI/CreateRecord/" + $"{phoneBookRecord}";
            using(_httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (response = await _httpClient.PostAsJsonAsync(urlRequest, phoneBookRecord))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    apiResponseConvert = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }
            
            return apiResponseConvert;
        }

        public async void DeletePhoneBookRecord(int id)
        {
            urlRequest = $"{url}" + "DeleteRecord/DeleteRecord/" + $"{id}";
            using(_httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await _httpClient.DeleteAsync(urlRequest);
            }
        }

        public async void EditPhoneBookRecord(PhoneBookRecord phoneBookRecord)
        {
            urlRequest = $"{url}" + "EditRecord/EditRecord/";

            using(_httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await _httpClient.PostAsJsonAsync(urlRequest, phoneBookRecord);
            }
        }
    }
}
