using CatalogueAPI.Data.DataSeeder;
using CatalogueAPI.Data.Interface;
using CatalogueAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CatalogueAPI.Data
{
    public class CatalogueContext : ICatalogueContext
    {

        public CatalogueContext(/*IConfiguration configuration */  IOptions<DatabaseSettings> databaseSettings)
        {

          
                var mongoClient = new MongoClient(
                    databaseSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    databaseSettings.Value.DatabaseName);

                Products = mongoDatabase.GetCollection<Product>(
                    databaseSettings.Value.CollectionName);
            
            /*var mongoClient = new MongoClient(configuration.GetValue<string>("DatabaseSettings: ConnectionString"));
            var database = mongoClient.GetDatabase(configuration.GetValue<string>("DatabaseSettings: DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings: CollectionName"));*/
            CatalogueContextSeed.seedData(Products);
        }

        public IMongoCollection<Product> Products
        {
            get;
        }

        
    }
}
