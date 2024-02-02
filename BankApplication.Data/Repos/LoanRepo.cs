using Dapper;
using System.Data;
using BankApplication.Data.Interfaces;
using BankApplication.Domain.Entities;

namespace BankApplication.Data.Repos
{
    public class LoanRepo : ILoanRepo
    {
        private readonly IBankApplicationContext _context;

        public LoanRepo(IBankApplicationContext context)
        {
            _context = context;
        }

        // Create a loan for a customer
        public async Task CreateLoanForCustomerAsync(Loan loan)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AccountId", loan.AccountId);
                parameters.Add("@Amount", loan.Amount);
                parameters.Add("@Duration", loan.Duration);
                parameters.Add("@Payments", loan.Payments);
                parameters.Add("@Status", loan.Status);

                await db.ExecuteAsync("sp_CreateLoanForCustomer", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
