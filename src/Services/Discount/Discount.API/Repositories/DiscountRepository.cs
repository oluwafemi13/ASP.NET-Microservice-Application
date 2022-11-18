using Discount.API.Connection;
using Discount.API.Entities;
using Discount.API.Repositories.Interfaces;
using System.Linq;
using Dapper;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository: IDiscountRepository
    {
        private readonly DatabaseConnection _databaseConnection;
        private readonly IConfiguration _configuration;

        public DiscountRepository(DatabaseConnection databaseConnection, IConfiguration configuration)
        {
            _databaseConnection = databaseConnection;
            _configuration = configuration;
        }

        public Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDiscount(string ProductName)
        {
            throw new NotImplementedException();
        }

        public async Task<Coupon> GetDiscount(string ProductName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings: ConnectionString"));

            var coupon = connection.QueryFirstOrDefaultAsync<Coupon>("");


        }

        public Task<bool> HasDiscount(string ProductName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
