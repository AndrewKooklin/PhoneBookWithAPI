using System;
using System.Windows;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class CloseWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is Window)
            {
                Window window = (Window)parameter;
                window.Close();
            }
            else
            {
                return;
            }
        }
    }
}
