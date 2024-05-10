using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdminModule.Resources.Methods
{
    public class CheckEmailAddress
    {
        private static readonly Regex regex;
        static CheckEmailAddress()
        {
            var pattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            regex = new(pattern);
        }
        public static bool Check(string number)
        {
            return regex.IsMatch(number);
        }
    }
}
