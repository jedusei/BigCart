using BigCart.Messaging;
using BigCart.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace BigCart.Pages
{
    public enum StatusBarStyle
    {
        LightContent,
        DarkContent
    }

    public class Page : ContentPage
    {
        public static readonly BindableProperty StatusBarStyleProperty = BindableProperty.Create(nameof(StatusBarStyle), typeof(StatusBarStyle), typeof(Page), StatusBarStyle.DarkContent);
        protected bool _hasLoaded;
        private const int TRANSITION_DURATION = 250;
        private ViewModel _viewModel;

        public StatusBarStyle StatusBarStyle
        {
            get => (StatusBarStyle)GetValue(StatusBarStyleProperty);
            set => SetValue(StatusBarStyleProperty, value);
        }

        static Page()
        {
            if (Device.RuntimePlatform != Device.Android)
            {
                MessagingCenter.Subscribe<Xamarin.Forms.Application>(typeof(Page), MessageKeys.Pause, app =>
                {
                    var navigationStack = GetNavigationStack();
                    if (navigationStack.Count > 0)
                    {
                        var page = navigationStack[^1] as Page;
                        page?.OnPause();
                    }
                });

                MessagingCenter.Subscribe<Xamarin.Forms.Application>(typeof(Page), MessageKeys.Resume, app =>
                {
                    var navigationStack = GetNavigationStack();
                    if (navigationStack.Count > 0)
                    {
                        var page = navigationStack[^1] as Page;
                        page?.OnResume();
                    }
                });
            }
        }

        public Page()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>()
                .SetUseSafeArea(false);
        }

        protected static IReadOnlyList<Xamarin.Forms.Page> GetNavigationStack()
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _viewModel = BindingContext as ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = OnAppearingAsync();
        }

        private async Task OnAppearingAsync()
        {
            await NextTickAsync();

            if (_hasLoaded)
                OnResume();
            else
            {
                _hasLoaded = true;
                OnStart();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _ = OnDisappearingAsync();
        }

        private async Task OnDisappearingAsync()
        {
            OnPause();

            await NextTickAsync();

            if (App.Current.Status == AppStatus.Stopped)
                OnStop();
            else
            {
                await Task.Delay(TRANSITION_DURATION);
                bool isNotInStack = GetNavigationStack().All(p => p != this);
                if (isNotInStack)
                    OnStop();
            }
        }

        protected virtual void OnStart()
        {
            _viewModel?.OnStart();
        }

        protected virtual void OnResume()
        {
            _viewModel?.OnResume();
        }

        protected override bool OnBackButtonPressed()
        {
            return _viewModel?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
        }

        protected virtual void OnPause()
        {
            _viewModel?.OnPause();
        }

        protected virtual void OnStop()
        {
            _viewModel?.OnStop();
        }

        private static Task NextTickAsync()
        {
            var tcs = new TaskCompletionSource<bool>();

            Device.StartTimer(TimeSpan.FromSeconds(0), () =>
            {
                Device.BeginInvokeOnMainThread(() => tcs.SetResult(true));
                return false;
            });

            return tcs.Task;
        }
    }
}
