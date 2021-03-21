using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace TestFirebaseConnectApp.GUI
{
    public class RegisterDataUserConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        { 
            return new UserInputInfo
            {
                Email = (string)values[0],
                Password = (string)values[1],
                ConfirmPassword = (string)values[2]
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
