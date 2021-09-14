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
            // Clear cache directory so that fonts will be updated.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                string cacheDirectory = CacheDir.AbsolutePath;
                System.IO.Directory.Delete(cacheDirectory, true);
                System.IO.Directory.CreateDirectory(cacheDirectory);
            }
#endif
        }
    }
}