using BankApplication.Core.Interfaces;
using BankApplication.Data.Interfaces;
using BankApplication.Domain.DTO;
using BankApplication.Domain.Entities;

namespace BankApplication.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _repo;
        private readonly IUtilityMethods _utilityMethods;

        public AccountService(IAccountRepo repo, IUtilityMethods utilityMethods)
        {
            _repo = repo;
            _utilityMethods = utilityMethods;
        }

        public async Task CreateCustomerAccountAsync(int customerId, Account account)
        {
            // Generate Account Number and store it in the account object property
            account.AccountNumber = _utilityMethods.GenerateAccountNumber();

            // Create the account
            await _repo.CreateCustomerAccountAsync(customerId, account);
        }

        public async Task<IEnumerable<Account>> GetAllCustomerAccountsAsync(int customerId)
        {
            return await _repo.GetAllCustomerAccountsAsync(customerId);
        }

        public async Task<Account> GetAccountDetailsAsync(int customerId, int accountId)
        {
            // Get a list of all accounts associated with the customer
            var customerAccounts = await _repo.GetAllCustomerAccountsAsync(customerId);

            // Check if the customer has any account matching the accountId
            var account = customerAccounts.FirstOrDefault(a => a.AccountId == accountId);

            if (account != null)
            {
                // If the account is found, return its details
                return await _repo.GetAccountDetailsAsync(accountId);
            }

            return null;
        }

        public async Task<bool> TransferBetweenAccountsAsync(int customerId, TransferBetweenAccountsDTO transferDTO)
        {
            // Get a list of all accounts associated with the customer
            var customerAccounts = await _repo.GetAllCustomerAccountsAsync(customerId);

            // Check if the customer has accounts from the list above involved in the transfer
            var fromAccount = customerAccounts.FirstOrDefault(a => a.AccountId == transferDTO.FromAccountId);
            var toAccount = customerAccounts.FirstOrDefault(a => a.AccountId == transferDTO.ToAccountId);

            if (fromAccount != null && toAccount != null)
            {
                // Perform the transfer
                await _repo.TransferBetweenAccountsAsync(transferDTO.FromAccountId, transferDTO.ToAccountId, transferDTO.Amount);
                return true;
            }

            return false;
        }

        public async Task<bool> TransferToOtherCustomerAsync(int customerId, TransferToOtherCustomerDTO transferDTO)
        {
            // Get a list of all accounts associated with the customer
            var customerAccounts = await _repo.GetAllCustomerAccountsAsync(customerId);

            // Check if the customer has one account from the list above involved in the transfer
            var fromAccount = customerAccounts.FirstOrDefault(a => a.AccountId == transferDTO.FromAccountId);

            // check if the other customer's account you want to tranfer money to exists
            var ToAccountNumber = await _repo.GetAccountDetailsAsync(transferDTO.ToAccountNumber);

            if (fromAccount != null && ToAccountNumber != null)
            {
                // Perform the transfer to another customer
                await _repo.TransferToOtherCustomerAsync(transferDTO.FromAccountId, transferDTO.ToAccountNumber, transferDTO.Amount);
                return true;
            }

            return false;
        }

    }
}
