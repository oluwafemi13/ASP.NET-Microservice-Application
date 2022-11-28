using Discount.API.Connection;
using Discount.API.Extensions;
using Discount.API.Repositories;
using Discount.API.Repositories.Interfaces;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<DatabaseConnection>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();
app.MigrateDatabase<Program>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
