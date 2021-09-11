using Syncfusion.SfRating.XForms;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public class RatingEx : SfRating
    {
        public RatingEx()
        {
            var style = App.Current.Resources["Rating"] as Style;
            foreach (Setter setter in style.Setters)
                SetValue(setter.Property, setter.Value);
        }
    }
}
