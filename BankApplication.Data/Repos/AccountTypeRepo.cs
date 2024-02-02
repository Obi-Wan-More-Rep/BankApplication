using BankApplication.Data.Interfaces;

namespace BankApplication.Data.Repos
{
    public class AccountTypeRepo : IAccountTypeRepo
    {
        private readonly IBankApplicationContext _context;

        public AccountTypeRepo(IBankApplicationContext context)
        {
            _context = context;
        }
    }
}
