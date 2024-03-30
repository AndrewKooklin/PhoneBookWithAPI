using PhoneBookWPF.View;
using PhoneBookWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class ReadRecordsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private PhoneBookWindowViewModel _phoneBookWindowViewModel;

        public ReadRecordsCommand(PhoneBookWindowViewModel phoneBookWindowViewModel)
        {
            _phoneBookWindowViewModel = phoneBookWindowViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter == null)
            {
                return;
            }

            _phoneBookWindowViewModel.LeftCurrentView = null;
            _phoneBookWindowViewModel.LeftCurrentView = new RecordsView();
        }
    }
}
