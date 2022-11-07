using CatalogueAPI.Data.DataSeeder;
using CatalogueAPI.Data.Interface;
using CatalogueAPI.Models;
using MongoDB.Driver;

namespace CatalogueAPI.Data
{
    public class CatalogueContext : ICatalogueContext
    {

        public CatalogueContext(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetValue<string>("DatabaseSettings: ConnectionString"));
            var database = mongoClient.GetDatabase(configuration.GetValue<string>("DatabaseSettings: DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings: CollectionName"));
            CatalogueContextSeed.seedData(Products);
        }

        public IMongoCollection<Product> Products
        {
            get;
        }

        
    }
}
