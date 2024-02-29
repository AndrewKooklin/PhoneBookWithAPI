using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class ClearClientCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Grid)
            {
                Grid gridClient = (Grid)parameter;
                var gridChildren = gridClient.Children;
                TextBox lastName = (TextBox)gridChildren[7];
                TextBox firstName = (TextBox)gridChildren[9];
                TextBox fathersName = (TextBox)gridChildren[11];
                TextBox phoneNumber = (TextBox)gridChildren[13];
                TextBox email = (TextBox)gridChildren[15];

                Label lastNameError = (Label)gridChildren[8];
                Label firstNameError = (Label)gridChildren[10];
                Label fathersNameError = (Label)gridChildren[12];
                Label phoneNumberError = (Label)gridChildren[14];
                Label emailError = (Label)gridChildren[16];

                lastName.Text = "";
                firstName.Text = "";
                fathersName.Text = "";
                phoneNumber.Text = "";
                email.Text = "";

                lastNameError.Content = "";
                firstNameError.Content = "";
                fathersNameError.Content = "";
                phoneNumberError.Content = "";
                emailError.Content = "";
            }
        }
    }
}
