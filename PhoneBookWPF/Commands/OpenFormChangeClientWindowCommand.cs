using PhoneBookWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class OpenFormChangeClientWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        ListView listView;
        AddClientWindow addClientWindow;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ListView)
            {
                listView = (ListView)parameter;
            }
            else
            {
                return;
            }

            int indexView = listView.SelectedIndex;
            if (indexView == -1 || indexView > (listView.Items.Count - 1))
            {
                MessageBox.Show("Выберите клиента из списка", "Выбор клиента",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                addClientWindow = new AddClientWindow();
                Clients client = (Clients)listView.SelectedItem;
                if (client != null)
                {
                    addClientWindow.lClientId.Content = client.ClientId.ToString();
                    addClientWindow.tbLastName.Text = client.LastName.ToString();
                    addClientWindow.tbFirstName.Text = client.FirstName.ToString();
                    addClientWindow.tbFathersName.Text = client.FathersName.ToString();
                    addClientWindow.tbPhoneNumber.Text = client.PhoneNumber.ToString();
                    addClientWindow.tbEMail.Text = client.Email.ToString();
                    addClientWindow.Show();
                }
                else
                {
                    addClientWindow.lClientId.Content = "";
                    addClientWindow.tbLastName.Text = "";
                    addClientWindow.tbLastName.Text = "";
                    addClientWindow.tbFathersName.Text = "";
                    addClientWindow.tbPhoneNumber.Text = "";
                    addClientWindow.tbEMail.Text = "";
                }
            }
        }
    }
}
