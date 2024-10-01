namespace Catalog.Dtos
{
    public record CreateItemDto
    {
        public required string Name { get; init; }
        public decimal Price { get; init; }
    }
}