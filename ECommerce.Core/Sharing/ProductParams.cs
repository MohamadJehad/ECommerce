namespace ECommerce.Core.Sharing
{
    public class ProductParams
    {
        public string sort {  get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public string? Search { get; set; }
    }
}
