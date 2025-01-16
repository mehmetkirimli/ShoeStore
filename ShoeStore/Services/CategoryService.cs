using AutoMapper;
using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Repositories.Implementation;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper mappper;

        public CategoryService(IRepository<Category> repo,IMapper mapper)
        {
            this._categoryRepository = repo;
            this.mappper = mapper;  
        }

        public async Task AddCategoryAsync(CategoryDTO dto)
        {
            if (dto != null) { await _categoryRepository.AddAsync(mappper.Map<Category>(dto)); }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            IEnumerable<Category> enumerable = await _categoryRepository.GetAllAsync();
            return mappper.Map<IEnumerable<CategoryDTO>>(enumerable);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            return mappper.Map<CategoryDTO>(category);
        }

        public async Task UpdateCategoryAsync(CategoryDTO updatedCategory)
        {
            if(await _categoryRepository.GetByIdAsync(updatedCategory.Id) != null)
            {
                await _categoryRepository.UpdateAsync(mappper.Map<Category>(updatedCategory));
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            if (await _categoryRepository.GetByIdAsync(id) != null)
            {
                await _categoryRepository.DeleteAsync(id);
            }
        }

    }

}
