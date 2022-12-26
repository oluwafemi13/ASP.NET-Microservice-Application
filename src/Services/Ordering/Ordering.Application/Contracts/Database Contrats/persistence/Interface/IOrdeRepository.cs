using Ordering.Application.persistence.Interface;
using Ordering.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Database_Contrats.persistence.Interface
{
    public interface IOrdeRepository: IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUsername(string username);
        
    }
}
