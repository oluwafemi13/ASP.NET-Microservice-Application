using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderDatabaseContextSeed
    {
        public static async Task SeedAsync(OrderDatabaseContext orderContext, ILogger<OrderDatabaseContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderDatabaseContext).Name);
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
