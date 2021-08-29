using Xamarin.Forms;

namespace BigCart.Pages
{
    public partial class OnboardingPage : Page
    {
        public OnboardingPage()
        {
            InitializeComponent();
            SetPages();
        }

        private void SetPages()
        {
            carouselView.ItemsSource = new[] {
                new
                {
                    ShowLogo = true,
                    Title = "Welcome to",
                    Image = ImageSource.FromFile("onboarding_1.png")
                },
                new
                {
                    ShowLogo = false,
                    Title = "Buy Quality Dairy Products",
                    Image = ImageSource.FromFile("onboarding_2.png")
                },
                new
                {
                    ShowLogo = false,
                    Title = "Buy Premium Quality Fruits",
                    Image = ImageSource.FromFile("onboarding_3.png")
                },
                new
                {
                    ShowLogo = false,
                    Title = "Get Discounts On All Products",
                    Image = ImageSource.FromFile("onboarding_4.png")
                }
            };
        }
    }
}
