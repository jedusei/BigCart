using BigCart.DependencyInjection;
using BigCart.Models;
using System.Threading.Tasks;

namespace BigCart.Services.Transactions
{
    public interface ITransactionService : IDependency
    {
        Task<Transaction[]> GetTransactionsAsync();
        Task RegisterTransactionAsync(Transaction transaction);
    }
}
