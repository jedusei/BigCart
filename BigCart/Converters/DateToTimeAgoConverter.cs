using System;
using System.Globalization;
using Xamarin.Forms;

namespace BigCart.Converters
{
    public class DateToTimeAgoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result;
            TimeSpan timeSpan = DateTime.Now.Subtract((DateTime)value);

            if (timeSpan <= TimeSpan.FromSeconds(10))
            {
                result = "Just now";
            }
            else if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = (timeSpan.Minutes > 1) ?
                    string.Format("{0} minutes ago", timeSpan.Minutes) :
                    "a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = (timeSpan.Hours > 1)
                    ? string.Format("{0} hours ago", timeSpan.Hours)
                    : "an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = (timeSpan.Days > 1)
                    ? string.Format("{0} days ago", timeSpan.Days)
                    : "Yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = (timeSpan.Days > 30)
                    ? string.Format("{0} months ago", timeSpan.Days / 30)
                    : "a month ago";
            }
            else
            {
                result = (timeSpan.Days > 365)
                    ? string.Format("{0} years ago", timeSpan.Days / 365)
                    : "a year ago";
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
