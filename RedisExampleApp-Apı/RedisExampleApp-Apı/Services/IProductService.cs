using RedisExampleApp_Apı.Models;

namespace RedisExampleApp_Apı.Services
{
	public interface IProductService
	{
		Task<List<Product>> GetAsync();
		Task<Product> GetBtIdAsync(int Id);
		Task<Product> CreateAsync(Product product);
	}
}
