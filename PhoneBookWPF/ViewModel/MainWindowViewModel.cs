using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using PhoneBookWPF.Commands;
using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.Model;
using PhoneBookWPF.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        MyHttpClient MyHttp = new MyHttpClient();
        private string url = @"https://localhost:44379/api/";
        string urlRequest = "";
        HttpResponseMessage response = new HttpResponseMessage();
        bool userExist;
        IdentityUser user;
        List<string> userRoles = new List<string>();

        private string eMail;
        public string EMail
        {
            get
            {
                return eMail;
            }
            set
            {
                eMail = value;
                OnPropertyChanged(nameof(EMail));
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _errorEMAilBoxLabel = "";
        public string ErrorEMailBoxLabel
        {
            get
            {
                return _errorEMAilBoxLabel;
            }
            set
            {
                _errorEMAilBoxLabel = value;
                OnPropertyChanged(nameof(ErrorEMailBoxLabel));
            }
        }

        private string _errorPasswordBoxLabel = "";

        public string ErrorPasswordBoxLabel
        {
            get
            {
                return _errorPasswordBoxLabel;
            }
            set
            {
                _errorPasswordBoxLabel = value;
                OnPropertyChanged(nameof(ErrorPasswordBoxLabel));
            }
        }

        private string _ckeckUserLabelContent = "";
        public string CheckUserLabelContent
        {
            get
            {
                return _ckeckUserLabelContent;
            }
            set
            {
                _ckeckUserLabelContent = value;
                OnPropertyChanged(nameof(CheckUserLabelContent));
            }
        }

        public MainWindowViewModel()
        {
            LogInCommand = new RelayCommand(Execute, CanExecute);
            RedirectRegistrationCommand = new RedirectRegistrationCommand();
        }

        public ICommand LogInCommand { get; set; }

        public ICommand RedirectRegistrationCommand { get; set; }

        private bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }

            var values = (object[])parameter;
            string eMailValue = values[0].ToString();
            PasswordBox passwordBox = (PasswordBox)values[1];
            string passwordValue = passwordBox.Password;

            string _eMail = PhoneBookWPF.Properties.Settings.Default.EMail;
            string _password = PhoneBookWPF.Properties.Settings.Default.Password;

            if (eMailValue.Equals(_eMail) && passwordValue.Equals(_password))
            {
                return true;
            }
            if (string.IsNullOrEmpty(eMailValue) || eMailValue.Length < 3)
            {
                ErrorEMailBoxLabel = "Имя не менее 3 символов !";
                CheckUserLabelContent = "";
                return false;
            }
            if (String.IsNullOrEmpty(passwordValue) || passwordValue.Length < 6)
            {
                ErrorPasswordBoxLabel = "Пароль не менее 6 символов !";
                CheckUserLabelContent = "";
                return false;
            }
            else
            {
                ErrorEMailBoxLabel = "";
                ErrorPasswordBoxLabel = "";
                return true;
            }
        }

        private async void Execute(object param)
        {
            if (param == null)
            {
                return;
            }

            var values = (object[])param;
            string eMailValue = values[0].ToString();
            PasswordBox passwordBox = (PasswordBox)values[1];
            string passwordValue = passwordBox.Password;

            LoginModel model = new LoginModel
            {
                EMail = eMailValue,
                Password = passwordValue
            };

            urlRequest = $"{url}" + "Login/CheckUserToDB/";

            using (var client = MyHttp.GetHttpClient())
            {
                using (response = await client.PostAsJsonAsync(urlRequest, model))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userExist = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }

            if (!userExist)
            {
                CheckUserLabelContent = "Пользователь не найден, проверьте" +
                                        "\nимя и пароль или зарегистрируйтесь !";
                return;
            }

            urlRequest = $"{url}" + "Login/GetUserFromDB/";

            using (var client = MyHttp.GetHttpClient())
            {
                using (response = await client.PostAsJsonAsync(urlRequest, model))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<IdentityUser>(apiResponse);
                }
            }

            urlRequest = $"{url}" + "Login/GetUserRoles/";

            using (var client = MyHttp.GetHttpClient())
            {
                using (response = await client.PostAsJsonAsync(urlRequest, user))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userRoles = JsonConvert.DeserializeObject<List<string>>(apiResponse);
                }
            }

            CheckUserLabelContent = "";
            PhoneBookWindow bookWindow = new PhoneBookWindow();
            if (userRoles.Contains("Admin"))
            {

            }
            else if (!userRoles.Contains("Admin") && userRoles.Contains("User"))
            {

            }
            else
            {

            }
            
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).Hide();
            bookWindow.Show();
        }
    }
}
