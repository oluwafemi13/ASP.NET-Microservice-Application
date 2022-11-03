using CatalogueAPI.Models;
using MongoDB.Driver;

namespace CatalogueAPI.Data.Interface
{
    public interface ICatalogueContext
    {

        public IMongoCollection<Product> products { get; }
    }
}
