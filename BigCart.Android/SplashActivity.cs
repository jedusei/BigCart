using Android.App;
using AndroidX.AppCompat.App;
using System.Threading.Tasks;

namespace BigCart.Droid
{
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/SplashTheme")]
    public class SplashActivity : AppCompatActivity
    {
        protected override async void OnResume()
        {
            base.OnResume();

            await Task.Delay(2000);

            StartActivity(typeof(MainActivity));
        }

        public override void OnBackPressed() { }
    }
}