using Android.App;
using BigCart.DependencyInjection;
using BigCart.Services.Platform;

namespace BigCart.Droid.Services
{
    public class PlatformService : IPlatformService, ISingletonDependency
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