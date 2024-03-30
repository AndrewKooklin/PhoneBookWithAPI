using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class AddRoleUserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        private string urlRequest = "";
        private HttpResponseMessage response = new HttpResponseMessage();
        private bool apiResponseConvert;
        private Users users = new Users();
        private UserWithRolesModel userWithRoles;
        private RoleUserModel userModel;
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
            var checkInput = checkInputFields.CheckFieldsAddRoleUser(App.ActionsRoleUserView, parameter);
            if (!checkInput)
            {
                return;
            }
            else
            {
                var fieldElements = (object[])parameter;
                string userId = fieldElements[0].ToString();
                ComboBox cbRole = (ComboBox)fieldElements[1];
                IdentityRole role = (IdentityRole)cbRole.SelectedItem;
                string roleName = role.Name;


                urlRequest = $"{url}" + "UsersAPI/GetUserWithRoles/" + $"{userId}";
                using (_httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string result = _httpClient.GetStringAsync(urlRequest).Result;
                    userWithRoles = JsonConvert.DeserializeObject<UserWithRolesModel>(result);
                }

                if (userWithRoles == null)
                {
                    App.ActionsRoleUserView.tbResult.Text = "Такого пользователя нет!";
                    return;
                }
                else if (userWithRoles.Roles.Contains(roleName))
                {
                    App.ActionsRoleUserView.tbResult.Text = "Уже есть такая роль!";
                    return;
                }
                else
                {
                    userModel = new RoleUserModel
                    {
                        UserId = userId,
                        Role = roleName
                    };
                    urlRequest = $"{url}" + "UsersAPI/AddRoleToUser/" + $"{userModel}";
                    using (_httpClient = new HttpClient())
                    {
                        _httpClient.DefaultRequestHeaders.Accept.Clear();
                        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        using (response = await _httpClient.PostAsJsonAsync(urlRequest, userModel))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            apiResponseConvert = JsonConvert.DeserializeObject<bool>(apiResponse);
                        }
                    }

                    if (!apiResponseConvert)
                    {
                        App.ActionAddUserView.tbResult.Text = "Ошибка сервера!";
                        return;
                    }
                    else
                    {
                        App.UsersView.lbUsers.ItemsSource = null;
                        App.UsersView.lbUsers.ItemsSource = users.GetUsersWithRoles().GetAwaiter().GetResult();
                        App.ActionsRoleUserView.tbResult.Text = "Роль добавлена!";
                    }
                }
            }
        }
    }
}
