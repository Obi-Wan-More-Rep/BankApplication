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
    public class LoanService : ILoanService
    {
        private readonly ILoanRepo _repo;

        public LoanService(ILoanRepo repo)
        {
            _repo = repo;
        }

        public async Task CreateLoanForCustomerAsync(Loan loan)
        {
            await _repo.CreateLoanForCustomerAsync(loan);
        }
    }
}
