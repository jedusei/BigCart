using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public class SwitchEx : SfSwitch
    {
        public SwitchEx()
        {
            var style = App.Current.Resources["Switch"] as Style;
            foreach (Setter setter in style.Setters)
                SetValue(setter.Property, setter.Value);

            // Apply changes
            IsOn = true;
            IsOn = false;
        }
    }
}
