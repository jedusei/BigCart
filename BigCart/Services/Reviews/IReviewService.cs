using BigCart.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BigCart.Services.Reviews
{
    public interface IReviewService
    {
        Task<ReadOnlyObservableCollection<Review>> GetReviewsAsync(Product product);
        Task AddReviewAsync(Product product, Review review);
    }
}
