using BigCart.DependencyInjection;
using BigCart.Services.Platform;

namespace BigCart.iOS.Services
{
    public class PlatformService : IPlatformService, ISingletonDependency
    {
        public void Quit()
        {
            // Method intentionally left empty. Closing iOS apps programmatically is against the design guidelines.
        }
    }
}