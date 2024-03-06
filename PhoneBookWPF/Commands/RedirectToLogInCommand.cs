using PhoneBookWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class RedirectToLogInCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is RegistrationWindow)
            {
                var registrationWindow = (RegistrationWindow)parameter;
                registrationWindow.Hide();
                App.Current.MainWindow.Show();
            }
            else
            {
                return;
            }
        }
    }
}
