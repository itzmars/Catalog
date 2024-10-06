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
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await _repository.GetItemsAsync()).Select(item => item.AsDto());

            return items;
        }


        //Get /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item =await _repository.GetItemAsync(id);

            if(item is null){
                return NotFound();
            }

            return item.AsDto();
        }


        // Post /item
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }


        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = _repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updateItem = await existingItem with
            {
                Name = updateItemDto.Name,
                Price = updateItemDto.Price
            };

            await _repository.UpdateItemAsync(updateItem);
            return NoContent();
        }


        //Delelete /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id){
            var existingItem = _repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            await _repository.DeleteItemAsync(id);
            
            return NoContent();
        }

    }
}