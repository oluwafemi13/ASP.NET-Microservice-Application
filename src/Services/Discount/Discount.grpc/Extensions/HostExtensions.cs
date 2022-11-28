using Npgsql;

namespace Discount.grpc.Extensions
{
    //using the IHost property to automatically run database migration using Dapper.
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider;
                var config = databaseService.GetRequiredService<IConfiguration>();
                var logger = databaseService.GetRequiredService<ILogger<T>>();

                try
                {
                    logger.LogInformation("Migrating Postgres database");
                    using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection

                    };
                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                               ProductName VARCHAR(24) NOT NULL, 
                                                               Description TEXT, 
                                                               Amount INT)";
                    command.ExecuteNonQuery();

                    //seeding into the database table created
                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES (IPhone x, Smartphone, $1000)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES (Samsung, Smartphone, $1200)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES (Huawei, Smartphone, $1000)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES (Nokia, Smartphone, $1000)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES (Sony Xperia, Smartphone, $1000)";
                    command.ExecuteNonQuery();


                    logger.LogInformation("Data successfully Migrated into the Database");


                }
                catch (NpgsqlException ex)
                {

                    logger.LogError(ex, "Unable to Migrate Postgres Database from the DiscountAPI");
                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        logger.LogInformation(retryForAvailability, "Retrying Database Migration");
                        System.Threading.Thread.Sleep(2500);
                        MigrateDatabase<T>(host, retryForAvailability);

                    }
                    
                }
               
            }
            return host;
        }
    }
}
