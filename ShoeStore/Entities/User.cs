using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace ShoeStore.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Kullanıcı ID'si

        [Required]
        [StringLength(40)]
        public string FullName { get; set; }  // Kullanıcı adı

        [Required]
        public string Email { get; set; }  // E-posta

        [Required]
        public string PasswordHash { get; set; }  // Şifre (hash'lenmiş)

        [Required]
        [StringLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone Number must be 11 digits.")]
        public string Phone { get; set; }  // Telefon numarası
        public DateTime DateCreated { get; set; }  // Hesap oluşturulma tarihi
        public DateTime LastLogin { get; set; }  // Son giriş tarihi
        public ICollection<Address> Addresses { get; set; }  // Kullanıcının adresleri
    }

}
