using BigCart.DependencyInjection;
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

        static App()
        {
            SharedModule.Instance.Initialize();
        }

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDk1MjMxQDMxMzkyZTMyMmUzMFJWajRpaEhtMzlnQ1FPNStqREVtY1luTm9NeG9sOUljL29mR2U5TE8wWk09");
            InitializeComponent();
            MainPage = new ContentPage();
        }

        public void Stop()
        {
            Status = AppStatus.Stopped;
        }

        protected override void OnStart()
        {
            _ = StartAsync();
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

        private async Task StartAsync()
        {
            if (_isFirstTimeLoad)
                _isFirstTimeLoad = false;
            else
                await Task.Delay(1500);

            DependencyResolver.Get<INavigationService>().Initialize();
            Status = AppStatus.Running;
            MessagingCenter.Send((Application)this, MessageKeys.Start);
        }
    }
}
