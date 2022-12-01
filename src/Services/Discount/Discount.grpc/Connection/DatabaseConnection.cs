using Npgsql;
using Dapper;

namespace Discount.grpc.Connection
{
    public class DatabaseConnection
    {
        
        private readonly IConfiguration _configuration;

        public DatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public NpgsqlConnection ConnectionString()=>
 
            new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings: ConnectionString"));

    }
}
