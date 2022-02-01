using BigCart.UITests.Views.Pages;
using NUnit.Framework;
using Xamarin.UITest;

namespace BigCart.UITests
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
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
