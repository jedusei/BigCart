using System;
using Xamarin.UITest;
using Xamarin.UITest.Utils;

namespace BigCart.UITests
{
    public class AppInitializer
    {
        public static IApp App { get; private set; }

        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                App = ConfigureApp.Android
                    .InstalledApp("com.joe.bigcart")
                    .WaitTimes(new WaitTimes())
                    .StartApp();
            }
            else
            {
                App = ConfigureApp.iOS
                    .InstalledApp("com.joe.bigcart")
                    .WaitTimes(new WaitTimes())
                    .StartApp();
            }

            return App;
        }

        private class WaitTimes : IWaitTimes
        {
            public TimeSpan WaitForTimeout => TimeSpan.FromSeconds(5);

            public TimeSpan GestureWaitTimeout => TimeSpan.FromSeconds(1);

            public TimeSpan GestureCompletionTimeout => TimeSpan.FromSeconds(1);
        }
    }
}