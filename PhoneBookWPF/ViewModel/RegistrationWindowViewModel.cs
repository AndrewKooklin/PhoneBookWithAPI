using Newtonsoft.Json;
using PhoneBookWPF.Commands;
using PhoneBookWPF.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mail;
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

        public string ErrorInputConfirmPassword
        {
            get
            {
                return _errorInputConfirmPasswordContent;
            }
            set
            {
                _errorInputConfirmPasswordContent = value;
                OnPropertyChanged(nameof(ErrorInputConfirmPassword));
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

        private bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            Grid gReg = (Grid)parameter;
            var gRegChildren = gReg.Children;
            TextBox tbemail = (TextBox)gRegChildren[7];
            string emailValue = tbemail.Text;
            PasswordBox passwordBox = (PasswordBox)gRegChildren[8];
            string passwordValue = passwordBox.Password;
            PasswordBox confirmPassword = (PasswordBox)gRegChildren[9];
            string confirmPasswordValue = confirmPassword.Password;

            if (String.IsNullOrEmpty(emailValue))
            {
                ErrorInputEMailContent = "Заполните поле \"EMail!\"";
                return false;
            }
            else if (!String.IsNullOrEmpty(emailValue))
            {
                try
                {
                    var mailAddress = new MailAddress(emailValue);
                }
                catch
                {
                    ErrorInputEMailContent = "EMail формата name@site.com!";
                    return false;
                }
                ErrorInputEMailContent = "";
            }

            if (String.IsNullOrEmpty(passwordValue) || passwordValue.Length < 6)
            {
                ErrorInputPasswordContent = "Пароль не менее 6 символов!";
                return false;
            }
            else
            {
                ErrorInputPasswordContent = "";
            }

            if (!passwordValue.Equals(confirmPasswordValue))
            {
                ErrorInputConfirmPassword = "Пароли не совпадают!";
                return false;
            }
            else
            {
                ErrorInputConfirmPassword = "";
                return true;
            }
        }

        private async void Execute(object parameter)
        {
            if (parameter == null)
            {
                return;
            }
            Grid gReg = (Grid)parameter;
            var gRegChildren = gReg.Children;
            TextBox tbemail = (TextBox)gRegChildren[7];
            string eMailValue = tbemail.Text;
            PasswordBox passwordBox = (PasswordBox)gRegChildren[8];
            string passwordValue = passwordBox.Password;

            RegisterModel model = new RegisterModel();
            model.Password = passwordValue;
            model.Email = eMailValue;
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

            if (CkeckRememberMe)
            {
                PhoneBookWPF.Properties.Settings.Default.EMail = eMailValue;
                PhoneBookWPF.Properties.Settings.Default.Password = passwordValue;
                PhoneBookWPF.Properties.Settings.Default.Save();
            }
        }
    }
}
