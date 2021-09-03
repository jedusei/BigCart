using BigCart.Pages;
using CoreGraphics;
using Foundation;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Page = BigCart.Pages.Page;

[assembly: ExportRenderer(typeof(Page), typeof(BigCart.iOS.Renderers.PageRenderer))]
namespace BigCart.iOS.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        private const double EXTRA_SHIFT_AMOUNT = 20;
        private NSObject _keyboardShowObserver, _keyboardHideObserver;
        private bool _isKeyboardShown;
        private UIViewPropertyAnimator _viewPropertyAnimator;

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

            if (Page.WindowSoftInputModeAdjust != Xamarin.Forms.PlatformConfiguration.AndroidSpecific.WindowSoftInputModeAdjust.Unspecified)
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
            CGRect keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);

            if (Page.WindowSoftInputModeAdjust != Xamarin.Forms.PlatformConfiguration.AndroidSpecific.WindowSoftInputModeAdjust.Resize)
            {
                UIView activeView = FindFirstResponder(View);
                if (activeView != null)
                {
                    double viewBottomPosition = GetViewBottomPosition(activeView, View);
                    double pageHeight = View.Frame.Height;
                    double keyboardHeight = keyboardFrame.Height;

                    if (viewBottomPosition >= (pageHeight - keyboardHeight)) // Is keyboard overlapping view?
                    {
                        double newY = pageHeight - viewBottomPosition - keyboardHeight - EXTRA_SHIFT_AMOUNT;
                        StartSlideAnimation(notification, newY);
                    }
                }
            }
            else
            {
                Thickness padding = Page.Padding;
                padding.Bottom = keyboardFrame.Height;
                Page.Padding = padding;
            }
        }

        private void OnKeyboardHide(NSNotification notification)
        {
            _isKeyboardShown = false;
            if (Page.WindowSoftInputModeAdjust != Xamarin.Forms.PlatformConfiguration.AndroidSpecific.WindowSoftInputModeAdjust.Resize)
                StartSlideAnimation(notification, 0);
            else
            {
                Thickness padding = Page.Padding;
                padding.Bottom = 0;
                Page.Padding = padding;
            }
        }

        private static UIView FindFirstResponder(UIView view)
        {
            if (view.IsFirstResponder)
                return view;

            foreach (UIView subView in view.Subviews)
            {
                var firstResponder = FindFirstResponder(subView);
                if (firstResponder != null)
                    return firstResponder;
            }

            return null;
        }

        private static double GetViewBottomPosition(UIView view, UIView rootView)
        {
            CGPoint viewRelativeCoordinates = rootView.ConvertPointFromView(view.Frame.Location, view);
            double activeViewRoundedY = Math.Round(viewRelativeCoordinates.Y, 2);

            return activeViewRoundedY + view.Frame.Height;
        }

        private void StartSlideAnimation(NSNotification notification, double endValue)
        {
            double duration = ((NSNumber)notification.UserInfo[UIKeyboard.AnimationDurationUserInfoKey]).DoubleValue;
            ulong curveValue = ((NSNumber)notification.UserInfo[UIKeyboard.AnimationCurveUserInfoKey]).UnsignedLongValue;

            _viewPropertyAnimator?.StopAnimation(true);

            _viewPropertyAnimator = new UIViewPropertyAnimator(duration, (UIViewAnimationCurve)curveValue, () =>
            {
                CoreGraphics.CGRect frame = View.Frame;
                frame.Y = (nfloat)endValue;
                View.Frame = frame;
            });

            _viewPropertyAnimator.StartAnimation();
        }
    }
}