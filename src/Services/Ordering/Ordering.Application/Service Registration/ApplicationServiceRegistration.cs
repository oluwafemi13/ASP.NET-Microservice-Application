using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
//using Ordering.Application.Behaviours;
using Ordering.Application.Behavoiurs;
using System.Reflection;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            Services.AddMediatR(Assembly.GetExecutingAssembly());
            
            //Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return Services;
        }
    }
}
