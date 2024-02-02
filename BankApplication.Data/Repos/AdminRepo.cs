using Dapper;
using System.Data;
using BankApplication.Data.Interfaces;
using BankApplication.Domain.Entities;

namespace BankApplication.Data.Repos
{
    public class AdminRepo : IAdminRepo
    {
        private readonly IBankApplicationContext _context;

        public AdminRepo(IBankApplicationContext context)
        {
            _context = context;
        }

        // Get Admin by Username
        public async Task<Admin> GetAdminByUsernameAsync(string username)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);

                return await db.QuerySingleOrDefaultAsync<Admin>("sp_GetAdminByUsername", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
