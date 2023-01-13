using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Database_Contrats.persistence.Interface;
using Ordering.Application.Contracts.Infrastructure.Interface;
using Ordering.Application.Models;
using Ordering.Application.persistence.Interface;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrdeRepository _OrderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;
        private readonly IEmailService _emailService;

        public CheckoutOrderCommandHandler(IOrdeRepository OrderRepository, 
                                           IMapper mapper, 
                                           ILogger<CheckoutOrderCommandHandler> logger, 
                                           IEmailService emailService)
        {
            _emailService= emailService;
            _OrderRepository = OrderRepository;
            _logger= logger;
            _mapper= mapper;
        }
       
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            
            var correctOrder = _mapper.Map<Order>(request);
            var newOrder =await _OrderRepository.AddAsync(correctOrder);
            _logger.LogInformation($"order {newOrder.Id} is successfully created");
            await SendEmail(newOrder);
            return newOrder.Id; 
        }

        //will include abstractions later
        private async Task SendEmail(Order order)
        {
            var email = new Email
            {
                To = order.EmailAddress,
                Subject = $"Your {order.Id} Order has been confirmed",
                Body = "Order was Created"
            };
            try
            {
                await _emailService.SendEmail(email);

            }catch(Exception ex)
            {
                _logger.LogInformation($"Order {order.Id} failed to send due to some error with the mail sending Service.\n {ex.Message}");
            }
        }
    }
}
