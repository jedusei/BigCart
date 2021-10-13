using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BigCart.Droid
{
    internal class PlatformServicesProxy : IPlatformServices
    {
        private readonly IPlatformServices _originalPlatformServices;

        public bool IsInvokeRequired => _originalPlatformServices.IsInvokeRequired;
        public OSAppTheme RequestedTheme => _originalPlatformServices.RequestedTheme;
        public string RuntimePlatform => _originalPlatformServices.RuntimePlatform;
        public TimeSpan AddTransitionTimerInterval { get; } = TimeSpan.FromMilliseconds(323);
        public Task EnterTransitionTask { get; set; }

        public PlatformServicesProxy()
        {
            _originalPlatformServices = Device.PlatformServices;
            Device.PlatformServices = this;
        }

        public async void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            if (EnterTransitionTask != null && interval == AddTransitionTimerInterval)
            {
                await EnterTransitionTask;
                callback.Invoke();
                return;
            }

            _originalPlatformServices.StartTimer(interval, callback);
        }

        public void BeginInvokeOnMainThread(Action action)
        {
            _originalPlatformServices.BeginInvokeOnMainThread(action);
        }

        public Ticker CreateTicker()
        {
            return _originalPlatformServices.CreateTicker();
        }

        public Assembly[] GetAssemblies()
        {
            return _originalPlatformServices.GetAssemblies();
        }

        public string GetHash(string input)
        {
            return _originalPlatformServices.GetHash(input);
        }

        [Obsolete("GetMD5Hash is obsolete as of version 4.7.0")]
        public string GetMD5Hash(string input)
        {
            return _originalPlatformServices.GetMD5Hash(input);
        }

        public Color GetNamedColor(string name)
        {
            return _originalPlatformServices.GetNamedColor(name);
        }

        public double GetNamedSize(NamedSize size, Type targetElementType, bool useOldSizes)
        {
            return _originalPlatformServices.GetNamedSize(size, targetElementType, useOldSizes);
        }

        public SizeRequest GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
        {
            return _originalPlatformServices.GetNativeSize(view, widthConstraint, heightConstraint);
        }

        public Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
        {
            return _originalPlatformServices.GetStreamAsync(uri, cancellationToken);
        }

        public IIsolatedStorageFile GetUserStoreForApplication()
        {
            return _originalPlatformServices.GetUserStoreForApplication();
        }

        public void OpenUriAction(Uri uri)
        {
            _originalPlatformServices.OpenUriAction(uri);
        }

        public void QuitApplication()
        {
            _originalPlatformServices.QuitApplication();
        }
    }
}