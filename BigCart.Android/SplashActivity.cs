using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using System.Threading.Tasks;

namespace BigCart.Droid
{
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/SplashTheme")]
    public class SplashActivity : AppCompatActivity
    {
        private static bool _firstTime = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);
        }

        protected override async void OnResume()
        {
            base.OnResume();

            if (!_firstTime)
                await Task.Delay(2000);
            else
                _firstTime = false;

            StartActivity(typeof(MainActivity));
        }

        public override void OnBackPressed() { }
    }
}