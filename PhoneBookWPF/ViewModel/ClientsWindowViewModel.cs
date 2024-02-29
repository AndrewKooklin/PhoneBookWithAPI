using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneBookWPF.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections;
using PhoneBookWPF.Model;

namespace PhoneBookWPF.ViewModel
{
    public class ClientsWindowViewModel : BaseViewModel
    {
        //StoreWithEFDBEntities context = new StoreWithEFDBEntities();

        public ClientsWindowViewModel()
        {
            _observableClients = new ObservableCollection<Clients>();
            
            //context.Clients.Load();
            OpenFormAddClientCommand = new OpenFormAddClientWindowCommand();
            AddClientCommand = new AddClientCommand(ObservableClients/*, context*/);
            OpenFormChangeClientWindowCommand = new OpenFormChangeClientWindowCommand();
            ChangeClientCommand = new ChangeClientCommand(ObservableClients/*, context*/);
            DeleteClientCommand = new DeleteClientCommand(ObservableClients/*, context*/);
            ClearClientCommand = new ClearClientCommand();
            CloseWindowCommand = new CloseWindowCommand();
            OpenProductsWindowCommand = new OpenProductsWindowCommand();
            OpenProductsClientWindowCommand = new OpenProductsClientWindowCommand();

            //foreach (var item in context.Clients)
            //{
            //    _observableClients.Add(item);
            //}
        }

        public ICommand OpenFormAddClientCommand { get; set; }

        public ICommand AddClientCommand { get; set; }

        public ICommand DeleteClientCommand { get; set; }

        public ICommand OpenFormChangeClientWindowCommand { get; set; }

        public ICommand ChangeClientCommand { get; set; }

        public ICommand ClearClientCommand { get; set; }

        public ICommand CloseWindowCommand { get; set; }

        public ICommand OpenProductsWindowCommand { get; set; }

        public ICommand OpenProductsClientWindowCommand { get; set; }

        private ObservableCollection<Clients> _observableClients;

        public ObservableCollection<Clients> ObservableClients
        {
            get
            {
                return _observableClients;
            }
            set
            {
                _observableClients = value;
                OnPropertyChanged(nameof(ObservableClients));
            }
        }
    }
}
