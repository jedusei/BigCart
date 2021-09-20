using BigCart.DependencyInjection;
using BigCart.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BigCart.Services.Reviews
{
    public interface IReviewService : IDependency
    {
        Task<ReadOnlyObservableCollection<Review>> GetReviewsAsync(Product product);
        Task AddReviewAsync(Product product, Review review);
    }
}
