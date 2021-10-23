using BigCart.Messaging;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BigCart.Pages
{
    public abstract class ModalPage : Page
    {
        private const int TRANSITION_DURATION = 250;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform != Device.iOS)
            {
                await Task.Delay(TRANSITION_DURATION);
                Start();
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (Device.RuntimePlatform != Device.iOS)
            {
                Pause();
                await Task.Delay(TRANSITION_DURATION);
                Stop();
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            if (Device.RuntimePlatform != Device.iOS)
            {
                MessagingCenter.Subscribe<Application>(this, MessageKeys.Pause, _ => Pause());
                MessagingCenter.Subscribe<Application>(this, MessageKeys.Resume, _ => Resume());
            }
        }

        protected override void OnStop()
        {
            base.OnStop();

            if (Device.RuntimePlatform != Device.iOS)
            {
                MessagingCenter.Unsubscribe<Application>(this, MessageKeys.Pause);
                MessagingCenter.Unsubscribe<Application>(this, MessageKeys.Resume);
            }
        }
    }
}
