using CatalogueAPI.Data.Interface;
using CatalogueAPI.Models;
using MongoDB.Driver;

namespace CatalogueAPI.Data
{
    public class CatalogueContext : ICatalogueContext
    {

        public CatalogueContext(IConfiguration configuration)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetValue<string>("DatabaseSettings: ConnectionString"));
            var dataBase = mongoClient.GetDatabase(configuration.GetValue<string>("DatabaseSettings: DatabaseName"));
            var collections = dataBase.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings: CollectionName"));
        }

        public IMongoCollection<Product> products
        {
            get;
        }

        
    }
}
