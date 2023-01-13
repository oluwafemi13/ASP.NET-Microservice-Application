using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Database_Contrats.persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    internal class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrdeRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _Logger;

        public DeleteOrderCommandHandler(IOrdeRepository orderRepository, IMapper mapper, ILogger logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _Logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var checkOrder = await _orderRepository.GetByIdAsync(request.Id);
            if (checkOrder == null)
            {
                _Logger.LogInformation($"order with Id{request.Id} was not found");
            }
            await _orderRepository.DeleteAsync(checkOrder);
            return Unit.Value;
        }
    }
}
