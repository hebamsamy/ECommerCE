using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTOs
{
    public class UserLoginDTO
    {
        //hebamsamy
        //hebasamy655@gamil.com
        [Required]
        [Display(Name = "User Name Or Email")]
        public string Text { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        
        public bool RememberMe { get; set; }
    }
}
