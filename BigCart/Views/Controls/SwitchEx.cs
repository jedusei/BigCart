using Syncfusion.XForms.Buttons;
using System.Linq;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public class SwitchEx : SfSwitch
    {
        public SwitchEx()
        {
            Style = App.Current.Resources["Switch"] as Style;
            Setter setter = Style.Setters.FirstOrDefault(s => s.Property == VisualStateManager.VisualStateGroupsProperty);
            if (setter != null)
                VisualStateManager.SetVisualStateGroups(this, (VisualStateGroupList)setter.Value);
        }
    }
}
