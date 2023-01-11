using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Database_Contrats.persistence.Interface;
using Ordering.Application.Features.Orders.Queries.GetOrderQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrdersDTO>>
    {
        private readonly IOrdeRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IOrdeRepository orderRepository,
                                        IMapper mapper)
        {
            _mapper= mapper;
            _orderRepository= orderRepository;
        }

        public async Task<List<OrdersDTO>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrdersByUsername(request.UserName);
            return  _mapper.Map<List<OrdersDTO>>(order);
        }
    }
}
