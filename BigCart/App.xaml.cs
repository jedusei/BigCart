using BigCart.Messaging;
using BigCart.Services.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BigCart
{
    public enum AppStatus
    {
        Starting,
        Running,
        Paused,
        Resumed,
        Stopped
    }

    public partial class App : Application
    {
        private static bool _isFirstTimeLoad = true;
        public static new App Current => Application.Current as App;
        public AppStatus Status { get; private set; }

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDk1MjMxQDMxMzkyZTMyMmUzMFJWajRpaEhtMzlnQ1FPNStqREVtY1luTm9NeG9sOUljL29mR2U5TE8wWk09");
            InitializeComponent();
            MainPage = new ContentPage();
        }

        public void Stop()
        {
            Status = AppStatus.Stopped;
            MessagingCenter.Send((Application)this, MessageKeys.Stop);
        }

        protected override async void OnStart()
        {
            if (_isFirstTimeLoad)
                _isFirstTimeLoad = false;
            else
                await Task.Delay(1500);

            ServiceProvider.GetService<INavigationService>().Initialize();
            Status = AppStatus.Running;
            if (Device.RuntimePlatform == Device.iOS)
                MessagingCenter.Send((Application)this, MessageKeys.Start); // On Android this will be called by the page renderer right before it is drawn.
        }

        protected override void OnSleep()
        {
            if (Status == AppStatus.Running)
            {
                Status = AppStatus.Paused;
                MessagingCenter.Send((Application)this, MessageKeys.Pause);
            }
        }

        protected override void OnResume()
        {
            if (Status == AppStatus.Paused)
            {
                Status = AppStatus.Running;
                MessagingCenter.Send((Application)this, MessageKeys.Resume);
            }
        }
    }
}
