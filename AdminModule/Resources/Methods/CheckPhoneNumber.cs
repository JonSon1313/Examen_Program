using System.Text.RegularExpressions;

namespace AdminModule.Resources.Methods
{
    public class CheckPhoneNumber
    {
        private static readonly Regex regex;
        static CheckPhoneNumber() 
        {
            var pattern = @"\d{10}$";
            regex = new(pattern);
        }
        public static bool Check(string number)
        {
            return regex.IsMatch(number);
        }
    }
}
