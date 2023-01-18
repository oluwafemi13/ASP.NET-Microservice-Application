using AutoMapper;
using Discount.grpc.Connection;
using Discount.grpc.Entities;
using Discount.grpc.Extensions;
using Discount.grpc.MapperProfile;
using Discount.grpc.Protos;
using Discount.grpc.Repositories;
using Discount.grpc.Repositories.Interfaces;
using Discount.grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add Services to the container.
//builder.Services.AddAutoMapper(typeof(DiscountProfile));
var config = new MapperConfiguration(cfg => {
 cfg.AddProfile<DiscountProfile>();
});
builder.Services.AddGrpc();
builder.Services.AddSingleton<DatabaseConnection>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();


var app = builder.Build();
app.MigrateDatabase<Program>();
// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
