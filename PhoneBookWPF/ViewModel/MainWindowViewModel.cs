using Newtonsoft.Json;
using PhoneBookWPF.Commands;
using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
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
            if (String.IsNullOrEmpty(eMailValue))
            {
                ErrorEMailBoxLabel = "Заполните поле \"EMail!\"";
                return false;
            }
            else if (!String.IsNullOrEmpty(eMailValue))
            {
                try
                {
                    var mailAddress = new MailAddress(eMailValue);
                }
                catch
                {
                    ErrorEMailBoxLabel = "EMail формата name@site.com!";
                    return false;
                }
                ErrorEMailBoxLabel = "";
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
                Email = eMailValue,
                Password = passwordValue
            };

            urlRequest = $"{url}" + "Login/GetUserRoles/" + $"{model}";

            using (var client = MyHttp.GetHttpClient())
            {
                using (response = await client.PostAsJsonAsync(urlRequest, model))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userRoles = JsonConvert.DeserializeObject<List<string>>(apiResponse);
                }
            }

            CheckUserLabelContent = "";
            var bookWindow = App.PhoneBookWindow;
            bookWindow.ccRightPartPage = null;

            if (userRoles == null)
            {
                CheckUserLabelContent = "Пользователь не найден, проверьте" +
                                        "\nимя и пароль или зарегистрируйтесь !";
                return;
            }
            else if (userRoles.Contains("Admin"))
            {
                if (App.MainWindow.IsInitialized)
                {
                    App.MainWindow.Close();
                }
                if (App.RegistrationWindow.IsInitialized)
                {
                    App.RegistrationWindow.Close();
                }
                bookWindow.miAddRecord.Visibility = Visibility.Visible;
                bookWindow.miChangeRecord.Visibility = Visibility.Visible;
                bookWindow.miDeleteRecord.Visibility = Visibility.Visible;
                bookWindow.miUsers.Visibility = Visibility.Visible;
                bookWindow.miRoles.Visibility = Visibility.Visible;
                bookWindow.miRegister.Visibility = Visibility.Collapsed;
                bookWindow.miLogIn.Visibility = Visibility.Collapsed;
                bookWindow.miUserName.Visibility = Visibility.Visible;
                bookWindow.miUserName.Header = $"Hello {model.Email}";
                bookWindow.miLogOut.Visibility = Visibility.Visible;

                App.ActionsWithRecordView.Visibility = Visibility.Visible;
                App.ActionsWithRecordView.bAddRecord.Visibility = Visibility.Visible;
                App.ActionsWithRecordView.bChangeRecord.Visibility = Visibility.Visible;
                App.ActionsWithRecordView.bDeleteRecord.Visibility = Visibility.Visible;
                App.ActionsWithRecordView.bClearForm.Visibility = Visibility.Visible;
                App.ActionAddUserView.Visibility = Visibility.Visible;
                App.ActionDeleteUserView.Visibility = Visibility.Visible;
                App.ActionsRoleUserView.Visibility = Visibility.Visible;
                App.ActionsWithRoleView.Visibility = Visibility.Visible;
                App.UsersView.Visibility = Visibility.Visible;
                App.RolesView.Visibility = Visibility.Visible;
            }
            else if (!userRoles.Contains("Admin") && userRoles.Contains("User"))
            {
                if (App.MainWindow.IsInitialized)
                {
                    App.MainWindow.Close();
                }
                if (App.RegistrationWindow.IsInitialized)
                {
                    App.RegistrationWindow.Close();
                }

                bookWindow.miRegister.Visibility = Visibility.Collapsed;
                bookWindow.miLogIn.Visibility = Visibility.Collapsed;
                bookWindow.miUserName.Visibility = Visibility.Visible;
                bookWindow.miUserName.Header = $"Hello {model.Email}";
                bookWindow.miLogOut.Visibility = Visibility.Visible;
                bookWindow.miAddRecord.Visibility = Visibility.Visible;
                App.ActionsWithRecordView.bAddRecord.Visibility = Visibility.Visible;
            }
            else
            {
                if (App.MainWindow.IsInitialized)
                {
                    App.MainWindow.Close();
                }
                if (App.RegistrationWindow.IsInitialized)
                {
                    App.RegistrationWindow.Close();
                }

                bookWindow.miRegister.Visibility = Visibility.Collapsed;
                bookWindow.miLogIn.Visibility = Visibility.Collapsed;
                bookWindow.miUserName.Visibility = Visibility.Visible;
                bookWindow.miUserName.Header = $"Hello {model.Email}";
                bookWindow.miLogOut.Visibility = Visibility.Visible;
            }

            bookWindow.Activate();
            bookWindow.Focus();
        }
    }
}

