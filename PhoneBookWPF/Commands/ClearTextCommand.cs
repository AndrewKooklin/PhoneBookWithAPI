using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class ClearTextCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
            {
                return;
            }
            else
            {
                var fieldElements = (object[])parameter;
                TextBox tbrecordId = (TextBox)fieldElements[0];
                TextBox tbRecordLastName = (TextBox)fieldElements[1];
                TextBox tbRecordFirstName = (TextBox)fieldElements[2];
                TextBox tbRecordFathersName = (TextBox)fieldElements[3];
                TextBox tbRecordPhoneNumber = (TextBox)fieldElements[4];
                TextBox tbRecordAddress = (TextBox)fieldElements[5];
                TextBox tbRecordDescription = (TextBox)fieldElements[6];
                TextBlock tbErrorRecordLastName = (TextBlock)fieldElements[7];
                TextBlock tbErrorRecordFirstName = (TextBlock)fieldElements[8];
                TextBlock tbErrorRecordFathersName = (TextBlock)fieldElements[9];
                TextBlock tbErrorRecordPhoneNumber = (TextBlock)fieldElements[10];

                tbrecordId.Text = "";
                tbRecordLastName.Text = "";
                tbRecordFirstName.Text = "";
                tbRecordFathersName.Text = "";
                tbRecordPhoneNumber.Text = "";
                tbRecordAddress.Text = "";
                tbRecordDescription.Text = "";
                tbErrorRecordLastName.Text = "";
                tbErrorRecordFirstName.Text = "";
                tbErrorRecordFathersName.Text = "";
                tbErrorRecordPhoneNumber.Text = "";

                App.ActionsWithRecordView.tbResult.Text = "";
            }
        }
    }
}
