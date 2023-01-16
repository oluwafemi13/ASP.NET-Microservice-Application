using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderDatabaseContext: DbContext
    {
        public OrderDatabaseContext():base()
        {

        }
        public DbSet<Order> Orders { get; set; }
    }
}
