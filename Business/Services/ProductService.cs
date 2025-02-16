using Business.Models;
using Business.Factories;
using Data.Interfaces;

namespace Business.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductAsync(ProductModel model)
        {
            var entity = ProductFactory.Create(model);
            await _productRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(ProductFactory.Create);
        }

        public async Task<ProductModel?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null ? ProductFactory.Create(product) : null;
        }

        public async Task UpdateProductAsync(ProductModel model)
        {
            var entity = ProductFactory.Create(model);
            await _productRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }
    }
}
