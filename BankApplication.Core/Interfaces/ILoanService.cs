using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Domain.Entities;

namespace BankApplication.Core.Interfaces
{
    public interface ILoanService
    {
        Task CreateLoanForCustomerAsync(Loan loan);
    }
}
