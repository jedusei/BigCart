using BigCart.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BigCart.Services.CreditCards
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ObservableCollection<CreditCard> _creditCards;
        private readonly ReadOnlyObservableCollection<CreditCard> _creditCardsReadOnly;

        public CreditCardService()
        {
            _creditCards = new ObservableCollection<CreditCard>
            {
                new()
                {
                    Owner = "Russell Austin",
                    Number = "5678",
                    Cvv = 908,
                    ExpiryDate = new DateTime(2022,1,1),
                    IsDefault = true
                },
                new()
                {
                    Owner = "Russell Austin",
                    Number = "4321",
                    Cvv = 456,
                    ExpiryDate = new DateTime(2024,5,1)
                },
                new()
                {
                    Owner = "Russell Austin",
                    Number = "5432",
                    Cvv = 123,
                    ExpiryDate = new DateTime(2021,12,31)
                }
            };
            _creditCardsReadOnly = new ReadOnlyObservableCollection<CreditCard>(_creditCards);
        }

        public async Task<ReadOnlyObservableCollection<CreditCard>> GetCardsAsync()
        {
            await Task.Delay(1000);
            return _creditCardsReadOnly;
        }

        public async Task AddCardAsync(CreditCard card)
        {
            await Task.Delay(1000);
            if (_creditCards.FirstOrDefault(c => c.Number == card.Number) == null)
                _creditCards.Add(card);
        }

        public async Task UpdateCardAsync(CreditCard updatedCard)
        {
            await Task.Delay(1000);
            CreditCard card = _creditCards.FirstOrDefault(c => c.Number == updatedCard.Number);
            card?.CopyFrom(updatedCard);
        }

        public async Task<CreditCard> GetDefaultCardAsync()
        {
            await Task.Delay(1000);
            return _creditCards.FirstOrDefault(c => c.IsDefault);
        }

        public async Task SetDefaultCardAsync(string cardNumber)
        {
            await Task.Delay(1000);
            cardNumber = cardNumber?.Trim();
            CreditCard card = _creditCards.FirstOrDefault(c => c.Number == cardNumber);
            if (card != null)
            {
                foreach (CreditCard c in _creditCards)
                    c.IsDefault = c == card;
            }
        }
    }
}
