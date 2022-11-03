using CatalogueAPI.Models;

namespace CatalogueAPI.Repository.Interface
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetProduct();

        Task<Product> GetProductById(string id);

        Task<IEnumerable<Product>> GetProductByName(string name);

        Task<IEnumerable<Product>> GetProductByCategory(string productCategory);

        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
