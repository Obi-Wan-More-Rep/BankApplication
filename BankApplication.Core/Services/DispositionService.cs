using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Core.Interfaces;
using BankApplication.Data.Interfaces;

namespace BankApplication.Core.Services
{
    public class DispositionService : IDispositionService
    {
        private readonly IDispositionRepo _dispositionRepo;

        public DispositionService(IDispositionRepo dispositionRepo)
        {
            _dispositionRepo = dispositionRepo;
        }
    }
}
