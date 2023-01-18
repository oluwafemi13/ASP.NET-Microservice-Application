using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Database_Contrats.persistence.Interface;
using Ordering.Application.Contracts.Infrastructure.Interface;
using Ordering.Application.Models.Email;
using Ordering.Application.persistence.Interface;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration configuration)
        {            
            Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));                        
            Services.AddScoped<IOrdeRepository, OrderRepository>();

            Services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            Services.AddTransient<IEmailService, EmailService>();

            return Services;
        }
    }
}
