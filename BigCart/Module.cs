using BigCart.DependencyInjection;
using System.Reflection;

namespace BigCart
{
    public abstract class Module
    {
        private bool _isInitialized;

        public void Initialize()
        {
            if (!_isInitialized)
            {
                DependencyResolver.RegisterDependencies(Assembly.GetCallingAssembly());
                OnInitialize();
                _isInitialized = true;
            }
        }

        protected virtual void OnInitialize()
        {
        }
    }

    class SharedModule : Module
    {
        public static SharedModule Instance { get; } = new SharedModule();
    }
}
