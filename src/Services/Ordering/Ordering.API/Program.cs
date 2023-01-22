using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ordering.Application;
using Ordering.Application.Contracts.Database_Contrats.persistence.Interface;
using Ordering.Application.Contracts.Infrastructure.Interface;
using Ordering.Application.Models.Email;
using Ordering.Application.persistence.Interface;
using Ordering.Infrastructure;
using Microsoft.Extensions.Configuration;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using EllipticCurve;
using Microsoft.Extensions.Options;



var builder = WebApplication.CreateBuilder(args);

//register configuration
ConfigurationManager configuration = builder.Configuration;
// Add Services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddDbContext<DatabaseContext>(Options => Options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("Ordering.Infrastructure")));

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
