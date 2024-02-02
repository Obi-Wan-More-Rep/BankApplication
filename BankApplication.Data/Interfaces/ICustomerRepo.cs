using BankApplication.Domain.Entities;

namespace BankApplication.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<string> GetHashedPasswordByUsernameAsync(string username);
        Task<Customer> GetCustomerByUsernameAsync(string username);
        Task<int> GetCustomerIdByUsernameAsync(string username);
        Task CreateNewCustomerAndAccountAsync(Customer customer, Account account);
        Task<IEnumerable<Customer>> SearchCustomersByNameAsync(string customerName);
    }
}
