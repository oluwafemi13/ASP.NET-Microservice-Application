using Microsoft.EntityFrameworkCore;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.API.Extensions;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

//register configuration
ConfigurationManager configuration = builder.Configuration;
// Add Services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
//database dependency
builder.Services.AddDbContext<DatabaseContext>(Options => Options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
   /* b => b.MigrationsAssembly("Ordering.API")));*/

var app = builder.Build();

#region automatic migration using IHost Extension class
app.MigrateDatabase<DatabaseContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderDatabaseContextSeed>>();
    OrderDatabaseContextSeed
        .SeedAsync(context, logger)
        .Wait();
});
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
