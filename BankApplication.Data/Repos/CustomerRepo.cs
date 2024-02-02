using Dapper;
using System.Data;
using System.Runtime.CompilerServices;
using BankApplication.Data.Interfaces;
using BankApplication.Domain.Entities;

namespace BankApplication.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly IBankApplicationContext _context;

        public CustomerRepo(IBankApplicationContext context)
        {
            _context = context;
        }

        // Get hashed password by Username
        public async Task<string> GetHashedPasswordByUsernameAsync(string username)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);

                return await db.QuerySingleOrDefaultAsync<string>("sp_GetHashedPasswordByUsername", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Get customer by Username
        public async Task<Customer> GetCustomerByUsernameAsync(string username)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);

                return await db.QuerySingleOrDefaultAsync<Customer>("sp_GetCustomerByUsername", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        //public async Task<string> GetSaltByUsernameAsync(string username) // I don't use this method anymore since for some reason I can't manage BCrypt to generate a hash without salt. I could obviously remove the salt using a string method like ".Insert" and replace the salt with an empty string but it doesn't seem right.
        //{
        //    using (var db = _context.GetConnection())
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@Username", username);

        //        return await db.QuerySingleOrDefaultAsync<string>("sp_GetSaltByUsername", parameters, commandType: CommandType.StoredProcedure);
        //    }
        //}

        // Get CustomerId by Username
        public async Task<int> GetCustomerIdByUsernameAsync(string username)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);

                return await db.QuerySingleOrDefaultAsync<int>("sp_GetCustomerIdByUsername", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Create a new customer and account
        public async Task CreateNewCustomerAndAccountAsync(Customer customer, Account account)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Gender", customer.Gender);
                parameters.Add("@Givenname", customer.Givenname);
                parameters.Add("@Surname", customer.Surname);
                parameters.Add("@Streetaddress", customer.Streetaddress);
                parameters.Add("@City", customer.City);
                parameters.Add("@Zipcode", customer.Zipcode);
                parameters.Add("@Country", customer.Country);
                parameters.Add("@CountryCode", customer.CountryCode);
                parameters.Add("@Birthday", customer.Birthday);
                parameters.Add("@Telephonecountrycode", customer.Telephonecountrycode);
                parameters.Add("@Telephonenumber", customer.Telephonenumber);
                parameters.Add("@Emailaddress", customer.Emailaddress);
                parameters.Add("@Username", customer.Username);
                parameters.Add("@HashedPassword", customer.HashedPassword);
                parameters.Add("@Frequency", account.Frequency);
                parameters.Add("@AccountTypesId", account.AccountTypesId);
                parameters.Add("@AccountNumber", account.AccountNumber);

                await db.ExecuteAsync("sp_CreateNewCustomerAndAccount", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Search customers based on their name and get back a list
        public async Task<IEnumerable<Customer>> SearchCustomersByNameAsync(string customerName)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerName", customerName);

                return await db.QueryAsync<Customer>("sp_SearchCustomersByName", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
