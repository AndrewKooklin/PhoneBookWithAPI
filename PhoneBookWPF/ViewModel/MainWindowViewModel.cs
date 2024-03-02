using Microsoft.AspNetCore.Identity;
using PhoneBookWPF.Commands;
using PhoneBookWPF.HelpMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.ViewModel
{
    public class MainWindowViewModel
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
                //OnPropertyChanged(nameof(UserName));
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
                //OnPropertyChanged(nameof(Password));
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
                //OnPropertyChanged(nameof(InputLabelContent));
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
                //OnPropertyChanged(nameof(InputLabelContent));
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
                //OnPropertyChanged(nameof(CheckUserLabelContent));
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


            //SignInManager<IdentityUser> _signInManager = new SignInManager<IdentityUser>(PhoneBookContext.GetContext());

            if (userName.Equals(_userName) && passwordValue.Equals(_password))
            {
                return true;
            }
            if (String.IsNullOrEmpty(userName) ||
                userName.Length < 3 || passwordValue.Length < 3 ||
                 String.IsNullOrEmpty(passwordValue))
            {
                ErrorUserNameBoxLabel = "Имя и пароль не менее 3 символов !";
                CheckUserLabelContent = "";
                return false;
            }
            else
            {
                ErrorUserNameBoxLabel = "";
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

            //CheckUserToDataBase checkUserToDB = new CheckUserToDataBase();

            //if (!checkUserToDB.CheckUser(userNameValue, passwordValue))
            //{
            //    CheckUserLabelContent = "Пользователь не найден, проверьте\nимя и пароль или зарегистрируйтесь !";
            //    return;
            //}
            //else
            //{
            //    CheckUserLabelContent = "";
            //    //Application.Current.MainWindow.Hide();
            //    //App.clientsWindow = new ClientsWindow();
            //    //App.productsWindow = new ProductsWindow();
            //    //App.clientsWindow.Show();
            //}
        }
    }
}
