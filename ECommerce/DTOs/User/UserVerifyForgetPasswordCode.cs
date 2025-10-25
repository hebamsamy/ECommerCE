using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTOs
{
    public class UserVerifyForgetPasswordCodeDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType (DataType.PhoneNumber)]
        public string Code { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
