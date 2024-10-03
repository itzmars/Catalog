namespace Catalog.Dtos
{
    public record UpdateItemDto
    {
        public required string Name { get; init; }
        public decimal Price { get; init; }
    }
}