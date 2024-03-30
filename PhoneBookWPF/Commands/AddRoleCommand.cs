using Microsoft.AspNet.Identity.EntityFramework;
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
    public class AddRoleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        private string urlRequest = "";
        private HttpResponseMessage response = new HttpResponseMessage();
        private bool apiResponseConvert;
        private Roles roles = new Roles();
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
            var checkInput = checkInputFields.CheckFieldsRole(App.ActionsWithRoleView, parameter);
            if (!checkInput)
            {
                return;
            }
            else
            {
                var fieldElements = (object[])parameter;
                string roleId = fieldElements[0].ToString();
                string roleName = fieldElements[1].ToString();

                IdentityRole role = new IdentityRole
                {
                    Name = roleName
                };

                urlRequest = $"{url}" + "RolesAPI/CreateRole/" + $"{role}";
                using (_httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (response = await _httpClient.PostAsJsonAsync(urlRequest, role))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        apiResponseConvert = JsonConvert.DeserializeObject<bool>(apiResponse);
                    }
                }

                if (!apiResponseConvert)
                {
                    App.ActionsWithRecordView.tbResult.Text = "Роль уже существует!";
                    return;
                }
                else
                {
                    App.RolesView.lbRoles.ItemsSource = null;
                    App.RolesView.lbRoles.ItemsSource = roles.GetRoles().GetAwaiter().GetResult();
                    App.ActionsRoleUserView.cbRoles.ItemsSource = null;
                    App.ActionsRoleUserView.cbRoles.ItemsSource = roles.GetRoles().GetAwaiter().GetResult();
                    App.ActionsWithRoleView.tbErrorRoleName.Text = "";
                    App.ActionsWithRoleView.tbResult.Text = "Роль добавлена!";
                }
            }
        }
    }
}
