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


        // Post /item
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto){
            Item item = new(){
                Id= Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new {id=item.Id}, item.AsDto());
        }


        // PUT /items/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateItem(Guid id, UpdateItemDto updateItemDto){
            var existingItem = _repository.GetItem(id);

            if(existingItem == null){
                return NotFound();
            }

            Item updateItem = existingItem with {
                Name = updateItemDto.Name,
                Price = updateItemDto.Price
            };

            _repository.UpdateItem(updateItem);
            return NoContent();
        }

    }
}