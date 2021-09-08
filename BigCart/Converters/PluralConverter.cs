using System;
using System.Globalization;
using Xamarin.Forms;

namespace BigCart.Converters
{
    public class PluralConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringFormats = (string[])parameter;
            int number = System.Convert.ToInt32(value);
            string stringFormat = stringFormats[(number == 1) ? 0 : 1];
            return string.Format(stringFormat, number);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
