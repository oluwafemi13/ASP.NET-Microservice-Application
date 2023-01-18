using DbUp;
using System.Reflection;

namespace Discount.grpc.Extensions
{
    public static class HostExtension
    {
       
            public static IHost MigrateDatabase<TContext>(this IHost host)
            {
                using (var scope = host.Services.CreateScope())
                {
                    var Services = scope.ServiceProvider;
                    var configuration = Services.GetRequiredService<IConfiguration>();
                    var logger = Services.GetRequiredService<ILogger<TContext>>();

                    logger.LogInformation("Migrating postresql database.");

                    string connection = configuration.GetValue<string>("DatabaseSettings:ConnectionString");

                    EnsureDatabase.For.PostgresqlDatabase(connection);

                    var upgrader = DeployChanges.To
                        .PostgresqlDatabase(connection)
                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                        .LogToConsole()
                        .Build();

                    var result = upgrader.PerformUpgrade();

                    if (!result.Successful)
                    {
                        logger.LogError(result.Error, "An error occurred while migrating the postresql database");
                        return host;
                    }


                    logger.LogInformation("Migrated postresql database.");
                }

                return host;
            }
        }
    }

