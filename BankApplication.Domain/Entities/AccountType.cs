using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Domain.Entities
{
    public class AccountType // Correct
    {
        public int AccountTypeId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        // Relationships
        public List<Account> Accounts { get; set; }
    }
}
