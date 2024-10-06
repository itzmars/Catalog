using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {

        private const string DatabaseName = "Catalog";
        private const string CollectionName = "Items";
        private readonly IMongoCollection<Item> itemCollection;
        public MongoDbItemsRepository(IMongoClient mongoClient) { 
            IMongoDatabase database = mongoClient.GetDatabase(DatabaseName);
            itemCollection = database.GetCollection<Item>(CollectionName);

        }

        public void CreateItem(Item item)
        {
            itemCollection.InsertOne(item);
        }


        public Item GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }


        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}