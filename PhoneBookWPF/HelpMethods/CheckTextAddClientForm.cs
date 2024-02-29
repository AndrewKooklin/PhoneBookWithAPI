using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Mail;

namespace PhoneBookWPF.HelpMethods
{
    public class CheckTextAddClientForm
    {
        public bool CheckName(string text)
        {
            if (Regex.IsMatch(text, "^[a-zA-Z]{3,}"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CheckPhone(string text)
        {
            if (Regex.IsMatch(text, "^[0-9]{11}"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool CheckEmail(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            try
            {
                MailAddress m = new MailAddress(text);
                
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool CheckClientId(int id)
        {
            if (Regex.IsMatch(id.ToString(), "^[0-9]{1,}"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckProductCode(int id)
        {
            if (Regex.IsMatch(id.ToString(), "^[0-9]{1,}"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckProductName(string productName)
        {
            if (Regex.IsMatch(productName, "^[a-zA-Z]{3,}"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckParseToInt32(string text)
        {
            bool check = Int32.TryParse(text, out int value);

            if (check)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
