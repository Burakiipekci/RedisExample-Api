using Microsoft.EntityFrameworkCore;
using Redis.Cache;
using RedisExampleApp_Apı;
using RedisExampleApp_Apı.Repository;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("myDb"));
builder.Services.AddSingleton<RedisService>(sp =>
{
	return new RedisService(builder.Configuration["CacheOptions:Url"]);

});
builder.Services.AddSingleton<IDatabase>(sp =>
{
	var redisService = sp.GetRequiredService<RedisService>();
	var redis = redisService.GetDb(0);
	return redis;
});
builder.Services.AddScoped<IProductRepository,ProductRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scoper= app.Services.CreateScope())
{
	var dbContext= scoper.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
