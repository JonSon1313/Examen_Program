
using System.Text.RegularExpressions;

namespace ClientModule.Resources.Methods
{
    internal class CheckCreditCardOwnerName
    {
        private static readonly Regex regex;
        static CheckCreditCardOwnerName()
        {
            var pattern = @"([A-Z]+){1}((\s){1}[A-Z]+){1}";
            regex = new(pattern);
        }
        public static bool Check(string number)
        {
            return regex.IsMatch(number);
        }
    }
    internal class CheckCreditCardNumber
    {
        private static readonly Regex regex;
        static CheckCreditCardNumber()
        {
            var pattern = @"([0-9]{4}(-){1}){3}([0-9]{4}){1}";
            regex = new(pattern);
        }
        public static bool Check(string number)
        {
            return regex.IsMatch(number);
        }
    }
    internal class CheckCreditCardCVV
    {
        private static readonly Regex regex;
        static CheckCreditCardCVV()
        {
            var pattern = @"[0-9]{3}";
            regex = new(pattern);
        }
        public static bool Check(string number)
        {
            return regex.IsMatch(number);
        }
    }
}
