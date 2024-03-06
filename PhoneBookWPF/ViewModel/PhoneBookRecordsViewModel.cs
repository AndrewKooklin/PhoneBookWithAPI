using Newtonsoft.Json;
using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.Model;
using System;
using System.Collections.Generic;

namespace PhoneBookWPF.ViewModel
{
    public class PhoneBookRecordsViewModel : BaseViewModel
    {
        Records records = new Records();
        
        public PhoneBookRecordsViewModel()
        {
            PhoneBooks = records.GetRecords().GetAwaiter().GetResult();
        }

        private List<PhoneBookRecord> _phoneBooks = new List<PhoneBookRecord>();

        public List<PhoneBookRecord> PhoneBooks
        {
            get
            {
                return _phoneBooks;
            }
            set
            {
                _phoneBooks = value;
                OnPropertyChanged(nameof(PhoneBooks));
            }
        }
    }
}
