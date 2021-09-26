using BigCart.DependencyInjection;
using BigCart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigCart.Services.Transactions
{
    public class TransactionService : ITransactionService, ISingletonDependency
    {
        private static int _nextTransactionId = 123456;
        private readonly List<Transaction> _transactions = new()
        {
            new()
            {
                PaymentMethod = PaymentMethod.CreditCard,
                CreditCardType = CreditCardType.Mastercard,
                Amount = 89.99f
            },
            new()
            {
                PaymentMethod = PaymentMethod.CreditCard,
                CreditCardType = CreditCardType.Visa,
                Amount = 109.99f
            },
            new()
            {
                PaymentMethod = PaymentMethod.Paypal,
                Amount = 567
            },
            new()
            {
                PaymentMethod = PaymentMethod.ApplePay,
                Amount = 480
            },
            new()
            {
                PaymentMethod = PaymentMethod.CreditCard,
                CreditCardType = CreditCardType.Mastercard,
                Amount = 50
            }
        };

        public async Task<Transaction[]> GetTransactionsAsync()
        {
            await Task.Delay(1000);
            return _transactions.ToArray();
        }

        public async Task<Transaction> CreateTransactionAsync(CreateTransactionInput input)
        {
            await Task.Delay(1000);

            Transaction transaction = new()
            {
                Id = _nextTransactionId++,
                Amount = input.Amount,
                PaymentMethod = input.PaymentMethod,
                CreditCardType = input.CreditCardType
            };
            _transactions.Insert(0, transaction);

            return transaction;
        }
    }
}
