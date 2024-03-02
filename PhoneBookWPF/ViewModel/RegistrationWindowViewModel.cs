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


        private string _errorInputUserNameContent = "";

        public string ErrorInputUserNameContent
        {
            get
            {
                return _errorInputUserNameContent;
            }
            set
            {
                _errorInputUserNameContent = value;
                OnPropertyChanged(nameof(ErrorInputUserNameContent));
            }
        }

        private string _errorInputEMailContent = "";

        public string ErrorInputEMailContent
        {
            get
            {
                return _errorInputEMailContent;
            }
            set
            {
                _errorInputEMailContent = value;
                OnPropertyChanged(nameof(ErrorInputEMailContent));
            }
        }

        private string _errorInputPasswordContent = "";

        public string ErrorInputPasswordContent
        {
            get
            {
                return _errorInputPasswordContent;
            }
            set
            {
                _errorInputPasswordContent = value;
                OnPropertyChanged(nameof(ErrorInputPasswordContent));
            }
        }

        private string _errorInputConfirmPasswordContent = "";

        public string ErrorInputConfirmPasswordContent
        {
            get
            {
                return _errorInputConfirmPasswordContent;
            }
            set
            {
                _errorInputConfirmPasswordContent = value;
                OnPropertyChanged(nameof(ErrorInputConfirmPasswordContent));
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
            PasswordBox passwordBox = (PasswordBox)values[2];
            string passwordValue = passwordBox.Password;
            PasswordBox confirmPassword = (PasswordBox)values[3];
            string confirmPasswordValue = confirmPassword.Password;

            if (String.IsNullOrEmpty(userName) || userName.Length < 3)
            {
                ErrorInputUserNameContent = "Имя не менее 3 символов";
                ErrorRegistrationLabelContent = "";
                return false;
            }
            else if (String.IsNullOrEmpty(email) || new EmailAddressAttribute().IsValid(email))
            {
                ErrorInputEMailContent = "EMail формата name@site.com!";
                return false;
            }
            else if (String.IsNullOrEmpty(passwordValue) || passwordValue.Length < 6)
            {
                ErrorInputPasswordContent = "Пароль не менее 6 символов!";
                return false;
            }
            else if (!passwordValue.Equals(confirmPasswordValue))
            {
                ErrorInputConfirmPasswordContent = "Пароли не совпадают!";
                return false;
            }
            else
            {
                ErrorInputUserNameContent = "";
                ErrorInputEMailContent = "";
                ErrorInputPasswordContent = "";
                ErrorInputConfirmPasswordContent = "";
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
