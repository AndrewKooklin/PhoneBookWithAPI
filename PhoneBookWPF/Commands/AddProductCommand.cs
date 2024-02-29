using PhoneBookWPF.HelpMethods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class AddProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private ObservableCollection<Products> _observableProducts;
        StoreWithEFDBEntities _context;
        int clientIdValue;
        int productCodeValue;
        string productNameValue = "";
        string emailValue = "";

        public AddProductCommand(ObservableCollection<Products> observableProducts, StoreWithEFDBEntities context)
        {
            _observableProducts = observableProducts;
            _context = context;

        }

        public bool CanExecute(object parameter)
        {
            if (parameter is Grid)
            {
                Grid gridClient = (Grid)parameter;
                var gridChildren = gridClient.Children;
                TextBox tbClientId = (TextBox)gridChildren[6];
                TextBox tbProductCode = (TextBox)gridChildren[8];
                TextBox tbProductName = (TextBox)gridChildren[10];
                TextBox tbEmail = (TextBox)gridChildren[12];

                Label clientIdError = (Label)gridChildren[7];
                Label productCodeError = (Label)gridChildren[9];
                Label productNameError = (Label)gridChildren[11];

                clientIdValue = Convert.ToInt32(tbClientId.Text.ToString());
                productCodeValue = Convert.ToInt32(tbProductCode.Text.ToString());
                productNameValue = tbProductName.Text.ToString();

                CheckTextAddClientForm checkInputText = new CheckTextAddClientForm();

                if (!checkInputText.CheckClientId(clientIdValue))
                {
                    clientIdError.Content = "Минимум 1 цифра";
                    return false;
                }
                else
                {
                    clientIdError.Content = "";
                }
                if (_context.Clients.Any(c => c.ClientId == clientIdValue))
                {
                    Clients client = _context.Clients.First(c => c.ClientId == clientIdValue);
                    emailValue = client.Email;
                    clientIdError.Content = "";
                }
                else
                {
                    clientIdError.Content = "Клиента с таким Id нет в базе";
                    return false;
                }
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
                Products product = new Products(clientIdValue, productCodeValue, 
                    productNameValue, emailValue);
                _observableProducts.Add(product);
                _context.Products.Add(product);
                _context.SaveChanges();
                App.productsWindow.lvProducts.ItemsSource = null;
                App.productsWindow.lvProducts.ItemsSource = _observableProducts;
            }
        }
    }
}
