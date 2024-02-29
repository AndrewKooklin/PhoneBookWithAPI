using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using PhoneBookWPF.Commands;
using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.View;
using Microsoft.AspNetCore.Identity;

namespace PhoneBookWPF.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
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

        private string _labelErrorUserNameContent = "";
        public string LabelErrorUserNameContent
        {
            get
            {
                return _labelErrorUserNameContent;
            }
            set
            {
                _labelErrorUserNameContent = value;
                OnPropertyChanged(nameof(LabelErrorUserNameContent));
            }
        }

        private string _labelErrorPasswordContent = "";
        public string LabelErrorPasswordContent
        {
            get
            {
                return _labelErrorPasswordContent;
            }
            set
            {
                _labelErrorPasswordContent = value;
                OnPropertyChanged(nameof(LabelErrorPasswordContent));
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
                LabelErrorUserNameContent = "Имя не менее 3 символов!";
                CheckUserLabelContent = "";
                return false;
            }
            if (String.IsNullOrEmpty(passwordValue) || passwordValue.Length < 6)
            {
                LabelErrorPasswordContent = "Пароль не менее 6 символов!";
                CheckUserLabelContent = "";
                return false;
            }
            else
            {
                LabelErrorUserNameContent = "";
                LabelErrorPasswordContent = "";
                return true;
            }
        }

        private void Execute(object param)
        {
            if (param == null)
            {
                return;
            }
            var values = (object[])param;
            string userNameValue = values[0].ToString();
            PasswordBox passwordBox = (PasswordBox)values[1];
            string passwordValue = passwordBox.Password;



            CheckUserToDataBase checkUserToDB = new CheckUserToDataBase();

            if(!checkUserToDB.CheckUser(userNameValue, passwordValue))
            {
                CheckUserLabelContent = "Пользователь не найден, проверьте\nимя и пароль или зарегистрируйтесь !";
                return;
            }
            else
            {
                CheckUserLabelContent = "";
                Application.Current.MainWindow.Hide();
                App.clientsWindow = new ClientsWindow();
                //App.productsWindow = new ProductsWindow();
                App.clientsWindow.Show();
            }
        }
    }
}
