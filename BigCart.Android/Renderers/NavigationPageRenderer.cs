using Android.Animation;
using Android.Content;
using AndroidX.Fragment.App;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(BigCart.Droid.Renderers.NavigationPageRenderer))]
namespace BigCart.Droid.Renderers
{
    public class NavigationPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
    {
        private FragmentManager _fragmentManager;
        private int _enterTransitionDuration, _exitTransitionDuration;
        private TaskCompletionSource<bool> _enterTransitionTcs;

        public NavigationPageRenderer(Context context) : base(context)
        {
            _fragmentManager = context.GetFragmentManager();
            using Animator enterAnimator = AnimatorInflater.LoadAnimator(context, Resource.Animator.fragment_open_enter);
            using Animator exitAnimator = AnimatorInflater.LoadAnimator(context, Resource.Animator.fragment_open_exit);
            _enterTransitionDuration = (int?)enterAnimator?.TotalDuration ?? TransitionDuration;
            _exitTransitionDuration = (int?)exitAnimator?.TotalDuration ?? TransitionDuration;
        }

        public Fragment GetCurrentFragment()
        {
            return _fragmentManager.FindFragmentById(Id);
        }

        public void NotifyEnterTransitionStarted()
        {
            _enterTransitionTcs?.TrySetResult(true);
        }

        protected override void SetupPageTransition(FragmentTransaction transaction, bool isPush)
        {
            base.SetupPageTransition(transaction, isPush);
            transaction.SetReorderingAllowed(isPush);
            TransitionDuration = isPush ? _enterTransitionDuration : _exitTransitionDuration;
        }

        protected override async Task<bool> OnPushAsync(Page view, bool animated)
        {
            if (!animated)
                return await base.OnPushAsync(view, animated);

            Task transitionTask = WaitForTransitionToCompleteAsync();

            // Push page
            bool result = await base.OnPushAsync(view, animated);

            await transitionTask;

            return result;
        }

        private async Task WaitForTransitionToCompleteAsync()
        {
            _enterTransitionTcs = new TaskCompletionSource<bool>();

            // Wait for enter transition to start
            await _enterTransitionTcs.Task;

            // Wait for enter transition to complete
            await Task.Delay(_enterTransitionDuration);

            _enterTransitionTcs = null;
        }
    }
}