﻿using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace BigCart.Pages
{
    public partial class WelcomePage : Page
    {
        private const string SCROLL_ANIM_NAME = "scroll";
        private const string RESIZE_ANIM_NAME = "resize";
        private bool _measured;
        private ObservableCollection<ImageSource> _images = new ObservableCollection<ImageSource> { ImageSource.FromFile("welcome_1.png") };
        private ImageSource _loginImage = ImageSource.FromFile("welcome_2.png");
        private ImageSource _signupImage = ImageSource.FromFile("welcome_3.png");

        public WelcomePage()
        {
            InitializeComponent();
            _carouselView.ItemsSource = _images;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (!_measured)
            {
                _measured = true;
                AbsoluteLayout.SetLayoutBounds(_homeView, new Rectangle(0, 0, width, AbsoluteLayout.AutoSize));
                AbsoluteLayout.SetLayoutBounds(_loginView, new Rectangle(0, height, width, AbsoluteLayout.AutoSize));
                AbsoluteLayout.SetLayoutBounds(_signupView, new Rectangle(0, height, width, AbsoluteLayout.AutoSize));
                _scrollView.HeightRequest = _homeView.Height;
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

        private void GoTo(View tab, ImageSource image)
        {
            _scrollView.AbortAnimation(SCROLL_ANIM_NAME);
            _scrollView.AbortAnimation(RESIZE_ANIM_NAME);

            double tabPosition = _scrollView.ScrollX + _scrollView.Width;
            Rectangle bounds = new(tabPosition, 0, _scrollView.Width, AbsoluteLayout.AutoSize);
            AbsoluteLayout.SetLayoutBounds(tab, bounds);

            _images.Add(image);
            _carouselView.ScrollTo(_images.Count - 1);

            _scrollView.Animate(
                SCROLL_ANIM_NAME,
                x => _scrollView.ScrollToAsync(x, 0, false),
                start: _scrollView.ScrollX,
                end: tabPosition,
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