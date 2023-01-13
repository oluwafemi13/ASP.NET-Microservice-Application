using Ordering.Application.Models.Email;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Infrastructure.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
