using BigCart.UITests.Views.Pages;
using NUnit.Framework;
using Xamarin.UITest;

namespace BigCart.UITests.Tests
{
    [TestFixture(Platform.Android)]
    public class WelcomePage_Tests : TestClass
    {
        public WelcomePage_Tests(Platform platform) : base(platform)
        {
        }

        public override void BeforeEachTest()
        {
            base.BeforeEachTest();
            _app.Invoke("GoToPage", nameof(WelcomePage));
        }

        [Test]
        public void LoginTest()
        {
            new WelcomePage()
                .GoToLogin()
                .SetEmail("test@gmail.com")
                .SetPassword("Abc1234")
                .Invoke(_app.DismissKeyboard)
                .RememberMe(true)
                .Login()
                .WaitUntilHidden();
        }

        [Test]
        public void SignupTest()
        {
            new WelcomePage()
                .GoToSignup()
                .SetEmail("test@gmail.com")
                .SetPhoneNumber("0201234567")
                .SetPassword("Abc1234")
                .Invoke(_app.DismissKeyboard)
                .Signup()
                .WaitUntilHidden();
        }

        [Test]
        public void ForgotPasswordTest()
        {
            new WelcomePage()
                .ForgotPassword()
                .WaitUntilHidden();
        }
    }
}
