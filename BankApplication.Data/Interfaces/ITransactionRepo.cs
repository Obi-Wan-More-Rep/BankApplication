using BankApplication.Domain.Entities;

namespace BankApplication.Data.Interfaces
{
    public interface ITransactionRepo
    {
        Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(int accountId);
    }
}
