using RedisExampleApp_Apı.Models;

namespace RedisExampleApp_Apı.Repository
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAsync();
		Task<Product> GetBtIdAsync(int Id);
		Task<Product> CreateAsync(Product product);

	}
}
