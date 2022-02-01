using BigCart.UITests.Views.Pages;
using NUnit.Framework;
using Xamarin.UITest;

namespace BigCart.UITests.Tests
{
    [TestFixture(Platform.Android)]
    public class OnboardingPage_Tests : TestClass
    {
        public OnboardingPage_Tests(Platform platform) : base(platform)
        {
        }

        [Test]
        public void OnboardingPageTest()
        {
            new OnboardingPage()
                 .Next()
                 .Invoke(page => Assert.AreEqual(1, page.CurrentSlide))
                 .Back()
                 .Invoke(page => Assert.AreEqual(0, page.CurrentSlide))
                 .GoToEnd()
                 .GetStarted()
                 .WaitUntilHidden();
        }
    }
}
