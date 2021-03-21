namespace TestFirebaseConnectApp.GUI
{
    public static class PasswordValidator
    {
        public static bool Validate(string password, string confirmPassword, ref string errorText)
        {
            if (password.Length == 0)
            {
                errorText = "Пароль не введен.";
                return false;
            }

            if (!confirmPassword.Equals(password))
            {
                errorText = "Пароли не совпадают.";
                return false;
            }

            return true;
        }

        public static bool Validate(string password, ref string errorText)
        {
            if (password.Length == 0)
            {
                errorText = "Пароль не введен.";
                return false;
            }

            return true;
        }
    }
}
