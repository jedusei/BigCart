using BigCart.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace BigCart.Pages
{
    public enum PageStatus
    {
        Pending,
        Active,
        Paused,
        Stopped
    }

    public enum StatusBarStyle
    {
        LightContent,
        DarkContent
    }

    public abstract class Page : ContentPage
    {
        public static readonly BindableProperty StatusBarStyleProperty = BindableProperty.Create(nameof(StatusBarStyle), typeof(StatusBarStyle), typeof(Page), StatusBarStyle.DarkContent);
        public static readonly BindableProperty SoftInputModeProperty = BindableProperty.Create(nameof(WindowSoftInputModeAdjust), typeof(WindowSoftInputModeAdjust), typeof(Page), WindowSoftInputModeAdjust.Pan);

        public ViewModel ViewModel { get; private set; }
        public PageStatus Status { get; protected set; } = PageStatus.Pending;
        public StatusBarStyle StatusBarStyle
        {
            get => (StatusBarStyle)GetValue(StatusBarStyleProperty);
            set => SetValue(StatusBarStyleProperty, value);
        }
        public WindowSoftInputModeAdjust WindowSoftInputModeAdjust
        {
            get => (WindowSoftInputModeAdjust)GetValue(SoftInputModeProperty);
            set => SetValue(SoftInputModeProperty, value);
        }

        public Page()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(false);
        }

        public void Start()
        {
            if (Status == PageStatus.Pending)
            {
                Status = PageStatus.Active;
                OnStart();
            }
        }

        public void Pause()
        {
            if (Status == PageStatus.Active)
            {
                Status = PageStatus.Paused;
                OnPause();
            }
        }

        public void Resume()
        {
            if (Status == PageStatus.Paused)
            {
                Status = PageStatus.Active;
                OnResume();
            }
        }

        public void Stop()
        {
            if (Status == PageStatus.Paused)
            {
                Status = PageStatus.Stopped;
                OnStop();
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ViewModel = BindingContext as ViewModel;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent == null && Device.RuntimePlatform == Device.iOS)
                Stop();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust);
        }

        protected virtual void OnStart()
        {
            foreach (ToolbarItem item in ToolbarItems)
                item.BindingContext = BindingContext;

            ViewModel?.OnStart();
        }

        protected virtual void OnResume()
        {
            InputTransparent = false;
            ViewModel?.OnResume();
        }

        protected override bool OnBackButtonPressed()
        {
            return ViewModel?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
        }

        protected virtual void OnPause()
        {
            InputTransparent = true;
            ViewModel?.OnPause();
        }

        protected virtual void OnStop()
        {
            ViewModel?.OnStop();
        }
    }
}
