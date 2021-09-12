using Xamarin.Forms;

namespace BigCart.Controls
{
    public class SwipeViewEx : SwipeView
    {
        private const string MSG_OPENED = nameof(MSG_OPENED);
        private const string MSG_CLOSE = nameof(MSG_CLOSE);

        private Page Page
        {
            get
            {
                Element parent = Parent;

                while (parent != null)
                {
                    if (parent is Page page)
                        return page;

                    parent = parent.Parent;
                }

                return null;
            }
        }

        public SwipeViewEx()
        {
            SwipeEnded += OnSwipeEnded;

            MessagingCenter.Subscribe<SwipeViewEx, Page>(this, MSG_OPENED, (sender, page) =>
            {
                if (sender != this && page == Page)
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
                MessagingCenter.Send(this, MSG_OPENED, Page);
        }
    }
}
