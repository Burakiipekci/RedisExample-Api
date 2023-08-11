using RedisExampleApp_Apı.Models;
using RedisExampleApp_Apı.Repository;

namespace RedisExampleApp_Apı.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Product> CreateAsync(Product product)
		{
			return await _productRepository.CreateAsync(product);
		}

		public async Task<List<Product>> GetAsync()
		{
			return await _productRepository.GetAsync();
		}

		public async Task<Product> GetBtIdAsync(int Id)
		{
			return await _productRepository.GetBtIdAsync(Id);
		}
	}
}
