using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace BigCart.Pages
{
    public partial class MyAddressPage : Page
    {
        public MyAddressPage()
        {
            InitializeComponent();
        }

        private void Expander_Tapped(object sender, System.EventArgs e)
        {
            var expander = (Expander)sender;
            var dropdownIcon = expander.FindByName<View>("dropdownIcon");
            dropdownIcon.CancelAnimations();
            dropdownIcon.RotateTo(expander.IsExpanded ? -180 : 0, expander.AnimationLength, expander.AnimationEasing);
        }
    }
}