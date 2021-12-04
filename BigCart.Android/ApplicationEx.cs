using Android.App;
using Android.Runtime;
using System;

namespace BigCart.Droid
{
    [Application]
    public class ApplicationEx : Application
    {
        public ApplicationEx(IntPtr handle, JniHandleOwnership transer)
           : base(handle, transer)
        {
            return;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            AndroidModule.Instance.Initialize();

#if DEBUG
            // Delete cached icon fonts
            if (System.Diagnostics.Debugger.IsAttached)
            {
                string[] iconFontFilenames = new[] { "AppIcons.ttf" };
                string cacheDirectory = CacheDir.AbsolutePath;
                foreach (string filename in iconFontFilenames)
                    System.IO.File.Delete(System.IO.Path.Combine(cacheDirectory, filename));
            }
#endif
        }
    }
}