using Newtonsoft.Json;
using PhoneBookWPF.Commands;
using PhoneBookWPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.ViewModel
{
    public class RegistrationWindowViewModel : BaseViewModel
    {
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        string urlRequest = "";
        HttpResponseMessage response = new HttpResponseMessage();
        bool result;

        private StringBuilder _errorInputLabelContent = new StringBuilder();
        
        public StringBuilder ErrorInputLabelContent
        {
            get
            {
                return _errorInputLabelContent;
            }
            set
            {
                _errorInputLabelContent = value;
                OnPropertyChanged(nameof(ErrorInputLabelContent));
            }
        }

        private string _errorRegistrationLabelContent = "";

        public string ErrorRegistrationLabelContent
        {
            get
            {
                return _errorRegistrationLabelContent;
            }
            set
            {
                _errorRegistrationLabelContent = value;
                OnPropertyChanged(nameof(ErrorRegistrationLabelContent));
            }
        }

        private bool _ckeckRememberMe = false;
        public bool CkeckRememberMe
        {
            get
            {
                return _ckeckRememberMe;
            }
            set
            {
                if (_ckeckRememberMe != value)
                {
                    _ckeckRememberMe = false;
                }
                if (_ckeckRememberMe == value)
                {
                    return;
                }
                _ckeckRememberMe = value;
                OnPropertyChanged(nameof(CkeckRememberMe));
            }
        }

        public ICommand RedirectToLogInCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        public RegistrationWindowViewModel()
        {
            RedirectToLogInCommand = new RedirectToLogInCommand();
            RegisterCommand = new RelayCommand(Execute, CanExecute);
        }

        private bool CanExecute(object param)
        {
            if (param == null)
            {
                return false;
            }
            var values = (object[])param;
            string userName = values[0].ToString();
            string email = values[1].ToString();
            PasswordBox passwordBox = (PasswordBox)values[3];
            string passwordValue = passwordBox.Password;
            PasswordBox confirmPassword = (PasswordBox)values[4];
            string confirmPasswordValue = confirmPassword.Password;

            if (String.IsNullOrEmpty(userName) || userName.Length < 3)
            {
                ErrorInputLabelContent.Append("Имя не менее 3 символов!");
                ErrorRegistrationLabelContent = "";
                return false;
            }
            if (String.IsNullOrEmpty(email) || new EmailAddressAttribute().IsValid(email))
            {
                ErrorInputLabelContent.Append("\nEMail формата name@site.com!");
                ErrorRegistrationLabelContent = "";
                return false;
            }
            if (String.IsNullOrEmpty(passwordValue) || passwordValue.Length < 6)
            {
                ErrorInputLabelContent.Append("\nПароль не менее 6 символов!");
                return false;
            }
            if (!passwordValue.Equals(confirmPasswordValue))
            {
                ErrorInputLabelContent.Append("\nПароли не совпадают!");
                return false;
            }
            else
            {
                ErrorInputLabelContent.Clear();
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
            string email = values[0].ToString();
            PasswordBox passwordBox = (PasswordBox)values[1];
            string passwordValue = passwordBox.Password;
            PasswordBox confirmPassword = (PasswordBox)values[2];
            string confirmPasswordValue = confirmPassword.Password;

            RegisterModel model = new RegisterModel();

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            urlRequest = $"{url}" + "Register/AddUser/";

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
                ErrorRegistrationLabelContent = "Ошибка, проверьте работу" +
                                                "\nAPI сервера!";
                return;
            }
            else
            {
                ErrorRegistrationLabelContent = "Вы успешно зарегистрировались," +
                                                "\nперейдите на форму входа!";
            }
        }
    }
}
