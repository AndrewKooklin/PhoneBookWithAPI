using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PhoneBookWPF.Commands;
using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.Model;
using PhoneBookWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        string urlRequest = "";
        HttpResponseMessage response = new HttpResponseMessage();
        bool result;

        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
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

        private string _errorUserNameBoxLabel = "";
        public string ErrorUserNameBoxLabel
        {
            get
            {
                return _errorUserNameBoxLabel;
            }
            set
            {
                _errorUserNameBoxLabel = value;
                OnPropertyChanged(nameof(ErrorUserNameBoxLabel));
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
            //RedirectRegistrationCommand = new RedirectRegistrationCommand();
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
            string userName = values[0].ToString();
            PasswordBox passwordBox = (PasswordBox)values[1];
            string passwordValue = passwordBox.Password;

            string _userName = PhoneBookWPF.Properties.Settings.Default.UserName;
            string _password = PhoneBookWPF.Properties.Settings.Default.Password;

            if (userName.Equals(_userName) && passwordValue.Equals(_password))
            {
                return true;
            }
            if (String.IsNullOrEmpty(userName) || userName.Length < 3)
            {
                ErrorUserNameBoxLabel = "Имя не менее 3 символов !";
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
                ErrorUserNameBoxLabel = "";
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
            string userNameValue = values[0].ToString();
            PasswordBox passwordBox = (PasswordBox)values[1];
            string passwordValue = passwordBox.Password;

            LoginModel model = new LoginModel();
            LoginModel.InputModel input = new LoginModel.InputModel();
            model.Input = input;
            model.ReturnUrl = "";
            model.ErrorMessage = "";
            input.UserName = userNameValue;
            input.Password = passwordValue;
            input.RememberMe = false; ;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            urlRequest = $"{url}" + "Login/CheckUserToDB/";

            using (_httpClient)
            {
                using (response = await _httpClient.PostAsJsonAsync(urlRequest, model))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }

            if (!result)
            {
                CheckUserLabelContent = "Пользователь не найден, проверьте" +
                                        "\nимя и пароль или зарегистрируйтесь !";
                return;
            }
            else
            {
                CheckUserLabelContent = "";
                PhoneBookWindow bookWindow = new PhoneBookWindow();
                (Window.GetWindow(App.Current.MainWindow) as MainWindow).Hide();
                bookWindow.Show();
            }
        }
    }
}
