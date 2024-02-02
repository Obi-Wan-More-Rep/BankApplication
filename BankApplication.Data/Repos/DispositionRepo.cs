
using BankApplication.Data.Interfaces;

namespace BankApplication.Data.Repos
{
    public class DispositionRepo : IDispositionRepo
    {
        private readonly IBankApplicationContext _context;

        public DispositionRepo(IBankApplicationContext context)
        {
            _context = context;
        }
    }
}
