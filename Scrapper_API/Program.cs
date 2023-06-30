using Producer;
using Scrapper_API;
using Scrapper_API.Services;
using Scrapper_API.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IScrapingService, ScrapingService>();
builder.Services.AddHostedService<ScrapingBackgroundService>();
builder.Services.AddTransient<RabbitMqProducer>(); 
builder.Services.AddTransient<RedisCache>();
builder.Services.Configure<RedisConfiguration>(builder.Configuration.GetSection("ConnectionString"));

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("Redis");
//    options.InstanceName = "RedisDB_";
//});

builder.Services.AddSingleton<RabbitMQ.Client.IConnectionFactory, RabbitMQ.Client.ConnectionFactory>(sp =>
{
    var factory = new RabbitMQ.Client.ConnectionFactory()
    {
        // Configure the properties of the factory here
        HostName = "localhost",
        UserName = "guest",
        Password = "guest",
        //...
    };
    return factory;
});



var app = builder.Build();

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
