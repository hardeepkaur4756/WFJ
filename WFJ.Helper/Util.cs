using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WFJ.Helper
{
    public class Util
    {
        // Change encode and decode.
        public static string Encode(string stringToEncode)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(stringToEncode));
        }
        public static string Decode(string stringToDecode)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(stringToDecode));
        }
        public static bool ValidateEmail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})$";
            bool isVaidEmail = Regex.IsMatch(email, strRegex, RegexOptions.IgnoreCase);
            if (isVaidEmail) { return (true); } else { return (false); }
        }

        public static bool ValidatePassword(string password)
        {
            string strRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            bool isVaidPassword = Regex.IsMatch(password, strRegex, RegexOptions.IgnoreCase);
            if (isVaidPassword) { return (true); } else { return (false); }
        }
    }
}
