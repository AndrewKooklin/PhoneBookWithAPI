using Newtonsoft.Json;
using PhoneBookWPF.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhoneBookWPF.HelpMethods
{
    public class Records
    {
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        string urlRequest = "";
        HttpResponseMessage response = new HttpResponseMessage();
        List<PhoneBookRecord> bookRecords = null;
        PhoneBookRecord bookRecord = null;
        private readonly MyHttpClient MyHttp = new MyHttpClient();

        public async Task<List<PhoneBookRecord>> GetRecords()
        {
            using (_httpClient = MyHttp.GetHttpClient())
            {
                urlRequest = $"{url}" + "Home/GetRecords";
                response = _httpClient.GetAsync(urlRequest).GetAwaiter().GetResult();
                string apiResponse = await response.Content.ReadAsStringAsync();
                bookRecords = JsonConvert.DeserializeObject<List<PhoneBookRecord>>(apiResponse);
            }
            return bookRecords;
        }

        public PhoneBookRecord GetRecord(int id)
        {
            using (_httpClient = MyHttp.GetHttpClient())
            {
                urlRequest = $"{url}" + "Details/GetRecordById/" + $"{id}";
                string json = _httpClient.GetStringAsync(urlRequest).Result;
                bookRecord = JsonConvert.DeserializeObject<PhoneBookRecord>(json);
            }
            return bookRecord;
        }
    }
}
