using PhoneBookWPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            PasswordBox passwordBox = (PasswordBox)values[1];
            string passwordValue = passwordBox.Password;
            PasswordBox confirmPassword = (PasswordBox)values[2];
            string confirmPasswordValue = confirmPassword.Password;

            if (String.IsNullOrEmpty(userName) || userName.Length < 3)
            {
                ErrorInputLabelContent.Append("Имя не менее 3 символов !");
                ErrorRegistrationLabelContent = "";
                return false;
            }
            if (String.IsNullOrEmpty(passwordValue) || passwordValue.Length < 6)
            {
                ErrorInputLabelContent.Append("\nПароль не менее 6 символов !");
                return false;
            }
            if (!passwordValue.Equals(confirmPasswordValue))
            {
                ErrorInputLabelContent.Append("\nПароли не совпадают !");
                return false;
            }
            else
            {
                ErrorInputLabelContent.Clear();
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

            //CheckUserToDataBase checkUserToDB = new CheckUserToDataBase();

            //if (checkUserToDB.CheckUser(userNameValue, passwordValue))
            //{
            //    MessageBox.Show("Пользователь уже существует", "Registration",
            //                    MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            //if (!passwordValue.Equals(confirmPasswordValue))
            //{
            //    CkeckMatchPasswordsLabelContent = "Пароли не совпадают !";
            //    return;
            //}
            //else
            //{
            //    Users user = new Users(userNameValue, passwordValue);
            //    AddNewUser addUser = new AddNewUser();
            //    addUser.Add(user);

            //    if (CkeckRememberMe)
            //    {
            //        StoreWithEF.Properties.Settings.Default.UserName = userNameValue;
            //        StoreWithEF.Properties.Settings.Default.Password = passwordValue;
            //        StoreWithEF.Properties.Settings.Default.Save();
            //    }
            //    CkeckMatchPasswordsLabelContent = "";
            //    MessageBox.Show("Вы успешно зарегистрировались," +
            //                    "\nперейдите на форму входа.", "Registration",
            //                    MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }
    }
}
