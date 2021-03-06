using BigCart.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BigCart.Services.CreditCards
{
    public interface ICreditCardService
    {
        Task<ReadOnlyObservableCollection<CreditCard>> GetCardsAsync();
        Task AddCardAsync(CreditCard card);
        Task UpdateCardAsync(CreditCard updatedCard);
        Task<CreditCard> GetDefaultCardAsync();
        Task SetDefaultCardAsync(string cardNumber);
    }
}
