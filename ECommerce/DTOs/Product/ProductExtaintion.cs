using ECommerce.Models;

namespace ECommerce.DTOs
{
    public static class ProductExtaintion
    {
         public static ProductDetailsDTO ToDTO (this Product product)
        {
            return new ProductDetailsDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                ExpireDate = product.ExpireDate,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "",
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier?.User.FullName ?? "",
                ShopName = product.Supplier?.ShopName ?? "",
                ProductImages = product.ProductImages?.Select(pi => pi.ImageUrl).ToList() ?? new List<string>()
            };
        }
    }
}
