using BankApplication.Domain.Entities;

namespace BankApplication.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<string> CustomerSignInAsync(string username, string password);
        Task CreateNewCustomerAndAccountAsync(Customer customer, Account account);
        Task<IEnumerable<Customer>> SearchCustomersByNameAsync(string customerName);
    }
}
