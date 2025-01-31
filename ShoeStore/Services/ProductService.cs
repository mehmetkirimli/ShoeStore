using AutoMapper;
using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Repositories.Implementation;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepo, IMapper mapper) 
        { 
            this._productRepository = productRepo;
            this._mapper = mapper;
        }

        public async Task AddProductAsync(ProductDTO dto)
        {
            await _productRepository.AddAsync(_mapper.Map<Product>(dto));
        }

        public async Task DeleteProductAsync(int id)    
        {
            if(_productRepository.GetByIdAsync(id) == null)
            {
                throw new Exception("Product not found");
            }
            await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);

        }

        public async Task UpdateProductAsync(ProductDTO dto)
        {
            if(GetProductByIdAsync(dto.Id) == null)
            {
                throw new Exception("Product not found");
            }
            await _productRepository.UpdateAsync(_mapper.Map<Product>(dto));
        }

        public async Task<List<ProductDTO>> GetProductListByCategory(int categoryId)
        {
            List<Product> products = await _productRepository.FindByConditionAsync(p => p.CategoryId == categoryId , p => p.Category);
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<bool> CheckStockAsync(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Ürün bulunamadı!");

            return product.Stock >= quantity; // Stok yeterli mi?
        }

        public async Task DecreaseStockAsync(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Ürün bulunamadı!");

            if (product.Stock < quantity)
                throw new Exception($"{product.Name} için yeterli stok bulunmuyor!");

            product.Stock -= quantity; // Stoktan düş
            await _productRepository.UpdateAsync(product); // Güncelle
        }



    }
}
