using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhoneBookWPF.HelpMethods
{
    public class Roles
    {
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        string urlRequest = "";
        HttpResponseMessage response = new HttpResponseMessage();
        List<IdentityRole> roles = null;
        private readonly MyHttpClient MyHttp = new MyHttpClient();

        public async Task<List<IdentityRole>> GetRoles()
        {
            using (_httpClient = MyHttp.GetHttpClient())
            {
                urlRequest = $"{url}" + "RolesAPI/GetRoles";
                response = _httpClient.GetAsync(urlRequest).GetAwaiter().GetResult();
                string apiResponse = await response.Content.ReadAsStringAsync();
                roles = JsonConvert.DeserializeObject<List<IdentityRole>>(apiResponse);
            }
            return roles;
        }
    }
}
