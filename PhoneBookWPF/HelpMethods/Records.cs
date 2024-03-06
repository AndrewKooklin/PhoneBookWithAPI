using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWPF.HelpMethods
{
    public class Records
    {
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        string urlRequest = "";
        HttpResponseMessage response = new HttpResponseMessage();
        List<PhoneBookRecord> bookRecords = new List<PhoneBookRecord>();

        public async Task<List<PhoneBookRecord>> GetRecords()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            urlRequest = $"{url}" + "Home/GetRecords";

            using (_httpClient)
            {
                using (response = await _httpClient.GetAsync(urlRequest))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bookRecords = JsonConvert.DeserializeObject<List<PhoneBookRecord>>(apiResponse);
                }
            }
            return bookRecords;
        }
    }
}
