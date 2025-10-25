namespace ECommerce.Models
{
    public class ProductImage : BaseModel
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        // Navigation property
        public virtual Product Product { get; set; }
    }
}