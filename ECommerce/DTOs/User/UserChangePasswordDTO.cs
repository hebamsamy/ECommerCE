using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTOs
{
    public class UserChangePasswordDTO
    {
        [Required]
        [DataType (DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
