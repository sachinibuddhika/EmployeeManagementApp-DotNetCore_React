using Microsoft.Data.SqlClient;

namespace EmployeeManagementAPI.Data
{
    public class DatabaseHelper
    {

        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
