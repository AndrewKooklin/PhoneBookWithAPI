using PhoneBookWPF.Commands;
using PhoneBookWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhoneBookWPF.ViewModel
{
    public class ProductsWindowViewModel : BaseViewModel
    {
        //StoreWithEFDBEntities _context = new StoreWithEFDBEntities();

        public ProductsWindowViewModel()
        {
            _observableProducts = new ObservableCollection<Products>();
            //_context.Products.Load();

            //foreach (var item in _context.Products)
            //{
            //    _observableProducts.Add(item);
            //}

            OpenFormAddProductWindowCommand = new OpenFormAddProductWindowCommand();
            AddProductCommand = new AddProductCommand(_observableProducts/*, _context*/);
            DeleteProductCommand = new DeleteProductCommand(_observableProducts/*, _context*/);
            OpenFormChangeProductWindowCommand = new OpenFormChangeProductWindowCommand();
            ChangeProductCommand = new ChangeProductCommand(_observableProducts/*, _context*/);
            ClearProductCommand = new ClearProductCommand();
            CloseWindowCommand = new CloseWindowCommand();
        }

        private ObservableCollection<Products> _observableProducts;

        public ObservableCollection<Products> ObservableProducts
        {
            get
            {
                return _observableProducts;
            }
            set
            {
                _observableProducts = value;
                OnPropertyChanged(nameof(ObservableProducts));
            }
        }

        public ICommand OpenFormAddProductWindowCommand { get; set; }

        public ICommand AddProductCommand { get; set; }

        public ICommand DeleteProductCommand { get; set; }

        public ICommand ChangeProductCommand { get; set; }

        public ICommand ClearProductCommand { get; set; }

        public ICommand CloseWindowCommand { get; set; }

        public ICommand OpenFormChangeProductWindowCommand { get; set; }
    }
}
