using System.Text.RegularExpressions;

namespace TestFirebaseConnectApp.GUI
{
    public static class EmailValidator
    {
        private static readonly Regex regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

        public static bool Validate(string email, ref string errorText)
        {
            if(email is null)
            {
                errorText = "Почта не введена.";
                return false;
            }

            if(!regex.IsMatch(email))
            {
                errorText = $"{email} не является допустимым адресом почты.";
                return false;
            }

            return true;
        }
    }
}
