using BigCart.Views;

namespace BigCart
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
