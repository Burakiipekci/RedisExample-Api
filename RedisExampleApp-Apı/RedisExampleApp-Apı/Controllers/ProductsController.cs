using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisExampleApp_Apı.Models;
using RedisExampleApp_Apı.Repository;
using System.Xml.Linq;

namespace RedisExampleApp_Apı.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductRepository _productRepository;

		public ProductsController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}
		
		[Microsoft.AspNetCore.Mvc.HttpGet(Name = "GetAll")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _productRepository.GetAsync());	
		}
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			return Ok(await _productRepository.GetBtIdAsync(id));
		}
		[HttpPost(Name = "Create")]
		public async Task<IActionResult> Create(Product product)
		{
			return Ok(await _productRepository.CreateAsync(product));
		}
	}
}
