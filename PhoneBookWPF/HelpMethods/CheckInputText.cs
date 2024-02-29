using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneBookWPF.HelpMethods
{
    public class CheckInputText
    {
        public bool CheckText(string text)
        {
            if (Regex.IsMatch(text, "^[0-9a-zA-Z]{3,}"))
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
