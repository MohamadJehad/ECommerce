using Microsoft.AspNetCore.Http;

namespace ECommerce.Core.DTO
{
    public record ProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public virtual List<PhotoDTO> Photos { get; set; } = new List<PhotoDTO>();
    }

    public record AddProductDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public IFormFileCollection Photos { get; set; }
    }

    public record UpdateProductDTO : AddProductDTO
    {
        public int Id { get; set; }
    }

    public record PhotoDTO
    {
        public string ImageName { get; set; } = string.Empty;
        public int ProductId { get; set; }

    }
}
