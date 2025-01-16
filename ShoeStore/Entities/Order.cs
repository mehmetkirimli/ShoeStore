using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoeStore.Enumarations;

namespace ShoeStore.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Sipariş ID'si
        [Required]
        public int UserId { get; set; }  // Kullanıcı ID'si
        [ForeignKey("UserId")]
        public User User { get; set; }  // Kullanıcı
        public DateTime OrderDate { get; set; }  // Sipariş tarihi
        public decimal TotalAmount { get; set; }  // Sipariş toplam tutar
        [Required]
        public OrderStatus OrderStatus { get; set; }  // Sipariş durumu (örneğin "Hazırlanıyor", "Kargoya Verildi")
        public Payment Payment { get; set; }  // Ödeme bilgisi
        public ICollection<OrderItem>? OrderItems { get; set; }  // Sipariş içindeki ürünler
    }

}
