using FluentValidation;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is Required").NotNull().
                MaximumLength(60).WithMessage("Username must not Exceed 60 Characters");

            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email Address is Required").NotNull().
               WithMessage("Email Must not be Empty");

            RuleFor(x => x.TotalPrice).NotEmpty().WithMessage("Total Price is Required").NotNull().GreaterThan(0).
               WithMessage("Total Price must not be less than Zero");
        }
    }
}
