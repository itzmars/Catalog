using Catalog.Configurations;
using Catalog.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

// Configure MongoDB setting
builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection("MongoDbSettings")
);

//Register MongoDbSettings to be injectable
builder.Services.AddSingleton(serviceProvider => 
    serviceProvider.GetRequiredService<IOptions<MongoDbConfiguration>>().Value);

//Register MongoClient as a singleton
builder.Services.AddSingleton<IMongoClient>(serviceProvider => {
    var settings = serviceProvider.GetRequiredService<MongoDbConfiguration>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<IItemsRepository, MongoDbItemsRepository>();

//builder.Services.AddSingleton<IItemsRepository, InMemItemRepository>();

builder.Services.AddControllers(options => 
{
    // SuppressAsyncSuffixInActionNames is set to false
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
