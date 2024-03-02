using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWPF.HelpMethods
{
    public static class PhoneBookContext
    {
        public static ModelBook GetContext()
        {
            return new ModelBook();
        }
    }
}
