using Newtonsoft.Json;
using PhoneBookWPF.HelpMethods;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class DeleteRoleCommand : ICommand
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
                TextBox tbRoleId = (TextBox)fieldElements[0];
                string roleId = tbRoleId.Text;

                if (String.IsNullOrEmpty(roleId))
                {
                    App.ActionsWithRoleView.tbResult.Text = "Выберите роль!";
                    return;
                }
                else
                {
                    App.ActionsWithRoleView.tbResult.Text = "";
                }

                urlRequest = $"{url}" + "RolesAPI/DeleteRole/" + $"{roleId}";
                using (_httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (response = await _httpClient.PostAsJsonAsync(urlRequest, roleId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        apiResponseConvert = JsonConvert.DeserializeObject<bool>(apiResponse);
                    }
                }

                if (!apiResponseConvert)
                {
                    App.ActionsWithRoleView.tbResult.Text = "Ошибка, проверьте работу" +
                                                              "\nAPI сервера!";
                    return;
                }
                else
                {
                    App.RolesView.lbRoles.ItemsSource = null;
                    App.RolesView.lbRoles.ItemsSource = roles.GetRoles().GetAwaiter().GetResult();
                    App.ActionsRoleUserView.cbRoles.ItemsSource = null;
                    App.ActionsRoleUserView.cbRoles.ItemsSource = roles.GetRoles().GetAwaiter().GetResult();
                    App.ActionsWithRoleView.tbResult.Text = "Роль удалена!";
                }
            }
        }
    }
}
