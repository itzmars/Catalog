using Catalog.Entities;
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

        public void CreateItem(Item item)
        {
            itemCollection.InsertOne(item);
        }

        public IEnumerable<Item> GetItems()
        {
            return itemCollection.Find(item=>true).ToList();
        }


        public Item GetItem(Guid id)
        {
            var filter = filterDefinition.Eq(item => item.Id, id);
            return itemCollection.Find(item => item.Id == id).SingleOrDefault();
        }

      
        public void UpdateItem(Item item)
        {
            var filter = filterDefinition.Eq(ExistingItem => ExistingItem.Id, item.Id);
            itemCollection.ReplaceOne(filter, item);
        }


        public void DeleteItem(Guid id)
        {
            var filter = filterDefinition.Eq(item => item.Id, id);
            itemCollection.DeleteOne(filter);
        }
    }
}