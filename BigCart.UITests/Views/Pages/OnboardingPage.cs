using System;
using System.Threading;
using Xamarin.UITest.Queries;

namespace BigCart.UITests.Views.Pages
{
    public class OnboardingPage : Page
    {
        private static readonly Func<AppQuery, AppTypedSelector<int>> _currentSlide = e => e.Marked("CarouselView").Invoke("getLayoutManager").Invoke("findFirstVisibleItemPosition").Value<int>();
        private static readonly Query _nextButton = e => e.Button("Next");
        private static readonly Query _getStartedButton = e => e.Button("Get Started");

        public int CurrentSlide => _app.QueryOne(_currentSlide);

        public OnboardingPage Next()
        {
            _app.Tap(_nextButton);
            Thread.Sleep(300);
            return this;
        }

        public OnboardingPage GoToEnd()
        {
            for (int i = CurrentSlide + 1; i < 4; i++)
                Next();

            return this;
        }

        public OnboardingPage Back()
        {
            _app.Back();
            return this;
        }

        public OnboardingPage GetStarted()
        {
            _app.Tap(_getStartedButton);
            return this;
        }
    }
}
