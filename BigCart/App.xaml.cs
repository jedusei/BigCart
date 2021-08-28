using BigCart.Messaging;
using Xamarin.Forms;

namespace BigCart
{
    public enum AppStatus
    {
        Unknown,
        Running,
        Paused,
        Resumed,
        Stopped
    }

    public partial class App : Application
    {
        public static new App Current => Application.Current as App;
        public AppStatus Status { get; private set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public void Stop()
        {
            Status = AppStatus.Stopped;
        }

        protected override void OnStart()
        {
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
