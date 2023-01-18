using Basket.API.DiscountService;
using Basket.API.Repository.Interface;
using Discount.grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container.
builder.Services.AddStackExchangeRedisCache(option =>
{
    string Connection = builder.Configuration.GetConnectionString("");
    option.Configuration = builder.Configuration.GetValue<string>("CacheSettings: ConnectionString");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//register the discount GRPC client proto buffer service
//which was automatically generated
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    o => o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"])
    );
//register the discount grpc service created
builder.Services.AddScoped<DiscountGrpcService>();

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
