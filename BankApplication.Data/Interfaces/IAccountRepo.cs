using BankApplication.Domain.Entities;

namespace BankApplication.Data.Interfaces
{
    public interface IAccountRepo // Correct
    {
        Task CreateCustomerAccountAsync(int customerId, Account account);
        Task<IEnumerable<Account>> GetAllCustomerAccountsAsync(int customerId);
        Task<Account> GetAccountDetailsAsync(int accountId);
        Task<Account> GetAccountDetailsAsync(string accountNumber);
        Task TransferBetweenAccountsAsync(int fromAccountId, int toAccountId, decimal amount);
        Task TransferToOtherCustomerAsync(int fromAccountId, string toAccountNumber, decimal amount);
    }
}
