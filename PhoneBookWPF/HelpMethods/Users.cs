using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using PhoneBookWPF.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhoneBookWPF.HelpMethods
{
    public class Users
    {
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        string urlRequest = "";
        HttpResponseMessage response = new HttpResponseMessage();
        List<IdentityUser> users = null;
        List<UserWithRolesModel> usersWithRoles = null;
        private readonly MyHttpClient MyHttp = new MyHttpClient();

        public async Task<List<IdentityUser>> GetUsers()
        {
            using (_httpClient = MyHttp.GetHttpClient())
            {
                urlRequest = $"{url}" + "UsersAPI/GetUsers";
                response = _httpClient.GetAsync(urlRequest).GetAwaiter().GetResult();
                string apiResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<IdentityUser>>(apiResponse);
            }
            return users;
        }

        public async Task<List<UserWithRolesModel>> GetUsersWithRoles()
        {
            using (_httpClient = MyHttp.GetHttpClient())
            {
                urlRequest = $"{url}" + "UsersAPI/GetUsersWithRoles";
                response = _httpClient.GetAsync(urlRequest).GetAwaiter().GetResult();
                string apiResponse = await response.Content.ReadAsStringAsync();
                usersWithRoles = JsonConvert.DeserializeObject<List<UserWithRolesModel>>(apiResponse);
            }
            return usersWithRoles;
        }
    }
}
