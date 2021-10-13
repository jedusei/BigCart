using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public class SwitchEx : SfSwitch
    {
        public SwitchEx()
        {
            Style = (Style)App.Current.Resources["Switch"];
        }
    }
}
