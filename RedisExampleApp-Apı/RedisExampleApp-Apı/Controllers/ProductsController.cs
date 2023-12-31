﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.Cache;
using RedisExampleApp_Apı.Models;
using RedisExampleApp_Apı.Repository;
using RedisExampleApp_Apı.Services;
using StackExchange.Redis;
using System.Xml.Linq;

namespace RedisExampleApp_Apı.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;
	

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		
		}

		[Microsoft.AspNetCore.Mvc.HttpGet(Name = "GetAll")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _productService.GetAsync());	
		}
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			return Ok(await _productService.GetBtIdAsync(id));
		}
		[HttpPost(Name = "Create")]
		public async Task<IActionResult> Create(Product product)
		{
			return Ok(await _productService.CreateAsync(product));
		}
	}
}
