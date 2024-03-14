using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PhoneBook.Domain.Repositories.Abstract;
using PhoneBook.Views.Login;
using PhoneBook.Views.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PhoneBook.Domain.Repositories.API
{
    public class APIAccountRepository : IAccountRepository
    {
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        string urlRequest = "";
        HttpResponseMessage response;
        string result;
        bool apiResponseConvert;

        public APIAccountRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> CheckUserToDB(LoginModel model)
        {
            urlRequest = $"{url}" + "Login/CheckUserToDB/" + $"{model}";
            using (response = await _httpClient.PostAsJsonAsync(urlRequest, model))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                apiResponseConvert = JsonConvert.DeserializeObject<bool>(apiResponse);
            }
            return apiResponseConvert;
        }

        public Task<bool> CreateUser(RegisterModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetRoles(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> GetUser(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public void LogoutUser()
        {
            throw new NotImplementedException();
        }
    }
}
