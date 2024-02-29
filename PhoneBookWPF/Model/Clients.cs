using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhoneBookWPF.Model
{
    public partial class Clients : INotifyPropertyChanged
    {
        public Clients()
        {
            this.Products = new HashSet<Products>();
        }

        public Clients(string lastName, string firstName, 
            string fathersName, string phoneNumber, string email)
        {
            LastName = lastName;
            FirstName = firstName;
            FathersName = fathersName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        private string _lastName = String.Empty;
        private string _firstName = String.Empty;
        private string _fathersName = String.Empty;
        private string _phoneNumber = String.Empty;
        private string _email = String.Empty;


        public int ClientId { get; set; }
        public string LastName {
            get
            {
                return _lastName;
            }

            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    NotifyPropertyChanged(nameof(LastName));
                }
            }
        }
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    NotifyPropertyChanged(nameof(FirstName));
                }
            }
        }
        public string FathersName {
            get
            {
                return _fathersName;
            }

            set
            {
                if (value != _fathersName)
                {
                    _fathersName = value;
                    NotifyPropertyChanged(nameof(FathersName));
                }
            }
        }
        public string PhoneNumber {
            get
            {
                return _phoneNumber;
            }

            set
            {
                if (value != _phoneNumber)
                {
                    _phoneNumber = value;
                    NotifyPropertyChanged(nameof(PhoneNumber));
                }
            }
        }
        public string Email {
            get
            {
                return _email;
            }

            set
            {
                if (value != _email)
                {
                    _email = value;
                    NotifyPropertyChanged(nameof(Email));
                }
            }
        }
    
        
        public virtual ICollection<Products> Products { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
