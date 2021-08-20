using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext,
         ILogger<OrderContextSeed> logger  
         )
        {
            if(!orderContext.orders.Any())
            {
                orderContext.orders.AddRange(GetPreconfiguredOrder());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrder()
        {
            return new List<Order>
            {
                new Order()
                {
                    UserName="sachin",
                    FirstName="sachin",
                    LastName="kumar",
                    EmailAddress="sachin@gmail.com",
                    AddressLine="Ohayo",
                    Country="United States",
                    TotalPrice=350                   
                }
            };
        }
    }
}
