using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Core.Interfaces;
using BankApplication.Domain.Entities;

namespace BankApplication.Core.Utilities
{// or could name the map this class is in to "Helpers"
    public class UtilityMethods : IUtilityMethods
    {
        private readonly IConfiguration _configuration;

        public UtilityMethods(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            try
            {
                // If entered password matches the stored hashed password with salt, then return true.
                return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GenerateAccountNumber()
        {
            // Using a combination of timestamp and random number for simplicity
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string randomNumber = new Random().Next(1000, 9999).ToString();

            return timestamp + randomNumber;
        }

        // Generate a temporary 180 min token
        public string GenerateJwtToken(object user)
        {
            List<Claim> claims = new List<Claim>();

            if (user is Admin admin && admin.Role == "Admin")
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Admin")
                };
            }
            else if (user is Customer customer && customer.Role == "Customer")
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()), // User ID claim
                    new Claim(ClaimTypes.Role, "Customer") // Role claim
                };
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpiryInMinutes"])),
                signingCredentials: signinCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

    }
}
