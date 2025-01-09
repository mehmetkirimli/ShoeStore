using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Entities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }  // Adres ID'si
        [Required]
        public int UserId { get; set; }  // Kullanıcı ID'si

        [ForeignKey("UserId")]
        public User? User { get; set; }  // Kullanıcı

        [Required]
        public string? Street { get; set; }  // Sokak adı

        [Required]
        public string? City { get; set; }  // Şehir
        public string? PostalCode { get; set; }  // Posta kodu

        [Required]
        public string? Country { get; set; }  // Ülke
    }

}
