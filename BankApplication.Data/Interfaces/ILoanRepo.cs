using BankApplication.Domain.Entities;

namespace BankApplication.Data.Interfaces
{
    public interface ILoanRepo
    {
        Task CreateLoanForCustomerAsync(Loan loan);
    }
}
