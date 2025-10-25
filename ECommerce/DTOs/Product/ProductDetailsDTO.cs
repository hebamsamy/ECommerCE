namespace ECommerce.DTOs
{
    public class ProductDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ShopName { get; set; }
        public List<string>? ProductImages { get; set; } = null;

    }
}
