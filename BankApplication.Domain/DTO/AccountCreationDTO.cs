using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Domain.DTO
{
    public class AccountCreationDTO
    {
        public string Frequency { get; set; }
        public int AccountTypesId { get; set; }
    }
}
