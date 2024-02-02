using Dapper;
using System.Data;
using BankApplication.Data.Interfaces;
using BankApplication.Domain.Entities;

namespace BankApplication.Data.Repos
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly IBankApplicationContext _context;

        public TransactionRepo(IBankApplicationContext context)
        {
            _context = context;
        }

        // Get all transactions for a specific account
        public async Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(int accountId)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AccountId", accountId);

                return await db.QueryAsync<Transaction>("sp_GetAccountTransactions", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
