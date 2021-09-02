using BigCart.Pages;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Page = BigCart.Pages.Page;

[assembly: ExportRenderer(typeof(Page), typeof(BigCart.iOS.Renderers.PageRenderer))]
namespace BigCart.iOS.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        private NSObject _keyboardShowObserver, _keyboardHideObserver;
        private bool _isKeyboardShown;

        public Page Page => Element as Page;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;

            if (e.NewElement != null)
                e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            UpdateStatusBarStyle();

            if (Page.WindowSoftInputModeAdjust == Xamarin.Forms.PlatformConfiguration.AndroidSpecific.WindowSoftInputModeAdjust.Resize)
            {
                _keyboardShowObserver ??= NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardShow);
                _keyboardHideObserver ??= NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardHide);
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            if (_keyboardShowObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardShowObserver);
                _keyboardShowObserver.Dispose();
                _keyboardShowObserver = null;
            }

            if (_keyboardHideObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardHideObserver);
                _keyboardHideObserver.Dispose();
                _keyboardHideObserver = null;
            }
        }

        private void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Page.StatusBarStyleProperty.PropertyName)
                UpdateStatusBarStyle();
        }

        private void UpdateStatusBarStyle()
        {
            UIStatusBarStyle statusBarStyle = Element.GetValue(Page.StatusBarStyleProperty) switch
            {
                StatusBarStyle.LightContent => UIStatusBarStyle.LightContent,
                StatusBarStyle.DarkContent => UIStatusBarStyle.DarkContent,
                _ => UIStatusBarStyle.Default
            };

            if (statusBarStyle != UIApplication.SharedApplication.StatusBarStyle)
                UIApplication.SharedApplication.SetStatusBarStyle(statusBarStyle, true);
        }

        private void OnKeyboardShow(NSNotification notification)
        {
            if (_isKeyboardShown)
                return;

            _isKeyboardShown = true;
            CoreGraphics.CGRect keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
            Thickness padding = Page.Padding;
            padding.Bottom = keyboardFrame.Height;
            Page.Padding = padding;
        }

        private void OnKeyboardHide(NSNotification notification)
        {
            _isKeyboardShown = false;
            Thickness padding = Page.Padding;
            padding.Bottom = 0;
            Page.Padding = padding;
        }
    }
}