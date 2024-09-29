using Catalog.Dtos;
using Catalog;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        //GEt /items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = _repository.GetItems().Select(item => item.AsDto());
            
            return items;
        }


        //Get /items/{id}
        [HttpGet("{id}")]
        public ItemDto GetItem(Guid id)
        {
            var item = _repository.GetItem(id);

            return item.AsDto();
        }
    }
}