using ECommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTOs
{
    //Create a DTO for adding a new product with the following properties:
    //AddProductDTO:
    //ProductCreationDTO

    public class ProductCreationDTO
    {
        [Required , StringLength (100, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Description { get; set; }
        [Required (ErrorMessage ="Please add Valid Date")]

        public DateTime ExpireDate { get; set; }
        [Required]
        [Display(Name= "Select Category")]
        public int CategoryId { get; set; }

        public string SupplierId { get; set; } = "";

        public IFormFileCollection? Images { get; set; } = null;

        public List<string>? ImageUrls { get; set; } = null;

    }
}
