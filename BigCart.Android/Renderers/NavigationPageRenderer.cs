using Android.Animation;
using Android.Content;
using AndroidX.Fragment.App;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PageEx = BigCart.Pages.Page;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(BigCart.Droid.Renderers.NavigationPageRenderer))]
namespace BigCart.Droid.Renderers
{
    /// <summary>
    /// This class is here to facilitate smoother page transitions on Android.
    /// Sometimes the page transition is not smooth at all, especially when the new page has a lot of controls.
    /// The primary reason for this is that the transition is started at the same time that the controls are being laid out.
    /// The solution is to postpone the fragment transition animation until after the controls have been measured and laid out (https://developer.android.com/guide/fragments/animate#postpone).
    /// This is done in BaseContentPageRenderer.cs.
    /// The secondary reason for the break in transition is that, the default renderer code starts a
    /// fragment transaction to remove the previous page from the screen, before the transition completes (https://github.com/xamarin/Xamarin.Forms/blob/ba7bdcd7dd884e8d1cce23754e14ca33c965acc6/Xamarin.Forms.Platform.Android/AppCompat/NavigationPageRenderer.cs#L885).
    /// This breaks the transition, and the page is displayed immediately.
    /// So that transaction must be delayed until after the transition ends...but how?
    /// After looking at the Xamarin Forms source code for a while, I decided that the best way to do this was to override the Device.StartTimer call in the AddTransitionTimer method.
    /// Device.StartTimer calls Device.PlatformServices.StartTimer, so the solution was to replace Device.PlatformServices with a custom implementation of the
    /// IPlatformServices interface, which first captures the current Device.PlatformServices object, and replaces it with itself. Then all the interface methods (except StartTimer)
    /// are simply implemented by calling the corresponding method on the original Device.PlatformServices object.
    /// When a page is about to be pushed onto the stack, the renderer provides a task to the platform services proxy that completes when the page transition is complete.
    /// The StartTimer implementation uses a condition to check whether it's being called from the AddTransitionTimer method.
    /// If so, it delays the execution of the callback until after the transition completes, otherwise it simply calls the StartTimer method of the original IPlatformServices implementation.
    /// Since XF is open-source, I might create a PR to implement the smooth transitions so that I can remove this "hack", but this will do for now.
    /// </summary>
    public class NavigationPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
    {
        private readonly PlatformServicesProxy _platformServicesProxy;
        private readonly FragmentManager _fragmentManager;
        private TaskCompletionSource<bool> _transitionTcs;

        public NavigationPageRenderer(Context context) : base(context)
        {
            _platformServicesProxy = new PlatformServicesProxy();
            _fragmentManager = context.GetFragmentManager();
            using Animator exitAnimator = AnimatorInflater.LoadAnimator(context, Resource.Animator.fragment_open_exit);
            TransitionDuration = (int?)exitAnimator?.TotalDuration ?? TransitionDuration;
        }

        public Fragment GetCurrentFragment()
        {
            return _fragmentManager.FindFragmentById(Id);
        }

        // Called by the page renderer when the postponed transition is started
        public void NotifyTransitionCompleted()
        {
            _transitionTcs?.TrySetResult(true);
        }

        // This is called in base.OnPushAsync(...)
        protected override void SetupPageTransition(FragmentTransaction transaction, bool isPush)
        {
            base.SetupPageTransition(transaction, isPush);
            transaction.SetReorderingAllowed(isPush);
        }

        protected override async Task<bool> OnPushAsync(Page view, bool animated)
        {
            bool result;

            var navigationStack = view.Navigation.NavigationStack;
            if (navigationStack.Count > 1)
            {
                if (view.Navigation.NavigationStack[^2] is PageEx previousPage)
                    previousPage?.Pause();
            }

            if (!animated)
                result = await base.OnPushAsync(view, false);
            else
            {
                int prevTransitionDuration = TransitionDuration;

                // This will eventually be passed as an argument to the AddTransitionTimer method 
                TransitionDuration = _platformServicesProxy.AddTransitionTimerInterval.Milliseconds;

                _transitionTcs = new TaskCompletionSource<bool>();
                _platformServicesProxy.TransitionTask = _transitionTcs.Task;

                // Push page
                result = await base.OnPushAsync(view, true);

                TransitionDuration = prevTransitionDuration;
                _platformServicesProxy.TransitionTask = null;
            }

            return result;
        }

        protected override async Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            var previousPage = page.Navigation.NavigationStack[^2] as PageEx;

            bool result = await base.OnPopViewAsync(page, animated);

            previousPage?.Resume();

            return result;
        }

        protected override async Task<bool> OnPopToRootAsync(Page page, bool animated)
        {
            var rootPage = page.Navigation.NavigationStack[0] as PageEx;

            bool result = await base.OnPopToRootAsync(page, animated);

            rootPage?.Resume();

            return result;
        }
    }
}