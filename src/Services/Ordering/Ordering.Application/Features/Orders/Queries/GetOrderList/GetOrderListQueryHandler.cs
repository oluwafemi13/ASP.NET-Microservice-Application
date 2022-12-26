using MediatR;
using Ordering.Application.Features.Orders.Queries.GetOrderQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler: IRequestHandler<GetOrderListQuery, List<OrdersDTO>>
    {

       /* public Task<List<OrdersDTO>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            //return Handle(request, cancellationToken);
            throw new NotImplementedException();
        }*/
    }
}
