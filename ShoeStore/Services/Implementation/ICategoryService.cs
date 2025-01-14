using ShoeStore.DTO;

namespace ShoeStore.Services.Implementation
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(CategoryDTO dto);
        Task DeleteCategoryAsync(int id);
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(CategoryDTO dto);

    }
}
