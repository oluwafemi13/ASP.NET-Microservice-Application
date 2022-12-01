using Discount.grpc.Connection;
using Discount.grpc.Entities;
using Discount.grpc.Repositories.Interfaces;
using System.Linq;
using Dapper;
using Npgsql;

namespace Discount.grpc.Repositories
{
    public class DiscountRepository: IDiscountRepository
    {
        private readonly DatabaseConnection _databaseConnection;
        private readonly IConfiguration _configuration;

        public DiscountRepository(DatabaseConnection databaseConnection,
                                    IConfiguration configuration)
        {
            _databaseConnection = databaseConnection;
            _configuration = configuration;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var connection = _databaseConnection.ConnectionString();
            string sql = $"INSERT INTO Coupon(ProductName, description, Amount) VALUES ({coupon.ProductName}, {coupon.Description}, {coupon.Amount})";
            var affected = await connection.ExecuteAsync(sql);
            if (affected == 0)
                return false;
            return true;

        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            
            var connection = _databaseConnection.ConnectionString();
            string sql = $"DELETE * FROM Coupon WHERE ProductName = {productName}";
            //var check = await connection.ExecuteAsync("DELETE * FROM Coupon WHERE ProductName = @ProductName", new{ProductName = productName});
            var check = await connection.ExecuteAsync(sql);
            if (check == 0)
            {
               return false;
            }
            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings: ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new {ProductName = productName});
           // var coupon = await _databaseConnection.ConnectionString(_configuration).QueryFirstOrDefaultAsync<Coupon>($"SELECT * FROM Coupon WHERE ProductName = {productName}");

            if(coupon is null)
            {
                return new Coupon() { ProductName =productName, Amount = 0 , Description=$"No discount for {productName} at this time"};
            }

            return coupon;

        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var connection = _databaseConnection.ConnectionString();
            var sql = $"UPDATE Coupon SET ProductName = {coupon.ProductName}, Description = {coupon.Description}, Amount={coupon.Amount} WHERE Id = {coupon.Id} ";
            var check = await connection.ExecuteAsync(sql);
            if (check == 0)
                return false;
            return true;
            
        }
    }
}
