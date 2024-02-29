using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class ClearProductCommand : ICommand
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
                Grid gridProduct = (Grid)parameter;
                var gridChildren = gridProduct.Children;
                Label lProductId = (Label)gridChildren[1];
                TextBox tbClientId = (TextBox)gridChildren[6];
                tbClientId.IsReadOnly = true;
                TextBox tbProductCode = (TextBox)gridChildren[8];
                TextBox tbProductName = (TextBox)gridChildren[10];

                Label productCodeError = (Label)gridChildren[9];
                Label productNameError = (Label)gridChildren[11];

                lProductId.Content = "";
                tbClientId.Text = "";
                tbProductCode.Text = "";
                tbProductName.Text = "";

                productCodeError.Content = "";
                productNameError.Content = "";
            }
        }
    }
}
