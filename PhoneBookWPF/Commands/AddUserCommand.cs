using Newtonsoft.Json;
using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class AddUserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        private string urlRequest = "";
        private HttpResponseMessage response = new HttpResponseMessage();
        private bool apiResponseConvert;
        private Users users = new Users();
        private CheckInputFields checkInputFields = new CheckInputFields();

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (parameter == null)
            {
                return;
            }
            var checkInput = checkInputFields.CheckFieldsAddUser(App.ActionAddUserView, parameter);
            if (!checkInput)
            {
                return;
            }
            else
            {
                var fieldElements = (object[])parameter;
                string userEmail = fieldElements[0].ToString();
                string userPassword = fieldElements[1].ToString();

                RegisterModel registerModel = new RegisterModel
                {
                    Email = userEmail,
                    Password = userPassword
                };

                urlRequest = $"{url}" + "RegisterAPI/CreateNewUser/" + $"{registerModel}";
                using (_httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (response = await _httpClient.PostAsJsonAsync(urlRequest, registerModel))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        apiResponseConvert = JsonConvert.DeserializeObject<bool>(apiResponse);
                    }
                }

                if (!apiResponseConvert)
                {
                    App.ActionsWithRecordView.tbResult.Text = "Пользователь уже существует!";
                    return;
                }
                else
                {
                    App.UsersView.lbUsers.ItemsSource = null;
                    App.UsersView.lbUsers.ItemsSource = users.GetUsersWithRoles().GetAwaiter().GetResult();
                    App.ActionAddUserView.tbErrorEmail.Text = "";
                    App.ActionAddUserView.tbErrorPassword.Text = "";
                    App.ActionAddUserView.tbResult.Text = "Пользователь добавлен!";
                }
            }
        }
    }
}
