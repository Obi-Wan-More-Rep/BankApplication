using BankApplication.Domain.Entities;

namespace BankApplication.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAccountTransactions(int accountId);
    }
}
