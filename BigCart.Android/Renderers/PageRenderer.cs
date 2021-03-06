using Android.Content;
using Android.Views;
using AndroidX.Core.View;
using AndroidX.Fragment.App;
using AndroidX.Lifecycle;
using BigCart.Messaging;
using BigCart.Pages;
using Java.Interop;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FormsApplication = Xamarin.Forms.Application;
using Page = BigCart.Pages.Page;

[assembly: ExportRenderer(typeof(Page), typeof(BigCart.Droid.Renderers.PageRenderer))]
namespace BigCart.Droid.Renderers
{
    /// <summary>
    /// See NavigationPageRenderer.cs for explanation.
    /// </summary>
    public class PageRenderer : Xamarin.Forms.Platform.Android.PageRenderer, ILifecycleObserver
    {
        private static bool _isFirstPage = true;
        private static double _navBarHeight;
        private readonly Window _window;
        private NavigationPageRenderer _navigationPageRenderer;
        private Fragment _fragment;
        private ViewGroup _fragmentViewGroup;
        private Page _page;

        public PageRenderer(Context context) : base(context)
        {
            _window = context.GetActivity().Window;
            if (_isFirstPage)
                MessagingCenter.Subscribe<FormsApplication>(this, MessageKeys.Stop, OnExit, App.Current);
        }

        [Export]
        [Lifecycle.Event.OnCreate]
        public void OnFragmentCreated()
        {
            _fragment.PostponeEnterTransition();

            _fragmentViewGroup = _fragment.View.Parent as ViewGroup;
            if (_fragmentViewGroup != null)
                _fragmentViewGroup.ViewTreeObserver.PreDraw += OnPreDrawFragment;
        }

        [Export]
        [Lifecycle.Event.OnResume]
        public void OnFragmentResumed()
        {
            if (!_fragment.IsVisible)
                return;

            _navigationPageRenderer.NotifyTransitionCompleted();

            if (_page.Status != PageStatus.Pending)
                _page.Resume();
            else
            {
                _page.Start();

                if (_isFirstPage)
                {
                    _isFirstPage = false;
                    MessagingCenter.Send<FormsApplication>(App.Current, MessageKeys.Start);
                }
            }
        }

        [Export]
        [Lifecycle.Event.OnPause]
        public void OnFragmentPaused()
        {
            _page.Pause();
        }

        [Export]
        [Lifecycle.Event.OnDestroy]
        public void OnFragmentDestroyed()
        {
            _page.Stop();
        }

        private void OnPreDrawFragment(object sender, ViewTreeObserver.PreDrawEventArgs e)
        {
            _fragmentViewGroup.ViewTreeObserver.PreDraw -= OnPreDrawFragment;
            _fragment.StartPostponedEnterTransition();
            UpdateStatusBarStyle();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);

            _page = Element as Page;

            if (e.OldElement != null)
                e.OldElement.Appearing -= OnAppearing;

            if (e.NewElement != null)
            {
                e.NewElement.Appearing += OnAppearing;
                if (e.NewElement.Parent is NavigationPage navigationPage)
                {
                    _navigationPageRenderer = Platform.GetRenderer(navigationPage) as NavigationPageRenderer;
                    if (_navigationPageRenderer != null)
                    {
                        _fragment = _navigationPageRenderer.GetCurrentFragment();
                        _fragment.ViewLifecycleOwner.Lifecycle.AddObserver(this);
                    }
                }
            }
        }

        private void OnAppearing(object sender, EventArgs e)
        {
            if (_page.Status == PageStatus.Paused)
                UpdateStatusBarStyle();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Page.StatusBarStyleProperty.PropertyName)
                UpdateStatusBarStyle();
        }

        public override WindowInsets OnApplyWindowInsets(WindowInsets insets)
        {
            WindowInsetsCompat insetsCompat = WindowInsetsCompat.ToWindowInsetsCompat(insets);

            if (_window.Attributes.SoftInputMode != SoftInput.AdjustResize)
                _navBarHeight = insetsCompat.GetInsets(WindowInsetsCompat.Type.NavigationBars()).Bottom;
            else if (IsShown)
            {
                double keyboardHeight = insetsCompat.GetInsets(WindowInsetsCompat.Type.Ime()).Bottom;
                keyboardHeight -= _navBarHeight;
                Thickness padding = Element.Padding;
                padding.Bottom = (keyboardHeight > 0) ? Context.FromPixels(keyboardHeight) : 0;
                Element.Padding = padding;
            }

            return base.OnApplyWindowInsets(insets);
        }

        private void UpdateStatusBarStyle()
        {
            var statusBarStyle = (StatusBarStyle)Element.GetValue(Page.StatusBarStyleProperty);

#pragma warning disable CS0618 // Type or member is obsolete
            if (statusBarStyle == StatusBarStyle.DarkContent)
                _window.DecorView.SystemUiVisibility |= (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            else
                _window.DecorView.SystemUiVisibility &= (StatusBarVisibility)~SystemUiFlags.LightStatusBar;
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private void OnExit(FormsApplication _)
        {
            _isFirstPage = true;
            MessagingCenter.Unsubscribe<FormsApplication>(this, MessageKeys.Stop);
        }
    }
}