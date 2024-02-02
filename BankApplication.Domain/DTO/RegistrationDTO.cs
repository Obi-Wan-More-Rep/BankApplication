using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Domain.DTO
{
    public class RegistrationDTO
    {
        public CustomerRegistrationDTO CustomerDto { get; set; }
        public AccountCreationDTO AccountDto { get; set; }
    }
}
