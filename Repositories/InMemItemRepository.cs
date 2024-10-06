using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemItemRepository : IItemsRepository
    {
        private readonly List<Item> _items = [
            new Item{ Id= Guid.NewGuid(), Name = " Item1", Price=9},
            new Item{ Id= Guid.NewGuid(), Name = " Item2", Price=2},
            new Item{ Id= Guid.NewGuid(), Name = " Item3", Price=7},
            new Item{ Id= Guid.NewGuid(), Name = " Item4", Price=5}
        ];

        public InMemItemRepository()
        {
          
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(_items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = _items.SingleOrDefault(item => item.Id == id);
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            _items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = _items.FindIndex(existingItem => existingItem.Id == item.Id);
            _items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = _items.FindIndex(existingItem => existingItem.Id == id);
            _items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}