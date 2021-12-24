using Android.App;
using BigCart.Services.Platform;

namespace BigCart.Droid.Services
{
    public class PlatformService : IPlatformService
    {
        static Activity _activity;

        public static void Init(Activity activity)
        {
            _activity = activity;
        }

        public void Quit()
        {
            _activity?.Finish();
        }
    }
}