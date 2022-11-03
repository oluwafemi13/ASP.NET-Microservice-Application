using CatalogueAPI.Data.Interface;
using CatalogueAPI.Models;
using CatalogueAPI.Repository.Interface;
using MongoDB.Driver;

namespace CatalogueAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogueContext _context;

        public ProductRepository(ICatalogueContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateProduct(Product product)
        {
            await _context.products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.ProductId, id);  
            DeleteResult result = await _context.products.DeleteOneAsync(filter);

            return result.IsAcknowledged && 
                   result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProduct()
        {
            return await _context.products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string productCategory)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Category, productCategory);
            return await _context.
                                products.
                                Find(filterDefinition).
                                ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            
            return await _context.products.Find(p => p.ProductId.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context.
                                products.
                                Find(filterDefinition).
                                ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _context.products.
                                    ReplaceOneAsync(filter: f =>f.ProductId == product.ProductId, replacement: product);
            

            return result.IsAcknowledged &&
                   result.ModifiedCount > 0;
        }
    }
}
