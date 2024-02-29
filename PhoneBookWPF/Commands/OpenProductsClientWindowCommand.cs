using PhoneBookWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class OpenProductsClientWindowCommand : ICommand
    {
        ListView listViewClients;
        ObservableCollection<Products> _observableProducts;
        public event EventHandler CanExecuteChanged;
        StoreWithEFDBEntities _context = new StoreWithEFDBEntities();

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            if (parameter is ListView)
            {
                listViewClients = (ListView)parameter;
            }
            else
            {
                return;
            }
            int indexView = listViewClients.SelectedIndex;
            if (indexView == -1 || indexView > (listViewClients.Items.Count - 1))
            {
                MessageBox.Show("Выберите клиента из списка", "Выбор клиента",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Clients client = (Clients)listViewClients.SelectedItem;
            int clientId = client.ClientId;

            _observableProducts = new ObservableCollection<Products>();

            _context.Products.Load();

            foreach(Products product in _context.Products)
            {
                if(product.ClientId == clientId)
                {
                    _observableProducts.Add(product);
                }
            }

            App.productsClientWindow = new ProductsClientWindow();
            App.productsClientWindow.lvProductsClient.ItemsSource = _observableProducts;
            App.productsClientWindow.Show();
        }
    }
}
