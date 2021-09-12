using Xamarin.Forms;

namespace BigCart.Controls
{
    public class SwipeViewEx : SwipeView
    {
        private const string MSG_OPENED = nameof(MSG_OPENED);
        private const string MSG_CLOSE = nameof(MSG_CLOSE);

        public SwipeViewEx()
        {
            SwipeEnded += OnSwipeEnded;

            MessagingCenter.Subscribe<SwipeViewEx>(this, MSG_OPENED, sender =>
            {
                if (sender != this)
                    Close();
            });

            MessagingCenter.Subscribe<object, object>(this, MSG_CLOSE, (_, bc) =>
            {
                if (bc == BindingContext)
                    Close();
            });
        }

        public static void Close(object bindingContext)
        {
            MessagingCenter.Send<object, object>(typeof(SwipeViewEx), MSG_CLOSE, bindingContext);
        }

        private void OnSwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            if (e.IsOpen)
                MessagingCenter.Send(this, MSG_OPENED);
        }
    }
}
