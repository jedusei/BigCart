using Android.Content;
using AndroidX.Fragment.App;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(BigCart.Droid.Renderers.NavigationPageRenderer))]
namespace BigCart.Droid.Renderers
{
    public class NavigationPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
    {
        public static NavigationPageRenderer Instance { get; private set; }

        public NavigationPageRenderer(Context context) : base(context)
        {
            Instance = this;
        }

        protected override void SetupPageTransition(FragmentTransaction transaction, bool isPush)
        {
            base.SetupPageTransition(transaction, isPush);
            transaction.SetReorderingAllowed(isPush);
        }
    }
}