using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ordering.Application;
using Ordering.Application.Contracts.Database_Contrats.persistence.Interface;
using Ordering.Application.Contracts.Infrastructure.Interface;
using Ordering.Application.Models.Email;
using Ordering.Application.persistence.Interface;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
Services.AddScoped<IOrdeRepository, OrderRepository>();

Services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
