using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Core.Interfaces;
using BankApplication.Data.Interfaces;

namespace BankApplication.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _repo;
        private readonly IUtilityMethods _utilityMethods;

        public AdminService(IAdminRepo repo, IUtilityMethods utilityMethods)
        {
            _repo = repo;
            _utilityMethods = utilityMethods;
        }

        public async Task<string> AdminSignInAsync(string username, string enteredPassword)
        {
            // Get hashed password and salt from the database by entering the username
            var admin = await _repo.GetAdminByUsernameAsync(username);

            // Compare the entered password to the hashed one from the database
            if (_utilityMethods.VerifyPassword(enteredPassword, admin.HashedPassword))
            {
                // Generate JWT token for the signed-in admin
                return _utilityMethods.GenerateJwtToken(admin);
            }

            return null;
        }
    }
}
