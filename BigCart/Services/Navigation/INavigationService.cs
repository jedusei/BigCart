using BigCart.DependencyInjection;
using System.Threading.Tasks;

namespace BigCart.Services.Navigation
{
    public interface INavigationService : IDependency
    {
        void Initialize();
    }
}
