using System;
using System.Globalization;
using System.Windows.Data;

namespace TestFirebaseConnectApp.GUI
{
    public class LoginDataUserConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new UserInputInfo
            {
                Email = (string)values[0],
                Password = (string)values[1]
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
