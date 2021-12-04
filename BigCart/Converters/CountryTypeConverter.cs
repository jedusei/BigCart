using BigCart.Models;
using System.Linq;
using Xamarin.Forms;

namespace BigCart.Converters
{
    public class CountryTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            string name = value?.ToString().Trim().ToUpper();
            if (string.IsNullOrEmpty(name))
                return null;

            return Country.All.FirstOrDefault(c => (c.Name.ToUpper() == name) || (c.NameCode == name));
        }

        public override string ConvertToInvariantString(object value)
        {
            return (value as Country)?.Name;
        }
    }
}
