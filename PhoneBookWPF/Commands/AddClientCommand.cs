using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class AddClientCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        string lastNameValue = "";
        string firstNameValue = "";
        string fathersNameValue = "";
        string phoneNumberValue = "";
        string emailValue = "";

        private ObservableCollection<Clients> _observableClients;
        StoreWithEFDBEntities _context;

        public AddClientCommand(ObservableCollection<Clients> observableClients, StoreWithEFDBEntities context)
        {
            _observableClients = observableClients;
            _context = context;
        }

        public bool CanExecute(object parameter)
        {
            if(parameter is Grid)
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

                lastNameValue = lastName.Text.ToString();
                firstNameValue = firstName.Text.ToString();
                fathersNameValue = fathersName.Text.ToString();
                phoneNumberValue = phoneNumber.Text.ToString();
                emailValue = email.Text.ToString();

                CheckTextAddClientForm checkInputText = new CheckTextAddClientForm();

                if (!checkInputText.CheckName(lastNameValue))
                {
                    lastNameError.Content = "Фамилия не менее 3 букв";
                    return false;
                }
                else
                {
                    lastNameError.Content = "";
                }
                if (!checkInputText.CheckName(firstNameValue))
                {
                    firstNameError.Content = "Имя не менее 3 букв";
                    return false;
                }
                else
                {
                    firstNameError.Content = "";
                }
                if (!checkInputText.CheckName(fathersNameValue))
                {
                    fathersNameError.Content = "Отчество не менее 3 букв";
                    return false;
                }
                else
                {
                    fathersNameError.Content = "";
                }
                if (!checkInputText.CheckPhone(phoneNumberValue))
                {
                    phoneNumberError.Content = "Телефон 11 цифр";
                    return false;
                }
                else
                {
                    phoneNumberError.Content = "";
                }
                if(_context.Clients.Any(c => c.PhoneNumber == phoneNumberValue))
                {
                    phoneNumberError.Content = "Такой номер уже есть в базе";
                    return false;
                }
                else
                {
                    phoneNumberError.Content = "";
                }
                if (!checkInputText.CheckEmail(emailValue))
                {
                    emailError.Content = "Email в формате name@site.ru";
                    return false;
                }
                else
                {
                    emailError.Content = "";
                }
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter != null)
            {
                Clients client = new Clients(lastNameValue, firstNameValue, fathersNameValue,
                    phoneNumberValue, emailValue);
                _observableClients.Add(client);
                _context.Clients.Add(client);
                _context.SaveChanges();
                App.clientsWindow.lvClients.ItemsSource = null;
                App.clientsWindow.lvClients.ItemsSource = _observableClients;
            }
        }
    }
}
