using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Core.Interfaces
{
    public interface IUtilityMethods
    {
        bool VerifyPassword(string enteredPassword, string hashedPassword);
        string GenerateAccountNumber();
        string GenerateJwtToken(object user);
    }
}
