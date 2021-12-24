using BigCart.Models;
using System.Threading.Tasks;

namespace BigCart.Services.Transactions
{
    public interface ITransactionService
    {
        Task<Transaction[]> GetTransactionsAsync();
        Task<Transaction> CreateTransactionAsync(CreateTransactionInput input);
    }
}
