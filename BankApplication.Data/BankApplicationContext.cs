using Microsoft.Data.SqlClient;
using BankApplication.Data.Interfaces;
using BankApplication;
using Microsoft.Extensions.Configuration;

namespace BankApplication.Data
{
    public class BankApplicationContext : IBankApplicationContext
    {
        private readonly string? _connString;

        public BankApplicationContext(IConfiguration config)
        {
            _connString = config.GetConnectionString("Inlämningsuppgift3");
        }


        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connString);
        }
    }
}
