using System;
using Xamarin.UITest.Queries;

namespace BigCart.UITests.Views.Pages
{
    public class WelcomePage : Page
    {
        private static readonly Query _signupLink = e => e.Marked("SignupLink");
        private static readonly Query _loginLink = e => e.Marked("LoginLink");
        private static readonly Query _forgotPasswordLink = e => e.Marked("ForgotPasswordLink");
        private static readonly Query _emailField = e => e.TextField("EmailField");
        private static readonly Query _passwordField = e => e.TextField("PasswordField");
        private static readonly Query _phoneNumberField = e => e.TextField("PhoneNumberField");
        private static readonly Query _rememberMeSwitch = e => e.Marked("RememberMe");
        private static readonly Query _isRememberMeChecked = e => _rememberMeSwitch(e).Descendant().Class("SfSwitchElementViewRenderer").Property("getContentDescription").Contains("Off");
        private static readonly Query _loginButton = e => e.Button("LoginButton");
        private static readonly Query _signupButton = e => e.Button("SignupButton");

        private void TapLink(Query query)
        {
            AppResult link = _app.QueryOne(query);
            _app.TapCoordinates(link.Rect.X + link.Rect.Width - 20, link.Rect.CenterY);
        }

        public WelcomePage GoToSignup()
        {
            TapLink(_signupLink);
            return this;
        }

        public WelcomePage GoToLogin()
        {
            TapLink(_loginLink);
            return this;
        }

        public WelcomePage SetEmail(string email)
        {
            _app.EnterText(_emailField, email);
            return this;
        }

        public WelcomePage SetPassword(string password)
        {
            _app.EnterText(_passwordField, password);
            return this;
        }

        public WelcomePage SetPhoneNumber(string phoneNumber)
        {
            _app.EnterText(_phoneNumberField, phoneNumber);
            return this;
        }

        public WelcomePage RememberMe(bool rememberMe)
        {
            bool isChecked = _app.QueryOne(_isRememberMeChecked) is null;
            if (rememberMe != isChecked)
                _app.Tap(_rememberMeSwitch);

            return this;
        }

        public WelcomePage Login()
        {
            _app.Tap(_loginButton);
            return this;
        }

        public WelcomePage Signup()
        {
            _app.Tap(_signupButton);
            return this;
        }

        public WelcomePage ForgotPassword()
        {
            AppResult link = _app.QueryOne(_forgotPasswordLink);
            if (link is not null)
                _app.Tap(link);
            {
                GoToLogin();
                _app.Tap(_forgotPasswordLink);
            }

            return this;
        }
    }
}
