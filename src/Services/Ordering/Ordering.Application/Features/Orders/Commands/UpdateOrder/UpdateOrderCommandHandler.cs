using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Database_Contrats.persistence.Interface;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrdeRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrdeRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var findOrder =await _orderRepository.GetByIdAsync(request.Id);
            if (findOrder == null)
            {
                _logger.LogInformation("order not available");
                throw new NotFoundException(nameof(Order), request.Id);
            }
             _mapper.Map(request, findOrder, typeof(UpdateOrderCommand), typeof(Order));
             await _orderRepository.UpdateAsync(findOrder);
            _logger.LogInformation($"Order Updated{nameof(UpdateOrderCommandHandler)}");

            return Unit.Value;
        }
    }
}
