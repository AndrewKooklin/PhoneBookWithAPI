using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace PhoneBookWPF.HelpMethods
{
    public class ValidateTextBoxRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Regex.IsMatch(value.ToString(), "^[0-9a-zA-Z]{3,}"))
            {
                return new ValidationResult(false,
                    "Введите имя не менее 3 букв или цифр.");
            }
            return new ValidationResult(true, null);
        }
    }
}
