using PhoneBookWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class RedirectRegistrationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is MainWindow)
            {
                var mainWindow = (MainWindow)parameter;
                mainWindow.Hide();
                RegistrationWindow registrationWindow = new RegistrationWindow();
                registrationWindow.Show();
            }
            else
            {
                return;
            }
        }
    }
}
