using Microsoft.Extensions.DependencyInjection;
using System;

namespace BigCart
{
    public class ServiceProvider
    {
        private static IServiceProvider _instance;

        internal static void Init(IServiceProvider instance)
        {
            _instance = instance;
        }

        public static T GetService<T>()
        {
            return _instance.GetRequiredService<T>();
        }

        public static object GetService(Type serviceType)
        {
            return _instance.GetRequiredService(serviceType);
        }
    }
}
