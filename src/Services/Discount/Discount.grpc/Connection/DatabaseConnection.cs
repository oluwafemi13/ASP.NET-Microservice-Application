using Npgsql;
using Dapper;

namespace Discount.grpc.Connection
{
    public class DatabaseConnection
    {
        


        public NpgsqlConnection ConnectionString(IConfiguration configuration)
        {
           
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings: ConnectionString"));
            
            return connection;

        }
    }
}
