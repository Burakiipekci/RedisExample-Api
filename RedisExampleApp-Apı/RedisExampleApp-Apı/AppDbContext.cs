using Microsoft.EntityFrameworkCore;
using RedisExampleApp_Apı.Models;

namespace RedisExampleApp_Apı
{
	public class AppDbContext  :DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
		{

		}
		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().HasData(
				new Product() { ıd = 1, Name = "Kalem", Price = 100 },
				new Product() { ıd = 2, Name = "silgi", Price = 100 } ,
				new Product() { ıd = 3, Name = "pc", Price = 100 }




				);
		}
	}
}
