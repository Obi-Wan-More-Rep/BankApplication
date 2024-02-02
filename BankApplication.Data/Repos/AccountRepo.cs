using Dapper;
using System.Data;
using BankApplication.Data.Interfaces;
using BankApplication.Domain.Entities;

namespace BankApplication.Data.Repos
{
    public class AccountRepo : IAccountRepo // Correct
    {
        private readonly IBankApplicationContext _context;

        public AccountRepo(IBankApplicationContext context)
        {
            _context = context;
        }

        // Signed in customer creates a new account for themselves
        public async Task CreateCustomerAccountAsync(int customerId, Account account)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId);
                parameters.Add("@Frequency", account.Frequency);
                parameters.Add("@AccountTypesId", account.AccountTypesId);
                parameters.Add("@AccountNumber", account.AccountNumber);

                await db.ExecuteAsync("sp_CreateCustomerAccount", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Return a list of all accounts associated with a customer that's signed in, including AccountType information
        public async Task<IEnumerable<Account>> GetAllCustomerAccountsAsync(int customerId)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId);

                var result = await db.QueryAsync<Account, AccountType, Account>(
                    "sp_GetCustomerAccounts",
                    (account, accountType) =>
                    {
                        account.AccountType = accountType;
                        return account;
                    },
                    parameters,
                    splitOn: "AccountTypesId",
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }


        // Returns a single account including AccountType information
        public async Task<Account> GetAccountDetailsAsync(int accountId)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AccountId", accountId);

                var result = await db.QueryAsync<Account, AccountType, Account>(
                    "sp_GetAccountDetails",
                    (account, accountType) =>
                    {
                        if (accountType != null)
                        {
                            account.AccountType = accountType;
                        }
                        return account;
                    },
                    parameters,
                    splitOn: "AccountTypesId",
                    commandType: CommandType.StoredProcedure
                );

                return result.FirstOrDefault();
            }
        }

        // Returns a single account based on account number including AccountType information
        public async Task<Account> GetAccountDetailsAsync(string accountNumber)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AccountNumber", accountNumber);

                var result = await db.QueryAsync<Account, AccountType, Account>(
                    "sp_GetAccountDetailsByAccountNumber",
                    (account, accountType) =>
                    {
                        if (accountType != null)
                        {
                            account.AccountType = accountType;
                        }
                        return account;
                    },
                    parameters,
                    splitOn: "AccountTypesId",
                    commandType: CommandType.StoredProcedure
                );

                return result.FirstOrDefault();
            }
        }

        // Transfer money between two accounts of the same customer
        public async Task TransferBetweenAccountsAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FromAccountId", fromAccountId);
                parameters.Add("@ToAccountId", toAccountId);
                parameters.Add("@Amount", amount);

                await db.ExecuteAsync("sp_TransferBetweenAccounts", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Transfer money to another customer's account
        public async Task TransferToOtherCustomerAsync(int fromAccountId, string toAccountNumber, decimal amount)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FromAccountId", fromAccountId);
                parameters.Add("@ToAccountNumber", toAccountNumber);
                parameters.Add("@Amount", amount);

                await db.ExecuteAsync("sp_TransferToOtherCustomer", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
