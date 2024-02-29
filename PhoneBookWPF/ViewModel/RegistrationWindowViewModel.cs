using PhoneBookWPF.Commands;
using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.Model;
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
    public class RegistrationWindowViewModel : BaseViewModel
    {
        private string _inputLabelContent = "";
        public string InputLabelContent
        {
            get
            {
                return _inputLabelContent;
            }
            set
            {
                _inputLabelContent = value;
                OnPropertyChanged(nameof(InputLabelContent));
            }
        }

        private string _ckeckMatchPasswordsLabelContent = "";
        public string CkeckMatchPasswordsLabelContent
        {
            get
            {
                return _ckeckMatchPasswordsLabelContent;
            }
            set
            {
                _ckeckMatchPasswordsLabelContent = value;
                OnPropertyChanged(nameof(CkeckMatchPasswordsLabelContent));
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
                if(_ckeckRememberMe != value)
                {
                    _ckeckRememberMe = false;
                }
                if(_ckeckRememberMe == value)
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
            PasswordBox passwordBox = (PasswordBox)values[1];
            string passwordValue = passwordBox.Password;
            PasswordBox confirmPassword = (PasswordBox)values[2];
            string confirmPasswordValue = confirmPassword.Password;
            
            if (userName == null || String.IsNullOrEmpty(userName) || String.IsNullOrWhiteSpace(userName) ||
                userName.Length < 3 || passwordValue.Length < 3 ||
                String.IsNullOrWhiteSpace(passwordValue) || String.IsNullOrEmpty(passwordValue))
            {
                InputLabelContent = "Имя и пароль не менее 3 символов !";
                CkeckMatchPasswordsLabelContent = "";
                return false;
            }
            if (!passwordValue.Equals(confirmPasswordValue))
            {
                CkeckMatchPasswordsLabelContent = "Пароли не совпадают !";
                return false;
            }
            else
            {
                InputLabelContent = "";
                CkeckMatchPasswordsLabelContent = "";
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
            PasswordBox confirmPassword = (PasswordBox)values[2];
            string confirmPasswordValue = confirmPassword.Password;

            CheckUserToDataBase checkUserToDB = new CheckUserToDataBase();

            if (checkUserToDB.CheckUser(userNameValue, passwordValue))
            {
                MessageBox.Show("Пользователь уже существует", "Registration", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!passwordValue.Equals(confirmPasswordValue))
            {
                CkeckMatchPasswordsLabelContent = "Пароли не совпадают !";
                return;
            }
            else
            {
                User user = new User(userNameValue, passwordValue);
                AddNewUser addUser = new AddNewUser();
                addUser.Add(user);

                if (CkeckRememberMe)
                {
                    PhoneBookWPF.Properties.Settings.Default.UserName = userNameValue;
                    PhoneBookWPF.Properties.Settings.Default.Password = passwordValue;
                    PhoneBookWPF.Properties.Settings.Default.Save();
                }
                CkeckMatchPasswordsLabelContent = "";
                MessageBox.Show("Вы успешно зарегистрировались," +
                                "\nперейдите на форму входа.", "Registration", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
