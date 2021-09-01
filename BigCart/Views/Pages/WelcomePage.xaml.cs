using Xamarin.Forms;

namespace BigCart.Pages
{
    public partial class WelcomePage : Page
    {
        private const string SCROLL_ANIM_NAME = "scroll";
        private readonly uint SCROLL_ANIM_DURATION = 500u;
        private const string RESIZE_ANIM_NAME = "resize";
        private bool _measured;

        public WelcomePage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (!_measured)
            {
                _measured = true;

                Rectangle bounds = new Rectangle(0, 0, width, AbsoluteLayout.AutoSize);
                AbsoluteLayout.SetLayoutBounds(_homeImage, bounds);
                AbsoluteLayout.SetLayoutBounds(_homeView, bounds);
                AbsoluteLayout.SetLayoutBounds(_loginView, bounds);
                AbsoluteLayout.SetLayoutBounds(_signupView, bounds);

                _scrollView.HeightRequest = _homeView.Height;
                _rootView.HeightRequest = height;
            }
        }

        private void LoginLinkTapped(object sender, System.EventArgs e)
        {
            GoTo(_loginView, _loginImage);
        }

        private void SignupLinkTapped(object sender, System.EventArgs e)
        {
            GoTo(_signupView, _signupImage);
        }

        private void GoTo(View tab, Image image)
        {
            _scrollView.AbortAnimation(SCROLL_ANIM_NAME);
            _scrollView.AbortAnimation(RESIZE_ANIM_NAME);

            double tabPosition = _scrollView.ScrollX + _scrollView.Width;
            Rectangle bounds = new(tabPosition, 0, _scrollView.Width, AbsoluteLayout.AutoSize);
            AbsoluteLayout.SetLayoutBounds(tab, bounds);
            AbsoluteLayout.SetLayoutBounds(image, bounds);

            tab.IsVisible = image.IsVisible = true;

            _scrollView.Animate(
                SCROLL_ANIM_NAME,
                x =>
                {
                    _scrollView.ScrollToAsync(x, 0, false);
                    _imageScrollView.ScrollToAsync(x, 0, false);
                },
                start: _scrollView.ScrollX,
                end: tabPosition,
                length: SCROLL_ANIM_DURATION,
                easing: Easing.CubicOut,
                finished: (_, cancelled) =>
                {
                    if (!cancelled)
                    {
                        _scrollView.Animate(
                            RESIZE_ANIM_NAME,
                            h => _scrollView.HeightRequest = h,
                            start: _scrollView.HeightRequest,
                            end: tab.Height,
                            easing: Easing.CubicOut
                        );
                    }
                }
            );
        }
    }
}