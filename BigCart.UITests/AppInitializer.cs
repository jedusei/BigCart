using System;
using System.IO;
using System.Reflection;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

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
                    .StartApp();
            }
            else
            {
                App = ConfigureApp.iOS
                    .InstalledApp("com.joe.bigcart")
                    .StartApp();
            }

            return App;
        }
    }
}