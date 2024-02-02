using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Core.Interfaces;
using BankApplication.Data.Interfaces;
using BankApplication.Domain.Entities;

namespace BankApplication.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepo _repo;

        public TransactionService(ITransactionRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Transaction>> GetAccountTransactions(int accountId)
        {
            return await _repo.GetAccountTransactionsAsync(accountId);
        }
    }
}
