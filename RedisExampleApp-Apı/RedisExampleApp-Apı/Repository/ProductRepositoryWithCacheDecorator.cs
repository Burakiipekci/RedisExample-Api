using Redis.Cache;
using RedisExampleApp_Apı.Models;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace RedisExampleApp_Apı.Repository
{
	public class ProductRepositoryWithCacheDecorator : IProductRepository
	{
		private const string productKey = "productCaches";
		private readonly IProductRepository _repository;
		private readonly RedisService _redis;
		private readonly IDatabase _db;

		public ProductRepositoryWithCacheDecorator(IProductRepository repository, RedisService redis)
		{
			_repository = repository;
			_redis = redis;
			_db = _redis.GetDb(0);
		}


		public async Task<Product> CreateAsync(Product product)
		{
			var newProduct = await _repository.CreateAsync(product);
			if (await _db.KeyExistsAsync(productKey))
			{

				await _db.HashSetAsync(productKey, product.Id, JsonSerializer.Serialize(newProduct));
			}
			return newProduct;

		}

		public async Task<List<Product>> GetAsync()
		{

			if (!await _db.KeyExistsAsync(productKey))
				return await LoadToCacheFromDbAsync();

			var products= new List<Product>();
			var cacheProducts = await _db.HashGetAllAsync(productKey);
			foreach (var item in  cacheProducts.ToList())
			{
				var product = JsonSerializer.Deserialize<Product>(item.Value);
				products.Add(product);
			}
			return products;


		}

		public async Task<Product> GetBtIdAsync(int Id)
		{
			if (await _db.KeyExistsAsync(productKey))
			{
				var product = await _db.HashGetAsync(productKey, Id);
				return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;

			}
			var loadProduct = await LoadToCacheFromDbAsync();
			return loadProduct.FirstOrDefault(x => x.Id == Id);
		}
		private async Task<List<Product>> LoadToCacheFromDbAsync()
		{
			var product = await _repository.GetAsync();
			product.ForEach(p =>
			{
				_db.HashSet(productKey, p.Id, JsonSerializer.Serialize(p));


			});
			return product;
		}
	}
}
