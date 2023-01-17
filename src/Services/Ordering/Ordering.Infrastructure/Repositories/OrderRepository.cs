using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Database_Contrats.persistence.Interface;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrdeRepository
    {
        public OrderRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUsername(string username)
        {
            var search = await _dbContext.Orders.Where(o => o.UserName == username).ToListAsync();
            return search;
        }
    }
}
