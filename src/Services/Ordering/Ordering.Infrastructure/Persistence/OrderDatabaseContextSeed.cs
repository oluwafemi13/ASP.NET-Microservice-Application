using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;


namespace Ordering.Infrastructure.Persistence
{
    public class OrderDatabaseContextSeed
    {
        public static async Task SeedAsync(DatabaseContext orderContext, ILogger<OrderDatabaseContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(DatabaseContext).Name);
            }
        }


        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "Anonymous", FirstName = "Femi", LastName = "Iwuchukwu", EmailAddress = "jabikem@gmail.com", AddressLine = "Ikorodu", Country = "Nigeria", TotalPrice = 455 }
            };
        }
    }
}
