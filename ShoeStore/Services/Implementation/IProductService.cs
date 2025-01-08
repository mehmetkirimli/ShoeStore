using ShoeStore.Entities;

namespace ShoeStore.Services.Implementation
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync (Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
