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
    public class ChangeProductCommand : ICommand
    {
        private ObservableCollection<Products> _observableProducts;
        StoreWithEFDBEntities _context;
        public event EventHandler CanExecuteChanged;
        int clientIdValue;
        int productIdValue;
        int productCodeValue;
        string productNameValue = "";

        public ChangeProductCommand(ObservableCollection<Products> observableProducts, StoreWithEFDBEntities context)
        {
            _observableProducts = observableProducts;
            _context = context;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is Grid)
            {
                Grid gridProduct = (Grid)parameter;
                var gridChildren = gridProduct.Children;
                Label lProductId = (Label)gridChildren[1];

                if(String.IsNullOrEmpty(lProductId.Content.ToString()) || 
                    String.IsNullOrWhiteSpace(lProductId.Content.ToString()))
                {
                    MessageBox.Show("Закройте окно и выберите \nпродукт снова", "Выбор продукта",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                TextBox tbClientId = (TextBox)gridChildren[6];
                tbClientId.IsReadOnly= true;
                TextBox tbProductCode = (TextBox)gridChildren[8];
                TextBox tbProductName = (TextBox)gridChildren[10];
 
                Label productCodeError = (Label)gridChildren[9];
                Label productNameError = (Label)gridChildren[11];

                CheckTextAddClientForm checkInputText = new CheckTextAddClientForm();

                productIdValue = Convert.ToInt32(lProductId.Content.ToString());
                clientIdValue = Convert.ToInt32(tbClientId.Text.ToString());
                productCodeValue = Convert.ToInt32(tbProductCode.Text.ToString());
                productNameValue = tbProductName.Text.ToString();

                if (!checkInputText.CheckProductCode(productCodeValue))
                {
                    productCodeError.Content = "Минимум 1 цифра";
                    return false;
                }
                else
                {
                    productCodeError.Content = "";
                }
                if (!checkInputText.CheckName(productNameValue))
                {
                    productNameError.Content = "Наименование не менее 3 букв";
                    return false;
                }
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                Products observableProduct = _observableProducts.First(p => p.ProductId == productIdValue);
                observableProduct.ProductCode = productCodeValue;
                observableProduct.ProductName = productNameValue;
                Products contextProduct = _context.Products.First(p => p.ProductId == productIdValue);
                contextProduct.ProductCode = productCodeValue;
                contextProduct.ProductName = productNameValue;
                _context.SaveChanges();
                App.productsWindow.lvProducts.ItemsSource = null;
                App.productsWindow.lvProducts.ItemsSource = _observableProducts;
            }
        }
    }
}
