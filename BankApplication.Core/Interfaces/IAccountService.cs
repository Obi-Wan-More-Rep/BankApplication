using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Domain.DTO;
using BankApplication.Domain.Entities;

namespace BankApplication.Core.Interfaces
{
    public interface IAccountService
    {
        Task CreateCustomerAccountAsync(int customerId, Account account);

        Task<IEnumerable<Account>> GetAllCustomerAccountsAsync(int customerId);

        Task<Account> GetAccountDetailsAsync(int customerId, int accountId);

        Task<bool> TransferBetweenAccountsAsync(int customerId, TransferBetweenAccountsDTO transferDTO);

        Task<bool> TransferToOtherCustomerAsync(int customerId, TransferToOtherCustomerDTO transferDTO);
    }
}
