using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemItemRepository : IItemsRepository
    {
        private readonly List<Item> items = [
            new Item{ Id= Guid.NewGuid(), Name = " Item1", Price=9},
            new Item{ Id= Guid.NewGuid(), Name = " Item2", Price=2},
            new Item{ Id= Guid.NewGuid(), Name = " Item3", Price=7},
            new Item{ Id= Guid.NewGuid(), Name = " Item4", Price=5}
        ];

        public InMemItemRepository()
        {
        }

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.SingleOrDefault(item => item.Id == id);
        }

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
        }

        public void DeleteItem(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
        }
    }
}