using System.ComponentModel.DataAnnotations;

namespace ShoeStore.DTO
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(40)]
        public string? FullName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be 11 digits.")]
        public string? Phone { get; set; }

        [Required]
        public string? Password { get; set; }  // Şifre
    }

}
