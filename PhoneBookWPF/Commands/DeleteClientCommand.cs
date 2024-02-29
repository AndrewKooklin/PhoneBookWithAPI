using PhoneBookWPF.HelpMethods;
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
    public class DeleteClientCommand : ICommand
    {
        ObservableCollection<Clients> _observableClients;
        ListView listView;
        StoreWithEFDBEntities _context;
        public event EventHandler CanExecuteChanged;

        public DeleteClientCommand(ObservableCollection<Clients> observableClients, StoreWithEFDBEntities context)
        {
            _observableClients = observableClients;
            _context = context;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ListView)
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

            Clients client = (Clients)listView.SelectedItem;
            foreach (var item in _context.Clients)
            {
                if (item.Equals(client))
                {
                    _context.Clients.Remove(client);
                }
            }
            _context.SaveChanges();
            _observableClients.Remove(client);
            App.clientsWindow.lvClients.ItemsSource = null;
            App.clientsWindow.lvClients.ItemsSource = _observableClients;
        }
    }
}
