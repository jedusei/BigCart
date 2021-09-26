using BigCart.DependencyInjection;
using BigCart.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BigCart.Services.CreditCards
{
    public interface ICreditCardService : IDependency
    {
        Task<ReadOnlyObservableCollection<CreditCard>> GetCardsAsync();
        Task AddCardAsync(CreditCard card);
        void SetDefaultCard(string cardNumber);
    }
}
