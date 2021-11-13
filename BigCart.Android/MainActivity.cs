using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using BigCart.Messaging;
using System.Threading.Tasks;
using FormsApplication = Xamarin.Forms.Application;
using MessagingCenter = Xamarin.Forms.MessagingCenter;

namespace BigCart.Droid
{
    [Activity(
        MainLauncher = true,
        Theme = "@style/MainTheme",
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize
    )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetupWindow();

            Services.PlatformService.Init(this);
            Acr.UserDialogs.UserDialogs.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Syncfusion.XForms.Android.PopupLayout.SfPopupLayoutRenderer.Init();

            MessagingCenter.Subscribe<FormsApplication>(this, MessageKeys.Start, OnAppStart);
            LoadApplication(new App());
        }

        private async void OnAppStart(FormsApplication application)
        {
            MessagingCenter.Unsubscribe<FormsApplication>(this, MessageKeys.Start);
            await Task.Delay(1000);
            Window?.DecorView.SetBackgroundColor(Color.White);
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (IsFinishing)
                App.Current?.Stop();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void SetupWindow()
        {
            SystemUiFlags systemUiVisibility = SystemUiFlags.LightStatusBar
                | SystemUiFlags.LayoutStable
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.LayoutHideNavigation;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                systemUiVisibility |= SystemUiFlags.LightNavigationBar;

#pragma warning disable CS0618 // Type or member is obsolete
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)systemUiVisibility;
#pragma warning restore CS0618 // Type or member is obsolete

            var winParams = Window.Attributes;
            winParams.Flags &= ~(WindowManagerFlags.TranslucentStatus | WindowManagerFlags.TranslucentNavigation);
            Window.Attributes = winParams;
        }
    }
}