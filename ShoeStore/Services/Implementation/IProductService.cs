using ShoeStore.DTO;
using ShoeStore.Entities;

namespace ShoeStore.Services.Implementation
{
    public interface IProductService
    {
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task AddProductAsync (ProductDTO productDto);
        Task UpdateProductAsync(ProductDTO productDto);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<List<ProductDTO>> GetProductListByCategory(int categoryId);
    }
}
