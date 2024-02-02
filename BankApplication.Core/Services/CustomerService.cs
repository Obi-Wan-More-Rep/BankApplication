using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankApplication.Core.Interfaces;
using BankApplication.Data.Interfaces;
using BCrypt;
using BankApplication.Core.Utilities;
using Microsoft.Extensions.Configuration;
using BankApplication.Domain.Entities;

namespace BankApplication.Core.Services
{
    public class CustomerService : ICustomerService
    {
        
        private readonly ICustomerRepo _repo;
        private readonly IUtilityMethods _utilityMethods;

        public CustomerService(ICustomerRepo repo, IUtilityMethods utilityMethods)
        {
            _repo = repo;
            _utilityMethods = utilityMethods;
        }


        public async Task<string> CustomerSignInAsync(string username, string enteredPassword)
        {
            // Get hashed password and salt from the database by entering the username
            var customer = await _repo.GetCustomerByUsernameAsync(username);

            // Compare the entered password to the hashed one from the database
            if (_utilityMethods.VerifyPassword(enteredPassword, customer.HashedPassword))
            {  
                // Generate JWT token for the signed-in customer
                return _utilityMethods.GenerateJwtToken(customer);
            }

            return null;
        }

        public async Task CreateNewCustomerAndAccountAsync(Customer customer, Account account)
        {
            try
            {
                // Generate Account Number and store it in the account object property
                account.AccountNumber = _utilityMethods.GenerateAccountNumber();

                // Generate Salt
                string salt = BCrypt.Net.BCrypt.GenerateSalt();

                // Hash password with the salt // Hash password and concatenate it with the salt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(customer.HashedPassword, salt);

                // Store the hashed password and salt in the customer object
                customer.HashedPassword = hashedPassword;

                // Create the Customer and account 
                await _repo.CreateNewCustomerAndAccountAsync(customer, account);
            }
            catch (Exception ex)
            {
                throw; // could change this to a false later if I make the method a bool
            }
        }

        public async Task<IEnumerable<Customer>> SearchCustomersByNameAsync(string customerName)
        {
            return await _repo.SearchCustomersByNameAsync(customerName);
        }
    }
}
