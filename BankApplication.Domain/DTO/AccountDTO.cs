using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Domain.DTO
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public decimal Balance { get; set; }
        public string TypeName { get; set; }
        public string AccountNumber { get; set; }
    }
}
