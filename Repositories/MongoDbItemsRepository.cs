using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {

        private const string DatabaseName = "Catalog";
        private const string CollectionName = "Items";
        private readonly IMongoCollection<Item> itemCollection;
        private readonly FilterDefinitionBuilder<Item> filterDefinition = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient) { 
            IMongoDatabase database = mongoClient.GetDatabase(DatabaseName);
            itemCollection = database.GetCollection<Item>(CollectionName);

        }

        public async Task CreateItemAsync(Item item)
        {
            await itemCollection.InsertOneAsync(item);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemCollection.Find(new BsonDocument()).ToListAsync();
        }


        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterDefinition.Eq(item => item.Id, id);
            return await itemCollection.Find(item => item.Id == id).SingleOrDefaultAsync();
        }

      
        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterDefinition.Eq(ExistingItem => ExistingItem.Id, item.Id);
            await itemCollection.ReplaceOneAsync(filter, item);
        }


        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterDefinition.Eq(item => item.Id, id);
            await itemCollection.DeleteOneAsync(filter);
        }
    }
}