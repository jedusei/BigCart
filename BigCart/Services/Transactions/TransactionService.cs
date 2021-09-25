using BigCart.DependencyInjection;
using BigCart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigCart.Services.Transactions
{
    public class TransactionService : ITransactionService, ISingletonDependency
    {
        private readonly List<Transaction> _transactions = new()
        {
            new()
            {
                CreditCardType = CreditCardType.Mastercard,
                Amount = 89.99f
            },
            new()
            {
                CreditCardType = CreditCardType.Visa,
                Amount = 109.99f
            },
            new()
            {
                CreditCardType = CreditCardType.Paypal,
                Amount = 567
            },
            new()
            {
                CreditCardType = CreditCardType.ApplePay,
                Amount = 480
            },
            new()
            {
                CreditCardType = CreditCardType.Mastercard,
                Amount = 50
            }
        };

        public async Task<Transaction[]> GetTransactionsAsync()
        {
            await Task.Delay(1000);
            return _transactions.ToArray();
        }

        public async Task RegisterTransactionAsync(Transaction transaction)
        {
            await Task.Delay(1000);
            if (_transactions.Find(t => t.Id == transaction.Id) == null)
                _transactions.Insert(0, transaction);
        }
    }
}
