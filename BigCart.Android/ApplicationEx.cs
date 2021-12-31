using Android.App;
using Android.Runtime;
using BigCart.Droid.Services;
using BigCart.Services.Platform;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using ExportFontAttribute = Xamarin.Forms.ExportFontAttribute;

namespace BigCart.Droid
{
    [Application]
    public class ApplicationEx : Application
    {
        public ApplicationEx(IntPtr handle, JniHandleOwnership transer)
           : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            ServiceProvider.Init(ConfigureServices);

#if DEBUG
            // Delete cached icon fonts
            if (System.Diagnostics.Debugger.IsAttached)
            {
                IEnumerable<string> fontFileNames = typeof(App)
                    .Assembly
                    .GetCustomAttributes(typeof(ExportFontAttribute), false)
                    .Select(a => (a as ExportFontAttribute).FontFileName);

                string cacheDirectory = CacheDir.AbsolutePath;
                foreach (string filename in fontFileNames)
                    System.IO.File.Delete(System.IO.Path.Combine(cacheDirectory, filename));
            }
#endif
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPlatformService, PlatformService>();
        }
    }
}