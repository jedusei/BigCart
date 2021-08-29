using Android.App;
using Android.Runtime;
using System;

namespace BigCart.Droid
{
    [Application]
    class ApplicationEx : Application
    {
        public ApplicationEx(IntPtr handle, JniHandleOwnership transer)
           : base(handle, transer)
        {
            AndroidModule.Instance.Initialize();
        }
    }
}