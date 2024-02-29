using PhoneBookWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class OpenFormChangeProductWindowCommand : ICommand
    {
        ListView listView;
        AddProductWindow addProductWindow;
        public event EventHandler CanExecuteChanged;

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
            else
            {
                addProductWindow = new AddProductWindow();
                Products product = (Products)listView.SelectedItem;
                if (product != null)
                {
                    addProductWindow.lProductId.Content = product.ProductId.ToString();
                    addProductWindow.tbClientId.Text = product.ClientId.ToString();
                    addProductWindow.tbClientId.IsReadOnly = true;
                    addProductWindow.tbProductCode.Text = product.ProductCode.ToString();
                    addProductWindow.tbProductName.Text = product.ProductName.ToString();
                    addProductWindow.Show();
                }
                else
                {
                    addProductWindow.lProductId.Content = "";
                    addProductWindow.tbClientId.Text = "";
                    addProductWindow.tbProductCode.Text = "";
                    addProductWindow.tbProductName.Text = "";
                }
            }
        }
    }
}
