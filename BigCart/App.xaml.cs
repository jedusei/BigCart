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
        public static new App Current => Application.Current as App;
        public AppStatus Status { get; private set; }

        static App()
        {
            SharedModule.Instance.Initialize();
        }

        public App()
        {
            InitializeComponent();
        }

        public void Stop()
        {
            Status = AppStatus.Stopped;
        }

        protected override void OnStart()
        {
            DependencyResolver.Get<INavigationService>().Initialize();
            Status = AppStatus.Running;
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
            Status = AppStatus.Running;
            MessagingCenter.Send((Application)this, MessageKeys.Resume);
        }
    }
}
