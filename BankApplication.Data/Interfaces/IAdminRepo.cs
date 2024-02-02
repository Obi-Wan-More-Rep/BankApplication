using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Domain.Entities;

namespace BankApplication.Data.Interfaces
{
    public interface IAdminRepo
    {
        Task<Admin> GetAdminByUsernameAsync(string username);
    }
}
