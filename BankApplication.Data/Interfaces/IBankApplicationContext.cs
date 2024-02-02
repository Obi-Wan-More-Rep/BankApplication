using Microsoft.Data.SqlClient;

namespace BankApplication.Data.Interfaces
{
    public interface IBankApplicationContext
    {
        SqlConnection GetConnection();
    }
}
