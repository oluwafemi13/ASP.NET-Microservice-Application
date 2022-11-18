using Npgsql;
using Dapper;

namespace Discount.API.Connection
{
    public class DatabaseConnection
    {
        private  readonly IConfiguration _configuration;

        public DatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        public ConfigurationBuilder ConnectionString()
        {
           
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings: ConnectionString"));
            /*var connection = new NpgsqlConnection();
            using (connection)
            {
                conf = _configuration.GetValue<string>("DatabaseSettings: ConnectionString");

            }*/
            return new ConfigurationBuilder(connection);

        }
    }
}
