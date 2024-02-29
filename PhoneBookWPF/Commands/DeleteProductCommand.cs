using PhoneBookWPF.Model;
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
    public class DeleteProductCommand : ICommand
    {
        ObservableCollection<Products> _observableProducts;
        ListView listView;
        //StoreWithEFDBEntities _context;
        public event EventHandler CanExecuteChanged;

        public DeleteProductCommand(ObservableCollection<Products> observableProducts/*, StoreWithEFDBEntities context*/)
        {
            _observableProducts = observableProducts;
            //_context = context;
        }

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
                MessageBox.Show("Выберите продукт из списка", "Выбор продукта",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Products product = (Products)listView.SelectedItem;
            //Products contextProduct = _context.Products.First(p => p.ProductId == product.ProductId);
            //foreach(var item in _context.Products)
            //{
            //    if (item.ProductId.Equals(contextProduct.ProductId))
            //    {
            //        _context.Products.Remove(item);
            //    }
            //}
            //_context.SaveChanges();
            _observableProducts.Remove(product);
            App.productsWindow.lvProducts.ItemsSource = null;
            App.productsWindow.lvProducts.ItemsSource = _observableProducts;
        }
    }
}
