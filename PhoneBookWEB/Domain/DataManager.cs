using PhoneBookWEB.Domain.Repositories.Abstract;

namespace PhoneBookWEB.Domain
{
    public class DataManager
    {
        public IPhoneBookRecordRepository PhoneBookRecords { get; set; }

        public IAccountRepository Accounts { get; set; }

        public string Role { get; set; }

        public DataManager(IPhoneBookRecordRepository phoneBookRecords,
                           IAccountRepository accounts)
        {
            PhoneBookRecords = phoneBookRecords;
            Accounts = accounts;
        }
    }
}
