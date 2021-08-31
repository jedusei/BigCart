using BigCart.DependencyInjection;

namespace BigCart.Services.Platform
{
    public interface IPlatformService : IDependency
    {
        void Quit();
    }
}
