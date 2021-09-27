using System;
using System.Globalization;
using Xamarin.Forms;

namespace BigCart.Converters
{
    public class CreditCardNumberMaskConverter : IValueConverter
    {
        private const string CARD_NUMBER_PREFIX = "XXXX XXXX XXXX ";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string cardNumber = value?.ToString().Trim();
            if (string.IsNullOrEmpty(cardNumber))
                cardNumber = "XXXX";

            string lastFourDigits = (cardNumber.Length >= 4) ? cardNumber.Substring(cardNumber.Length - 4) : cardNumber;

            if (lastFourDigits.Length < 4)
                lastFourDigits = new string('X', 4 - lastFourDigits.Length) + lastFourDigits;

            return CARD_NUMBER_PREFIX + lastFourDigits;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
